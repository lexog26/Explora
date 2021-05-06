using AutoMapper;
using Explora.BusinessLogic.Services.Interfaces;
using Explora.DataLayer.Repository.Interfaces;
using Explora.DataLayer.UnitOfWork;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Domain;
using Explora.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Explora.BusinessLogic.Services
{
    /// <summary>
    /// Handles blobs persistance
    /// </summary>
    public class ExploraFileService : TransactionalService, IExploraFileService
    {
        private readonly IBlobStorageService _blobService;

        public ExploraFileService(IRepository repository,
                                  IBlobStorageService directoryBlobStorageService,
                                  IMapper mapper,
                                  IUnitOfWork unitOfWork) : base(mapper, unitOfWork, repository)
        {
            _blobService = directoryBlobStorageService;
        }

        public async Task<FileDto> CreateAsync(FileDto fileDto, Stream blobData, Stream imageData)
        {
            var entityFile = _mapper.Map<FileDto, ExploraFile>(fileDto);
            _repository.Create<ExploraFile, int>(entityFile);
            await SaveChangesAsync();

            ///Save file blob
            await _blobService.SaveResourceBlobAsync(
                new ResourceBlobDto
                {
                    Id = entityFile.Id,
                    Name = entityFile.Name,
                    Type = Resource.FileBlob,
                    Blob = blobData
                });

            //Save file image
            await _blobService.SaveResourceBlobAsync(
                new ResourceBlobDto
                {
                    Id = entityFile.Id,
                    Name = entityFile.Name,
                    Type = Resource.FileImage,
                    Blob = imageData
                });


            entityFile.Url = GenerateUrl(entityFile.Id);
            entityFile.ImageUrl = $"api/files/{entityFile.Id}/image-data";
            await SaveChangesAsync();
            return _mapper.Map<FileDto>(entityFile);
        }

        public async Task<FileDto> UpdateFileDataAsync(FileDto fileDto, Stream blobData, Stream imageData)
        {
            var file = await _repository.GetEntityByIdAsync<ExploraFile, int>(fileDto.Id);
            file.Description = fileDto.Description;
            file.Name = fileDto.Name;
            file.ModifiedDate = DateTime.UtcNow;

            if (blobData != null)
            {
                file.Version = file.Version + 1;
                //Save new file data
                await _blobService.UpdateResourceBlobAsync(
                        new ResourceBlobDto
                        {
                            Id = file.Id,
                            Blob = blobData,
                            Name = file.Name,
                            Type = Resource.FileBlob
                        }
                    );
            }
            
            //Save new changes to db
            _repository.Update(file);
            await SaveChangesAsync();

            //Save new image
            if (imageData != null)
            {
                await _blobService.UpdateResourceBlobAsync(
                    new ResourceBlobDto
                    {
                        Id = file.Id,
                        Blob = imageData,
                        Name = file.Name,
                        Type = Resource.FileImage
                    });
            }

            return _mapper.Map<FileDto>(file);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exploraFile = await _repository.GetEntityByIdAsync<ExploraFile, int>(id);
            //soft-delete in db
            exploraFile.Deleted = true;
            exploraFile.ModifiedDate = DateTime.UtcNow;
            _repository.Update(exploraFile);
            await SaveChangesAsync();
            //_repository.Delete(exploraFile);
            /////////////////////////

            //remove file's blob
            _blobService.DeleteResource(new ResourceKeyDto { Id = id, Type = Resource.FileBlob});

            //remove file's image
            _blobService.DeleteResource(new ResourceKeyDto { Id = id, Type = Resource.FileImage });
            
            return true;
        }

        public async Task<FileDto> GetByIdAsync(int id)
        {
            var exploraFile = await _repository.GetEntityByIdAsync<ExploraFile, int>(id);
            return _mapper.Map<ExploraFile, FileDto>(exploraFile);
        }

        public Stream GetFileBlob(int id)
        {
            var resource = _blobService.GetResourceByKey(new ResourceKeyDto { Id = id, Type = Resource.FileBlob });
            return resource.Blob;
        }

        public Stream GetFileImage(int id)
        {
            return _blobService.GetResourceByKey(new ResourceKeyDto { Id = id, Type = Resource.FileImage }).Blob;
        }

        public async Task<IEnumerable<FileDto>> GetAllAsync(bool deleted = false)
        {
            var files = await _repository.GetByFilterAsync<ExploraFile>(x => x.Deleted == deleted);
            return _mapper.Map<IEnumerable<ExploraFile>, IEnumerable<FileDto>>(files);
        }

        public async Task<IEnumerable<FileDto>> GetAllByLastTimeStampAsync(DateTime lastTimeStamp)
        {
            var files = await _repository.GetByFilterAsync<ExploraFile>(x => x.LastTimeStamp > lastTimeStamp);
            return _mapper.Map<IEnumerable<ExploraFile>, IEnumerable<FileDto>>(files);
        }

        public async Task<IEnumerable<FileDto>> GetAllByPlatformStamp(DateTime lastTimeStamp, Platform platform)
        {
            var files = await _repository.GetByFilterAsync<ExploraFile>(
                x => x.LastTimeStamp > lastTimeStamp && x.Platform == (int)platform);
            return _mapper.Map<IEnumerable<ExploraFile>, IEnumerable<FileDto>>(files);
        }

        private string GenerateUrl(int id)
        {
            return $"api/files/{id}/file-data";
        }

    }
}
