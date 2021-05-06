using Explora.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Explora.DataTransferObjects.DataTransferObjects
{
    public class ResourceBlobDto
    {
        public Resource Type { get; set; }

        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public Stream Blob { get; set; }

        public ResourceKeyDto Key
        {
            get
            {
                return new ResourceKeyDto { Id = Id, Type = Type };
            }
        }
    }
}
