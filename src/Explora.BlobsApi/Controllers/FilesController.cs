using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Explora.BusinessLogic.Services.Interfaces;
using Explora.DataTransferObjects.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explora.BlobsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        protected readonly IExploraFileService _fileService;

        public FilesController(IExploraFileService fileService)
        {
            _fileService = fileService;
        }

        // GET: api/files
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FileDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilesAsync()
        {
            var files = await _fileService.GetAllAsync();
            return Ok(files);
        }

        // GET: api/files/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FileDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Details(int id)
        {
            var file = await _fileService.GetByIdAsync(id);
            return Ok(file);
        }

        // GET: api/files/{id}/file-data
        [HttpGet("{id}/file-data")]
        [ProducesResponseType(typeof(Stream), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFileStream(int id)
        {
            var stream = await _fileService.GetFileBlobAsync(id);
            return Ok(stream);
        }

        #region Old-Generated-Code

        // GET api/files
        /*[HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/files/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
        #endregion
    }
}
