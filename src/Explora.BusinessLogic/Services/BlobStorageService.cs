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
        private const string _blobsFolderName = "blobs";
        private const string _imagesBaseFolderName = "images";
        private const string _fileImagesFolderName = "files";
        private const string _collectionImagesFolderName = "collections";
        protected Dictionary<Resource, string> _resourceBasePaths;

        /// <summary>
        /// Iniitalizes a DirectoryStorageService using a basePath
        /// </summary>
        /// <param name="basePath">Ex: base_path/wwwroot</param>
        public BlobStorageService(string basePath)
        {
            _basePath = Path.Combine(basePath, _baseFolderName);

            var fileImagesPath = Path.Combine(_basePath, _imagesBaseFolderName, _fileImagesFolderName);
            var collectionImagesPath = Path.Combine(_basePath, _imagesBaseFolderName, _collectionImagesFolderName);
            var fileBlobsPath = Path.Combine(_basePath, _blobsFolderName);

            _resourceBasePaths = new Dictionary<Resource, string>();
            _resourceBasePaths.Add(Resource.FileImage, fileImagesPath);
            _resourceBasePaths.Add(Resource.CollectionImage, collectionImagesPath);
            _resourceBasePaths.Add(Resource.FileBlob, fileBlobsPath);

            //Create directories
            Directory.CreateDirectory(_basePath);
            Directory.CreateDirectory(fileImagesPath);
            Directory.CreateDirectory(collectionImagesPath);
            Directory.CreateDirectory(fileBlobsPath);
        }

        public async Task<bool> SaveResourceBlobAsync(ResourceBlobDto blobDto)
        {
            var resourceDirPath = GetResouceDirectoryPath(blobDto.Key);
            Directory.CreateDirectory(resourceDirPath);
            var path = Path.Combine(resourceDirPath, blobDto.Name);
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                await blobDto.Blob.CopyToAsync(stream);
            }
            return true;
        }

        public async Task<bool> UpdateResourceBlobAsync(ResourceBlobDto resourceDto)
        {
            DeleteResource(new ResourceKeyDto { Id = resourceDto.Id, Type = resourceDto.Type });
            return await SaveResourceBlobAsync(resourceDto);
        }

        public bool DeleteResource(ResourceKeyDto resourceKey)
        {
            var directory = GetResourceDirectoryInfo(resourceKey);

            if (directory != null)
            {
                directory.Delete(true);
                return true;
            }
            return false;
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
            return Path.Combine(_resourceBasePaths[resourceKey.Type], resourceKey.Id.ToString());
        }

        protected DirectoryInfo GetResourceDirectoryInfo(ResourceKeyDto resourceKey)
        {
            var dir = new DirectoryInfo(GetResouceDirectoryPath(resourceKey));
            return dir.Exists ? dir : null;
        }

        protected FileInfo GetResourceFileInfo(ResourceKeyDto resourceKey)
        {
            var resourceDirectory = GetResourceDirectoryInfo(resourceKey);
            return resourceDirectory != null ? resourceDirectory.GetFiles().FirstOrDefault() : null;
        }

        
    }
}
