using Duende.IdentityServer.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Identity
{
    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() => new[]
        {
            new IdentityResources.OpenId()
        };

        public static IEnumerable<ApiResource> GetApiResources(IConfiguration config) => new[]
        {
            new ApiResource(config["Clients:API:Id"]!)
            {
                Scopes = new[] { config["Clients:API:Id"]! },
                AllowedAccessTokenSigningAlgorithms = new[] { "ES256" }
            }
        };

        public static IEnumerable<ApiScope> GetApiScopes(IConfiguration config) => new[]
        {
            new ApiScope(config["Clients:API:Id"]!, "API")
        };
    }
}
