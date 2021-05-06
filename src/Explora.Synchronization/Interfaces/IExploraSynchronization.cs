using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
using System.Collections.Generic;

namespace Explora.Synchronization.Interfaces
{
    public interface IExploraSynchronization
    {
        /// <summary>
        /// Returns new files
        /// </summary>
        /// <returns></returns>
        IEnumerable<FileDto> StartSynchronization(Platform platform);
    }
}
