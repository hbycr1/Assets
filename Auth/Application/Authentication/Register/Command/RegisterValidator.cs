using FluentValidation;

namespace Application.Authentication.Register.Command;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
	public RegisterValidator()
	{
		RuleFor(x => x.FirstName).NotEmpty();

		RuleFor(x => x.LastName).NotEmpty();

		RuleFor(x => x.Email).NotEmpty().EmailAddress();

		RuleFor(x => x.CompanyName).NotEmpty();

		RuleFor(x => x.CompanyEmail).NotEmpty();

		RuleFor(x => x.Password)
			.NotEmpty()
			.MinimumLength(4)
			.MaximumLength(255)
			.Matches("[A-z0-9]");
	}
}
