using Explora.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Explora.DataTransferObjects.DataTransferObjects
{
    /// <summary>
    /// Files base class
    /// </summary>
    public class FileDto : BaseDto
    {
        /// <summary>
        /// Creates a FileDto using full name (name + extension)
        /// </summary>
        /// <param name="fullName"></param>
        /*public FileDto(string fullName)
        {
            var split = fullName.Split('.');
            Name = split[0];
            if(split.Length > 1)
            {
                Extension = split[split.Length - 1];
                for (int i = 1; i < split.Length - 1; i++)
                {
                    Name = Name + "." + split[i];
                }
            }
            
        }*/

        public FileDto()
        { }

        public int Id { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public DateTime LastTimestamp { get; set; }

        public int Version { get; set; }

        public Platform Platform { get; set; }

        /// <summary>
        /// File Name + File Extension
        /// </summary>
        public string FullName
        {
            get
            {
                return string.IsNullOrEmpty(Extension) ? Name : Name + "." + Extension;
            }
        }

        public string Description { get; set; }

        public bool Deleted { get; set; }

        public int? CollectionId { get; set; }
    }
}
