using Application.Common.Constants;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Login.Command
{
	public class LoginValidator : AbstractValidator<LoginCommand>
	{
		private readonly IDbContext _db;

		public LoginValidator(IDbContext db)
		{
			_db=db;

			RuleFor(x => x.ReturnUrl).NotEmpty();
			RuleFor(x => x.UsernameOrEmail).NotEmpty().MustAsync(BeValidUsername).WithMessage(AccountOptions.InvalidCredentialsErrorMessage);
			RuleFor(x => x.Password).NotEmpty().MustAsync(BeValidPassword).WithMessage(AccountOptions.InvalidCredentialsErrorMessage);
		}

		private async Task<bool> BeValidPassword(LoginCommand command, string password, CancellationToken ct)
		{
			// build query
			var query = _db.User.Where(x => x.Status == (int)UserStatus.Active);

			// check email
			if (command.UsernameOrEmail?.Contains('@') == true)
				query = query.Where(x => x.Email == command.UsernameOrEmail);
			// check username
			else
				query = query.Where(x => x.Username == command.UsernameOrEmail);

			var user = await query.Select(x => new { x.PasswordHash, x.Salt }).FirstOrDefaultAsync(ct);

			return user != null && PasswordHash.ValidatePassword(password, user.PasswordHash, user.Salt);
		}

		private async Task<bool> BeValidUsername(string usernameOrEmail, CancellationToken ct)
		{
			// build query
			var query = _db.User.Where(x => x.Status == (int)UserStatus.Active);

			// check email
			if (usernameOrEmail?.Contains('@') == true)
				return await query.Where(x => x.Email == usernameOrEmail).AnyAsync(ct);

			// check username
			return await query.Where(x => x.Username == usernameOrEmail).AnyAsync(ct);
		}
	}
}
