using Application.Authentication.Register.Command;
using Application.Common.Models;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers.Account
{
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }

#if DEBUG

        [HttpPost]
        public async ValueTask<Result> Register([FromBody] RegisterCommand command, CancellationToken ct = default)
            => await _mediator.Send(command, ct);

#endif
    }
}
