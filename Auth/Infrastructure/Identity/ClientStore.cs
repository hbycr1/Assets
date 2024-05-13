using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Identity;

internal class ClientStore : IClientStore
{
	private readonly IConfiguration config;

	public ClientStore(IConfiguration config)
	{
		this.config = config;
	}

	public async Task<Client?> FindClientByIdAsync(string clientId)
	{
		// Web Client
		if (clientId == config["Clients:WebClient:Id"])
			return await Task.FromResult<Client>(new()
			{
				ClientId = config["Clients:WebClient:Id"],
				ClientName = "Web Client",
				ClientUri = config["Clients:WebClient:Uri"],

				AllowedGrantTypes = GrantTypes.Code,
				RequirePkce = true,
				RequireClientSecret = false,
				AllowAccessTokensViaBrowser = true,
				AccessTokenType = AccessTokenType.Jwt,

				AccessTokenLifetime = 86400, // 24 hrs
				IdentityTokenLifetime = 600, // 10 mins

				RedirectUris =
				{
					$"{config["Clients:WebClient:Uri"]}/signin-callback",
					$"{config["Clients:WebClient:Uri"]}/assets/silent.renew.html"
				},
				PostLogoutRedirectUris =
				{
					$"{config["Clients:WebClient:Uri"]}/signout-callback"
				},
				AllowedCorsOrigins =
				{
					config["Clients:WebClient:Uri"]
				},
				AllowedScopes =
				{
					IdentityServerConstants.StandardScopes.OpenId,
					config["Clients:API:Id"]
				}
			});

		// Mobile Client
		if (clientId == config["Clients:MobileClient:Id"])
			return await Task.FromResult<Client>(new()
			{
				ClientId = "native.code",
				ClientName = "Native Client (Code with PKCE)",
				RequireClientSecret = false,
				RedirectUris = { "com.anonymous.mobileclient:/oauthredirect" },
				AllowedGrantTypes = GrantTypes.Code,
				RequirePkce = true,
				AllowedScopes = { "openid", "profile" },
				AllowOfflineAccess = true
			});

		return null;
	}

}
