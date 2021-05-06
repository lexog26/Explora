using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.Domain
{
    public class User : Entity<int>
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string SecondLastName { get; set; }

        public string Email { get; set; }
    }
}
