using System;
using System.Collections.Generic;
using System.Text;

namespace Explora.ApiClient.Configurations
{
    /// <summary>
    /// Explora APi configurations
    /// </summary>
    public class ExploraApiConfig
    {
        public string ServerUrl { get; set; }

        public string GetAllPath { get; set; }

        public string IdentityUrl { get; set; }

        public string ApiScope { get; set; }

        public ExploraApiClientConfig ApiClient { get; set; }

        public ExploraApiConfig()
        {
            ApiScope = "filesApi";
            ApiClient = new ExploraApiClientConfig();
        }
    }
}
