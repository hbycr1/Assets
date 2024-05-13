using Application.Common.Models;
using Application.Session;
using Application.Session.Query;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.User;

public class SessionController : ControllerBase
{
	private readonly IMediator _mediator;

	public SessionController(IMediator mediator)
	{
		_mediator=mediator;
	}

	[HttpGet]
	public async Task<Result<UserSessionDto>> Get()
	{
		return await _mediator.Send(new GetUserSession { UserId = GetCurrentUserId() }); ;
	}
}
