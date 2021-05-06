using Explora.DataTransferObjects.DataTransferObjects;
using Explora.Synchronization.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Explora.Synchronization
{
    public class ExploraApplicationDataManager : IExploraApplicationDataManager
    {
        protected DateTime _lastSynchronization = DateTime.MinValue;
        protected string _applicationRootFolder;

        // Where blobs will be saved
        protected string _blobsFolderName = "files";
        protected string _blobsFolderPath;

        // Where lastSynchronization date will be saved
        protected const string _fileName = "explora_data.txt";
        protected string _filePath;

        public ExploraApplicationDataManager(string applicationRootPath)
        {
            _filePath = Path.Combine(applicationRootPath, _fileName);
            _blobsFolderPath = Path.Combine(applicationRootPath, _blobsFolderName);
        }

        public ExploraApplicationDataManager()
        {
            _filePath = _fileName;
            _blobsFolderPath = _blobsFolderName;
        }

        /// <summary>
        /// Application data path
        /// </summary>
        public string ApplicationRootFolder
        {
            get
            {
                return _applicationRootFolder;
            }
            set
            {
                _applicationRootFolder = value;
                _filePath = Path.Combine(_applicationRootFolder, _fileName);
                _blobsFolderPath = Path.Combine(_applicationRootFolder, _blobsFolderName);
            }
        }


        public DateTime LastSynchronization 
        {
            get
            {
                if (_lastSynchronization == DateTime.MinValue && File.Exists(_filePath))
                {
                    _lastSynchronization = DateTime.Parse(File.ReadAllText(_filePath));
                }
                return _lastSynchronization;
            }
            set
            {
                _lastSynchronization = value;
            }
        }

        public void SaveBlobData(BlobDto dto)
        {
            Directory.CreateDirectory(_blobsFolderPath);
            string filePath = Path.Combine(_blobsFolderPath, dto.FullName);
            var file = new FileStream(filePath, FileMode.OpenOrCreate);
            dto.Data.CopyTo(file);
            file.Close();
        }

        public void SaveLastSynchronization(DateTime lastSynchronization)
        {
            File.WriteAllText(_filePath, lastSynchronization.ToString());
        }

    }
}
