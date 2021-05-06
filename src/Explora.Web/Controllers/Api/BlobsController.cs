using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Explora.BusinessLogic.Services.Interfaces;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explora.Web.Controllers.Api
{
    [Route("api/files")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class BlobsController : ControllerBase
    {
        protected readonly IExploraFileService _fileService;

        public BlobsController(IExploraFileService fileService)
        {
            _fileService = fileService;
        }

        // GET: api/files
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FileDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilesAsync([FromQuery]DateTime lastTimeStamp)
        {
            var files = await _fileService.GetAllByLastTimeStampAsync(lastTimeStamp);
            return Ok(files);
        }

        // GET: api/files/platform/{platformType}?lastTimeStamp
        [HttpGet]
        [Route("platform/{platformType}")]
        [ProducesResponseType(typeof(IEnumerable<FileDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilesByFilterAsync(Platform platformType, [FromQuery]DateTime lastTimeStamp)
        {
            var files = await _fileService.GetAllByPlatformStamp(lastTimeStamp, platformType);
            return Ok(files);
        }

        // GET: api/files/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FileDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var file = await _fileService.GetByIdAsync(id);
            return Ok(file);
        }

        // GET: api/files/{id}/file-data
        [HttpGet("{id}/file-data")]
        [ProducesResponseType(typeof(Stream), StatusCodes.Status200OK)]
        public IActionResult GetFileStream(int id)
        {
            var stream = _fileService.GetFileBlob(id);
            return Ok(stream);
        }

        // GET: api/files/{id}/image-data
        [HttpGet("{id}/image-data")]
        [ProducesResponseType(typeof(Stream), StatusCodes.Status200OK)]
        public IActionResult GetImageStream(int id)
        {
            var stream = _fileService.GetFileImage(id);
            return Ok(stream);
        }

    }
}
