using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Explora.DataTransferObjects.DataTransferObjects
{
    public class BlobDto
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public string FullName
        {
            get
            {
                return string.IsNullOrEmpty(Extension) ? Name : Name + "." + Extension;
            }
        }

        public Stream Data { get; set; }
    }
}
