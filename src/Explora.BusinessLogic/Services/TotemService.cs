using AutoMapper;
using Explora.BusinessLogic.Services.Interfaces;
using Explora.DataLayer.Repository.Interfaces;
using Explora.DataLayer.UnitOfWork;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Explora.BusinessLogic.Services
{
    public class TotemService : TransactionalService, ITotemService
    {
        public TotemService(IRepository repository,
                            IMapper mapper,
                            IUnitOfWork unitOfWork) : base(mapper, unitOfWork, repository)
        {
        }

        public async Task<TotemDto> CreateAsync(TotemDto totemDto)
        {
            var entity = _mapper.Map<TotemDto, ExploraTotem>(totemDto);
            _repository.Create<ExploraTotem, int>(entity);
            await SaveChangesAsync();
            entity.ImageUrl = $"api/totems/{entity.Id}/image-data";
            entity.FileUrl = $"api/totems/{entity.Id}/file-data";
            await SaveChangesAsync();
            return _mapper.Map<TotemDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _repository.Delete<ExploraTotem, int>(id);
            await SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TotemDto>> GetAllAsync(int limit = int.MaxValue)
        {
            return _mapper.Map<IEnumerable<TotemDto>>(
                await _repository.GetAllAsync<ExploraTotem>(take: limit));
        }

        public async Task<TotemDto> GetByIdAsync(int id)
        {
            return _mapper.Map<TotemDto>(await _repository.GetEntityByIdAsync<ExploraTotem, int>(id));
        }

        public async Task<TotemDto> UpdateAsync(TotemDto totemDto)
        {
            var totem = await _repository.GetEntityByIdAsync<ExploraTotem, int>(totemDto.Id);
            if (totem != null)
            {
                totem.Name = totemDto.Name;
                totem.Description = totemDto.Description;
                totem.ModifiedDate = DateTime.UtcNow;
                _repository.Update(totem);
                await SaveChangesAsync();
                return _mapper.Map<TotemDto>(totem);
            }
            return default(TotemDto);
        }
    }
}
