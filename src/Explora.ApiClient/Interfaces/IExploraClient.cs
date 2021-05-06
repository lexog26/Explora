using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.ApiClient.Interfaces
{
    public interface IExploraClient
    {
        IEnumerable<FileDto> GetFiles(Platform platform, DateTime lastTimeStamp);

        /// <summary>
        /// Downloads fileDto blob's data
        /// </summary>
        /// <param name="fileDto"></param>
        /// <returns></returns>
        BlobDto DownloadBlobsData(FileDto fileDto);
    }
}
