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
    [Route("api/totems")]
    [ApiController]
    public class TotemsController : Controller
    {
        protected readonly IBlobStorageService _blobService;

        public TotemsController(IBlobStorageService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("{id}/image-data")]
        [ProducesResponseType(typeof(Stream), StatusCodes.Status200OK)]
        public IActionResult GetImageStream(int id)
        {
            var resource = _blobService.GetResourceByKey(new ResourceKeyDto { Id = id, Type = Resource.TotemImage });
            return Ok(resource != null ? resource.Blob : null);
        }

        [HttpGet("{id}/file-data")]
        [ProducesResponseType(typeof(Stream), StatusCodes.Status200OK)]
        public IActionResult GetFileStream(int id)
        {
            var resource = _blobService.GetResourceByKey(new ResourceKeyDto { Id = id, Type = Resource.TotemBlob });
            return Ok(resource != null ? resource.Blob : null);
        }
    }
}
