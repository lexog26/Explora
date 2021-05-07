using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explora.Web.Configurations
{
    public class SmtpConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public string From { get; set; }

        public string Alias { get; set; }
    }
}
