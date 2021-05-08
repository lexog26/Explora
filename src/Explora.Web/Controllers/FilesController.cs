using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Explora.BusinessLogic.Services.Interfaces;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
using Explora.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explora.Web.Controllers
{
    [Route("/files")]
    [Authorize]
    public class FilesController : Controller
    {
        protected readonly IExploraFileService _fileService;
        protected ICollectionService _collectionService;

        public FilesController(IExploraFileService fileService, ICollectionService collectionService)
        {
            _fileService = fileService;
            _collectionService = collectionService;
        }

        // GET: files
        public async Task<IActionResult> Index()
        {
            var files = await _fileService.GetAllAsync();
            return View(await GenerateViewFiles(files));
        }

        [HttpGet("{id}")]
        // GET: files/5
        public async Task<IActionResult> Details(int id)
        {
            var file = await _fileService.GetByIdAsync(id);
            return View(file);
        }

        [HttpGet("/files/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var file = await _fileService.GetByIdAsync(id);
            var collectionName = file.CollectionId != null ? (await _collectionService.GetByIdAsync(file.CollectionId.Value)).Name
                                                           : null;
            return View(
                new FileEditViewModel
                {
                    Id = file.Id,
                    Name = file.Name,
                    Collection = collectionName,
                    Description = file.Description,
                    Platform = file.Platform
                });
        }

        [HttpGet("/files/create")]
        public async Task<IActionResult> Create()
        {
            var names = new List<string>(await _collectionService.GetCollectionNamesAsync());
            return View(names);
        }

        // POST: files/create
        [HttpPost("/files/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, IFormFile androidFile, IFormFile iosFile, string description,
                                                IFormFile image, string collection)
        {
            try
            {
                int? collectionId = null;
                if (!string.IsNullOrEmpty(collection))
                {
                    collectionId = (await _collectionService.GetCollectionsByNameAsync(collection)).First().Id;

                    var androidDto = new FileDto
                    {
                        Name = name,
                        Description = description,
                        CollectionId = collectionId,
                        Platform = Platform.Android
                    };
                    //Create Android file
                    await _fileService.CreateAsync(androidDto, androidFile.OpenReadStream(), image.OpenReadStream());

                    //Create IOs file
                    var iosDto = new FileDto
                    {
                        Name = name,
                        Description = description,
                        CollectionId = collectionId,
                        Platform = Platform.IOs
                    };
                    await _fileService.CreateAsync(iosDto, iosFile.OpenReadStream(), image.OpenReadStream());
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST: files/update/5
        // POST: files/5
        [HttpPost("/files/update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, string name, IFormFile file, IFormFile image, string description)
        {
            try
            {
                await _fileService.UpdateFileDataAsync(
                    new FileDto { Id = id, Description = description, Name = name},
                    file != null ? file.OpenReadStream() : null,
                    image != null ? image.OpenReadStream() : null);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fileService.DeleteAsync(id);
            return Ok(true);
        }

        protected async Task<List<FileListModel>> GenerateViewFiles(IEnumerable<FileDto> fileDtos)
        {
            var hostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
            var files = new List<FileListModel>();
            foreach (var item in fileDtos)
            {
                var collection = item.CollectionId != null ? await _collectionService.GetByIdAsync(item.CollectionId.Value)
                                                           : null;
                files.Add(new FileListModel
                {
                    Id = item.Id,
                    ImageUrl = hostUrl + item.ImageUrl,
                    Name = item.Name,
                    Platform = item.Platform,
                    Collection = collection != null ? collection.Name : null
                });
            }
            return files;
        }

    }
}