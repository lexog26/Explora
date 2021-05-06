using Explora.ApiClient.Interfaces;
using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Enums;
using Explora.Synchronization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Explora.Synchronization
{
    /// <summary>
    /// Explora files synchronization manager
    /// </summary>
    public class ExploraSynchronization : IExploraSynchronization
    {
        protected IExploraApplicationDataManager _appDataManager;
        protected IExploraClient _exploraClient;
        protected DateTime _lastSynchronization;

        public ExploraSynchronization(IExploraApplicationDataManager appDataManager, IExploraClient exploraClient)
        {
            _appDataManager = appDataManager;
            _exploraClient = exploraClient;
            _lastSynchronization = _appDataManager.LastSynchronization;
        }

        public ExploraSynchronization()
        { }

        public IExploraApplicationDataManager ApplicationDataManager
        {
            get
            {
                return _appDataManager;
            }
            set
            {
                _appDataManager = value;
            }
        }

        public IExploraClient ExploraClient
        {
            get
            {
                return _exploraClient;
            }
            set
            {
                _exploraClient = value;
            }
        }

        /// <summary>
        /// Starts app synchronization process
        /// </summary>
        /// <returns>Files downloaded</returns>
        public IEnumerable<FileDto> StartSynchronization(Platform platform)
        {
            IEnumerable<FileDto> files = null;
            while(files == null)
            {
                Thread.Sleep(5000);
                files = Synchronization(platform);
            }
            return files;
        }

        private IEnumerable<FileDto> Synchronization(Platform platform)
        {
            try
            {
                var files = _exploraClient.GetFiles(platform, _lastSynchronization); 
                if (files.Count() > 0)
                {
                    var sortedSet = new SortedSet<DateTime>(files.Select(x => x.LastTimestamp));
                    _appDataManager.SaveLastSynchronization(sortedSet.Max);
                }
                else
                {
                    files = null;
                }
                return files;
            }
            catch (Exception e) { return null; }
        }
    }
}
