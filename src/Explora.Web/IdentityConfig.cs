using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explora.Web
{
    public static class IdentityConfig
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("filesApi", "Files")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "filesClient",
                    //clientId/clientSecret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    //Scopes that client has access to
                    AllowedScopes = { "filesApi" }
                }
            };
    }
}
