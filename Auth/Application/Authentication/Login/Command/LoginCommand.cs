using Application.Common.Constants;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Login.Command;

public sealed record LoginCommand : ICommand<Result>
{
	public string UsernameOrEmail { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;

	public bool? RememberMe { get; set; } = false;
	public string ReturnUrl { get; set; } = string.Empty;
}

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, Result>
{
	private readonly IDbContext _db;
	private readonly IIdentityServerInteractionService _interaction;
	private readonly IHttpContextAccessor _contextAccessor;

	public LoginCommandHandler(IDbContext db,
							   IIdentityServerInteractionService interaction,
							   IHttpContextAccessor contextAccessor)
	{
		_db=db;
		_interaction=interaction;
		_contextAccessor=contextAccessor;
	}

	public async ValueTask<Result> Handle(LoginCommand command, CancellationToken ct)
	{
		if (command != null)
		{
			// get contexts
			var authContext = await _interaction.GetAuthorizationContextAsync(command.ReturnUrl);
			var context = _contextAccessor.HttpContext;
			if (context != null && authContext != null)
			{
				// validate
				var validator = await new LoginValidator(_db).ValidateAsync(command, ct);
				if (validator != null && validator.IsValid)
				{
					// builder user query
					IQueryable<User> userQuery = _db.User;
					if (command.UsernameOrEmail.Contains('@'))
						userQuery = userQuery.Where(x => x.Email == command.UsernameOrEmail);
					else
						userQuery = userQuery.Where(x => x.Username == command.UsernameOrEmail);

					// get user
					var user = await userQuery.Select(x => new
					{
						x.Id,
						x.Email,
						x.Role
					}).FirstAsync(ct);

					// build login props
					var loginProps = new AuthenticationProperties
					{
						IsPersistent = true,
						ExpiresUtc = DateTime.UtcNow.Add(
							command.RememberMe == true ?
							AccountOptions.RememberMeLoginDuration :
							AccountOptions.LoginDuration
						)
					};

					// log em in
					await context.SignInAsync(new IdentityServerUser($"{user.Id}")
					{
						DisplayName=user.Email
					}, loginProps);
				}

				return new() { Errors = validator?.Errors?.Select(x => x.ErrorMessage)?.Distinct() };
			}
		}

		return new() { Error = AccountOptions.InvalidCredentialsErrorMessage };
	}
}
