using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Explora.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Blob's cloud storage services
    /// </summary>
    public interface IBlobStorageService
    {
        Task<bool> SaveResourceBlobAsync(ResourceBlobDto blobDto);

        Task<bool> UpdateResourceBlobAsync(ResourceBlobDto blobDto);

        bool DeleteResource(ResourceKeyDto resourceKey);

        ResourceBlobDto GetResourceByKey(ResourceKeyDto resourceKey);

    }
}
