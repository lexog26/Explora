using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.Domain
{
    public class ExploraCollection : Entity<int>
    { 
        public string Name { get; set; }

        public string Description { get; set; }
        
        public string ImageUrl { get; set; }

        public IEnumerable<ExploraFile> Files { get; set; }
    }
}
