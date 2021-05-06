using Explora.DataTransferObjects.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Explora.BusinessLogic.Services.Interfaces
{
    public interface ICollectionService
    {
        Task<CollectionDto> CreateAsync(CollectionDto collectionDto);

        Task<CollectionDto> UpdateAsync(CollectionDto collectionDto);

        Task<CollectionDto> GetByIdAsync(int id);

        Task<IEnumerable<CollectionDto>> GetAllAsync(int limit = int.MaxValue);

        Task<IEnumerable<string>> GetCollectionNamesAsync();

        Task<IEnumerable<CollectionDto>> GetCollectionsByNameAsync(string name);

        Task<bool> DeleteAsync(int id);
    }
}
