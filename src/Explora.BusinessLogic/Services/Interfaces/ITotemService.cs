using Explora.DataTransferObjects.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Explora.BusinessLogic.Services.Interfaces
{
    public interface ITotemService
    {
        Task<TotemDto> CreateAsync(TotemDto totemDto);

        Task<TotemDto> UpdateAsync(TotemDto totemDto);

        Task<TotemDto> GetByIdAsync(int id);

        Task<IEnumerable<TotemDto>> GetAllAsync(int limit = int.MaxValue);

        Task<bool> DeleteAsync(int id);
    }
}
