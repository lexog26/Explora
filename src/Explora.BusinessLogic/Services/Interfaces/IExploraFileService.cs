using Explora.BusinessLogic.Interfaces;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Domain;
using Explora.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Explora.BusinessLogic.Services.Interfaces
{
    public interface IExploraFileService : IServiceBase
    {
        Task<FileDto> CreateAsync(FileDto fileDto);

        Task<FileDto> UpdateAsync(FileDto fileDto);

        Task<FileDto> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<FileDto>> GetAllAsync(bool deleted = false);

        Task<IEnumerable<FileDto>> GetAllByLastTimeStampAsync(DateTime lastTimeStamp);

        /// <summary>
        /// Returns files with lastimestamp > lastimestamp parameter in specific platform
        /// </summary>
        /// <param name="lastTimeStamp"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<IEnumerable<FileDto>> GetAllByPlatformStamp(DateTime lastTimeStamp, Platform platform);

        /// <summary>
        /// Returns file stream using the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Stream GetFileBlob(int id);

        /// <summary>
        /// Returns the file image stream
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Stream GetFileImage(int id);
    }
}
