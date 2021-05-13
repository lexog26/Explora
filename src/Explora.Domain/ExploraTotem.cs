using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.Domain
{
    public class ExploraTotem :Entity<int>
    {
        public string Name { get; set; }

        public int Version { get; set; }

        public int Platform { get; set; }

        public string Description { get; set; }

        public bool Deleted { get; set; }

        public string ImageUrl { get; set; }

        public string FileUrl { get; set; }
    }
}
