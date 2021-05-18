using Explora.BusinessLogic.Services.Interfaces;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Explora.BusinessLogic.Services
{
    /// <summary>
    /// Blob storage implementation based on base path
    /// using file system server structure
    /// </summary>
    public class BlobStorageService : IBlobStorageService
    {
        private readonly string _basePath;
        private const string _baseFolderName = "files";

        /// <summary>
        /// Iniitalizes a DirectoryStorageService using a basePath
        /// </summary>
        /// <param name="basePath">Ex: base_path/wwwroot</param>
        public BlobStorageService(string basePath)
        {
            _basePath = Path.Combine(basePath, _baseFolderName);
        }

        public async Task<bool> SaveResourceBlobAsync(ResourceBlobDto blobDto)
        {
            if (blobDto.Blob != null)
            {
                var directoryInfo = GetResourceDirectoryInfo(blobDto.Key);

                //Delete to avoid bad information
                if (directoryInfo.Exists)
                {
                    directoryInfo.Delete(true);
                }
                ///
                directoryInfo.Create();
                var path = Path.Combine(directoryInfo.FullName, blobDto.Name);
                using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await blobDto.Blob.CopyToAsync(stream);
                }
            }
            return blobDto.Blob != null;
        }

        public async Task<bool> UpdateResourceBlobAsync(ResourceBlobDto resourceDto)
        {
            return await SaveResourceBlobAsync(resourceDto);
        }

        public bool DeleteResource(ResourceKeyDto resourceKey)
        {
            var directory = GetResourceDirectoryInfo(resourceKey);

            if (directory.Exists)
            {
                directory.Delete(true);
            }
            return directory.Exists;
        }

        public ResourceBlobDto GetResourceByKey(ResourceKeyDto resourceKey)
        {
            var fileInfo = GetResourceFileInfo(resourceKey);
            return new ResourceBlobDto
            {
                Id = resourceKey.Id,
                Type = resourceKey.Type,
                Name = fileInfo != null ? fileInfo.Name : null,
                Blob = fileInfo != null ? fileInfo.OpenRead() : null
            };
        }

        protected string GetResouceDirectoryPath(ResourceKeyDto resourceKey)
        {
            return Path.Combine(Path.Combine(_basePath, resourceKey.Type.ToString().ToLower()), resourceKey.Id.ToString());
        }

        protected DirectoryInfo GetResourceDirectoryInfo(ResourceKeyDto resourceKey)
        {
            return new DirectoryInfo(GetResouceDirectoryPath(resourceKey));
        }

        protected FileInfo GetResourceFileInfo(ResourceKeyDto resourceKey)
        {
            var resourceDirectory = GetResourceDirectoryInfo(resourceKey);
            return resourceDirectory.Exists ? resourceDirectory.GetFiles().FirstOrDefault() : null;
        }

        
    }
}
