using Explora.BusinessLogic.Services.Interfaces;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explora.Web.Controllers
{
    [Route("/totems")]
    [Authorize]
    public class TotemsController : Controller
    {
        protected ITotemService _totemService;
        protected IBlobStorageService _blobStorageService;

        public TotemsController(ITotemService totemService, IBlobStorageService blobStorageService)
        {
            _totemService = totemService;
            _blobStorageService = blobStorageService;
        }

        public async Task<IActionResult> Index()
        {
            var totems = await _totemService.GetAllAsync();
            var hostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
            foreach (var totem in totems)
            {
                totem.ImageUrl = hostUrl + totem.ImageUrl;
            }
            return View(totems);
        }

        [HttpGet("/totems/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/totems/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TotemDto totemDto, IFormFile image, IFormFile file)
        {
            //Save totem's data
            var totem = await _totemService.CreateAsync(totemDto);

            //Save totem's image
            await _blobStorageService.SaveResourceBlobAsync(
                new ResourceBlobDto
                {
                    Id = totem.Id,
                    Name = totem.Name,
                    Type = Resource.TotemImage,
                    Blob = image.OpenReadStream()
                });

            //Save totem's file blob
            await _blobStorageService.SaveResourceBlobAsync(
                new ResourceBlobDto
                {
                    Id = totem.Id,
                    Name = totem.Name,
                    Type = Resource.TotemBlob,
                    Blob = file.OpenReadStream()
                });
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/totems/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _totemService.GetByIdAsync(id));
        }

        [HttpPost("/totems/update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TotemDto totemDto, IFormFile image, IFormFile file)
        {
            //updates totem's data
            var totem = await _totemService.UpdateAsync(totemDto);

            if (image != null)
            {
                //updates totem's image
                await _blobStorageService.UpdateResourceBlobAsync(
                    new ResourceBlobDto
                    {
                        Id = totem.Id,
                        Name = totem.Name,
                        Type = Resource.TotemImage,
                        Blob = image.OpenReadStream()
                    });
            }

            if (file != null)
            {
                //updates totem's image
                await _blobStorageService.UpdateResourceBlobAsync(
                    new ResourceBlobDto
                    {
                        Id = totem.Id,
                        Name = totem.Name,
                        Type = Resource.TotemBlob,
                        Blob = file.OpenReadStream()
                    });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //remove totem;s metadata
            await _totemService.DeleteAsync(id);

            //remove totem's blob
            _blobStorageService.DeleteResource(new ResourceKeyDto { Id = id, Type = Resource.TotemBlob });

            //remove totem's image
            _blobStorageService.DeleteResource(new ResourceKeyDto { Id = id, Type = Resource.TotemImage });

            return Ok(true);
        }
    }
}
