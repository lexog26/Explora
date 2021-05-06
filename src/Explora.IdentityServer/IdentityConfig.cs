using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explora.IdentityServer
{
    public static class IdentityConfig
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

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
                        new Secret("filesSecret".Sha256())
                    },
                    //Scopes that client has access to
                    AllowedScopes = { "filesApi" }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "filesApi" }
                },
                //OpenId connect hybrid flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireConsent = false,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:5000/signin-oidc" },
                    //FrontChannelLogoutUri = "http://localhost:5000/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5000/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "filesApi"
                    }
                    //AllowedScopes = { "openid", "profile", "api1" }
                },
                // JS client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JS Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "http://localhost:5000/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5000/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5000" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "filesApi"
                    }

                }

            };
    }
}
