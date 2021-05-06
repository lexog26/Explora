using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.DataTransferObjects.DataTransferObjects
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string SecondLastName { get; set; }

        public string Email { get; set; }
    }
}
