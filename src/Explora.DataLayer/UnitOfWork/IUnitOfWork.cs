using System;
using System.Threading.Tasks;

namespace Explora.DataLayer.UnitOfWork
{
    /// <summary>
    /// Unit of Work dessign pattern definition
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>Task with the number of objects in an Added, Modified, or Deleted state</returns>
        Task<int> CommitAsync();
    }
}
