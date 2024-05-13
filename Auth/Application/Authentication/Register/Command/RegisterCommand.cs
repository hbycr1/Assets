using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Domain.Entities;
using Domain.Enums;
using Mediator;

namespace Application.Authentication.Register.Command;

public sealed record RegisterCommand : ICommand<Result>
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;

	public string CompanyName { get; set; } = string.Empty;
	public string CompanyEmail { get; set; } = string.Empty;
}

public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result>
{
	private readonly IDbContext _db;

	public RegisterCommandHandler(IDbContext db) { _db=db; }

	public async ValueTask<Result> Handle(RegisterCommand command, CancellationToken ct)
	{
		if (command != null)
		{
			var validator = await new RegisterValidator().ValidateAsync(command, ct);
			if (validator != null && validator.IsValid)
			{
				// Define company
				Company company = new()
				{
					Id = Guid.NewGuid(),
					Name = command.CompanyName,
					Email = command.CompanyEmail,
					Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    Status = CompanyStatus.Active,
					Users = new List<User>
					{
                        new()
						{
							Id = Guid.NewGuid(),
							FirstName = command.FirstName,
							LastName = command.LastName,
							Email = command.Email,
							PasswordHash = PasswordHash.CreateHash(command.Password, out string saltyboi),
							Salt = saltyboi,
							Created = DateTime.UtcNow,
							Updated = DateTime.UtcNow,
							Status = UserStatus.Inactive
						}
                    }
				};

				_db.Company.Add(company);

				// Save
				await _db.SaveChangesAsync(ct);
			}

			return new()
			{
				Errors =
				validator?.Errors?.Select(x => x.ErrorMessage)?.Distinct() ??
				new string[] { "Your username or password is invalid" }
			};
		}

		return new() { Error = "Your username or password is invalid" };
	}
}
