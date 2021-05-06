using Explora.DataTransferObjects.DataTransferObjects;
using System;

namespace Explora.Synchronization.Interfaces
{
    /// <summary>
    /// Manages data containing in app device
    /// </summary>
    public interface IExploraApplicationDataManager
    {
        /// <summary>
        /// Last synchronization mark using
        /// </summary>
        DateTime LastSynchronization { get; set; }

        /// <summary>
        /// Saves stream blob's data
        /// </summary>
        /// <param name="dto"></param>
        void SaveBlobData(BlobDto dto);

        void SaveLastSynchronization(DateTime lastSynchronization);
    }
}
