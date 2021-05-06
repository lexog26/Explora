using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.ApiClient.Configurations
{
    /// <summary>
    /// Client id Configurations for request api access token
    /// </summary>
    public class ExploraApiClientConfig
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public ExploraApiClientConfig()
        {
            ClientId = "filesClient";
            ClientSecret = "filesSecret";
        }
    }
}
