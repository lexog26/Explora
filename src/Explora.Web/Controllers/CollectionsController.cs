using Explora.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Explora.DataTransferObjects.DataTransferObjects;

namespace Explora.Web.Controllers
{
    [Route("/collections")]
    [Authorize]
    public class CollectionsController : Controller
    {
        protected ICollectionService _collectionService;
        protected IBlobStorageService _blobStorageService;

        public CollectionsController(ICollectionService collectionService, IBlobStorageService blobStorageService)
        {
            _collectionService = collectionService;
            _blobStorageService = blobStorageService;
        }

        public async Task<IActionResult> Index()
        {
            var collections = await _collectionService.GetAllAsync();
            var hostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
            foreach (var collection in collections)
            {
                collection.ImageUrl = hostUrl + collection.ImageUrl;
            }
            return View(collections);
        }

        [HttpGet("/collections/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/collections/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CollectionDto collectionDto, IFormFile image)
        {
            //Save collection's data
            var collection = await _collectionService.CreateAsync(collectionDto);

            //Save collectio's image
            if (image != null)
            {
                await _blobStorageService.SaveResourceBlobAsync(
                    new ResourceBlobDto
                    {
                        Id = collection.Id,
                        Name = collection.Name,
                        Type = Enums.Resource.CollectionImage,
                        Blob = image.OpenReadStream()
                    });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/collections/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _collectionService.GetByIdAsync(id));
        }

        [HttpPost("/collections/update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CollectionDto collectionDto, IFormFile image)
        {
            //updates collection's data
            var collection = await _collectionService.UpdateAsync(collectionDto);

            if (image != null)
            {
                //updates collection's image
                await _blobStorageService.UpdateResourceBlobAsync(
                    new ResourceBlobDto
                    {
                        Id = collection.Id,
                        Name = collection.Name,
                        Type = Enums.Resource.CollectionImage,
                        Blob = image.OpenReadStream()
                    });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
