using AutoMapper;
using Explora.BusinessLogic.Interfaces;
using Explora.DataLayer.Repository.Interfaces;
using Explora.DataLayer.UnitOfWork;
using Explora.DataTransferObjects;
using Explora.Domain;
using System.Threading.Tasks;

namespace Explora.BusinessLogic
{
    /// <summary>
    /// Groups all services with data persistances features based on Unit of Work design pattern
    /// </summary>
    public abstract class TransactionalService : ServiceBase, ITransactionalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionalService(IMapper mapper, IUnitOfWork unitOfWork, IRepository repository)
                                   : base(repository, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.CommitAsync();
        }
    }
}
