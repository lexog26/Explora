using AutoMapper;
using Explora.BusinessLogic.Services.Interfaces;
using Explora.DataLayer.Repository.Interfaces;
using Explora.DataLayer.UnitOfWork;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explora.BusinessLogic.Services
{
    public class CollectionService : TransactionalService, ICollectionService
    {
        public CollectionService(IRepository repository,
                                 IMapper mapper,
                                 IUnitOfWork unitOfWork) : base(mapper, unitOfWork, repository)
        {
        }

        public async Task<CollectionDto> CreateAsync(CollectionDto collectionDto)
        {
            var collection = _mapper.Map<CollectionDto, ExploraCollection>(collectionDto);
            _repository.Create<ExploraCollection, int>(collection);
            await SaveChangesAsync();
            collection.ImageUrl = GenerateImageUrl(collection.Id);
            await SaveChangesAsync();
            return _mapper.Map<CollectionDto>(collection);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _repository.Delete<ExploraCollection, int>(id);
            await SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CollectionDto>> GetAllAsync(int limit = int.MaxValue)
        {
            return _mapper.Map<IEnumerable<CollectionDto>>(
                await _repository.GetAllAsync<ExploraCollection>(take: limit));
        }

        public async Task<CollectionDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CollectionDto>(await _repository.GetEntityByIdAsync<ExploraCollection, int>(id));
        }

        public async Task<IEnumerable<string>> GetCollectionNamesAsync()
        {
            return (await GetAllAsync()).Select(x => x.Name);
        }

        public async Task<IEnumerable<CollectionDto>> GetCollectionsByNameAsync(string name)
        {
            return (_mapper.Map<IEnumerable<CollectionDto>>(await _repository.GetByFilterAsync<ExploraCollection>(
                filter: x => x.Name.Trim().ToUpper() == name.Trim().ToUpper()
                )));
        }

        private string GenerateImageUrl(int id)
        {
            return $"api/collections/{id}/image-data";
        }
    }
}
