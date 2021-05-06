using Explora.BusinessLogic.Services.Interfaces;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Explora.Web.Controllers.Api
{
    [Route("api/collections")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        protected readonly IBlobStorageService _blobService;

        public CollectionsController(IBlobStorageService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("{id}/image-data")]
        [ProducesResponseType(typeof(Stream), StatusCodes.Status200OK)]
        public IActionResult GetImageStream(int id)
        {
            var resource = _blobService.GetResourceByKey(new ResourceKeyDto { Id = id, Type = Resource.CollectionImage });
            return Ok(resource != null ? resource.Blob : null);
        }
    }
}
