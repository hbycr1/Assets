using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers.Account
{
    public class LogoutController : ControllerBase
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IConfiguration _config;

        public LogoutController(IIdentityServerInteractionService interaction,
                                IConfiguration config)
        {
            _interaction = interaction;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string logoutId)
        {
            return await Logout(logoutId);
        }

        [HttpPost]
        public async Task<IActionResult> Logout([FromQuery] string logoutId)
        {
            var logoutContext = await _interaction.GetLogoutContextAsync(logoutId);

            if (logoutContext != null &&
                User?.Identity?.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();

                string? logoutUrl = string.Empty;
                string? clientId = logoutContext.ClientIds?.FirstOrDefault();

                if (!string.IsNullOrEmpty(clientId))
                {
                    if (clientId == _config["Clients:WebClient:Id"]) logoutUrl = _config["Clients:WebClient:Uri"];

                    if (clientId == _config["Clients:MobileClient:Id"]) logoutUrl = _config["Clients:MobileClient:Uri"];
                }

                return Redirect(logoutUrl ?? "/account/login?returnUrl=/");
            }

            return NotFound();
        }
    }
}
