using Explora.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explora.Web.Models
{
    public class FileListModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Platform Platform { get; set; }

        public string ImageUrl { get; set; }

        public string Collection { get; set; }
    }
}
