using Explora.DataTransferObjects;
using System.Threading.Tasks;

namespace Explora.BusinessLogic.Interfaces
{
    /// <summary>
    /// Services that persists data with a transactional flow
    /// </summary>
    public interface ITransactionalService : IServiceBase
    {
        void SaveChanges();

        Task SaveChangesAsync();
    }
}
