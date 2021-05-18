using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.Domain
{
    public class ExploraFile : Entity<int>
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public string ScientificName { get; set; }

        public string Extension { get; set; }

        public int Version { get; set; }

        public int Platform { get; set; }

        public string Description { get; set; }

        public bool Deleted { get; set; }

        public int? CollectionId { get; set; }

        public ExploraCollection Collection { get; set; }

        public string ImageUrl { get; set; }
    }
}
