using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.DataTransferObjects.DataTransferObjects
{
    public class TotemDto : BaseDto
    {
        public int Id { get; set; }

        public string FileUrl { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}
