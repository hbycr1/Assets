using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Command;

public class CreateJobValidator : AbstractValidator<CreateJobCommand>
{
    private readonly IDbContext _db;

    public CreateJobValidator(IDbContext db)
    {
        _db = db;

        RuleFor(x => x.PONumber)
            .MustAsync(IsUniquePONumer)
            .When(x => !string.IsNullOrEmpty(x.PONumber))
            .WithMessage("A job with this PO Number already exists!");

        RuleFor(x => x.Description).NotNull().NotEmpty();
        RuleFor(x => x.Start).NotNull().NotEmpty();
        RuleFor(x => x.End).NotNull().NotEmpty();
        RuleFor(x => x.Address).NotNull().NotEmpty();
    }

    private async Task<bool> IsUniquePONumer(CreateJobCommand command, string po, CancellationToken token)
    {
        return !await _db.Jobs.AnyAsync(x => x.PONumber.ToLower() == po.ToLower() && command.CompanyId == x.CompanyId, token);
    }
}
