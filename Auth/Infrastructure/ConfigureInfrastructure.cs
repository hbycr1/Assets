using Duende.IdentityServer;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureInfrastructure
{
    public static void ConfigureIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityServer(x =>
        {
            x.UserInteraction = new()
            {
                LoginUrl = "/account/login",
                LogoutUrl = "/account/logout",
                ErrorUrl = "/account/login/error",
                LoginReturnUrlParameter = "returnUrl"
            };

            x.KeyManagement.Enabled = false;
        })
        .AddClientStore<ClientStore>()
        .AddInMemoryIdentityResources(Resources.GetIdentityResources())
        .AddInMemoryApiResources(Resources.GetApiResources(builder.Configuration))
        .AddInMemoryApiScopes(Resources.GetApiScopes(builder.Configuration))
        .AddProfileService<IdentityServerProfileService>()
        .AddSigningCredential(SigningCredential.GetSigningCredentials(), IdentityServerConstants.ECDsaSigningAlgorithm.ES256);
    }
}
