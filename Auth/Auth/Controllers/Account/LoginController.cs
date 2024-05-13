using Application.Authentication.Login.Command;
using Duende.IdentityServer.Services;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers.Account
{
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityServerInteractionService _interaction;

        public LoginController(IMediator mediator,
                               IIdentityServerInteractionService interaction)
        {
            _mediator = mediator;
            _interaction = interaction;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginCommand command, [FromQuery] string returnUrl, CancellationToken ct = default)
        {
            command.ReturnUrl = returnUrl;

            var login = await _mediator.Send(command, ct);
            if (!login.IsValid)
            {
                if (login.Errors?.Any() == true)
                    foreach (var error in login.Errors)
                        ModelState.TryAddModelError(string.Empty, error);

                return View("/Views/Auth/Login.cshtml", command);
            }

            return Redirect(returnUrl);
        }

        /// <summary>
        /// Identity Server login page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context != null)
                return View("/Views/Auth/Login.cshtml", new LoginCommand { ReturnUrl = returnUrl });

            return NotFound();
        }

        /// <summary>
        /// Identity Server error page
        /// </summary>
        [HttpGet("error")]
        public async Task<IActionResult> Error([FromQuery] string errorId)
        {
            if (!string.IsNullOrEmpty(errorId))
            {
                var error = await _interaction.GetErrorContextAsync(errorId);
                if (error != null)
                    return Ok(error);
            }

            return NotFound();
        }
    }
}
