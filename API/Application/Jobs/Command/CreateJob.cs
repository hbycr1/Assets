using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Jobs.Mappers;
using Mediator;

namespace Application.Jobs.Command;

public class CreateJobCommand : IRequest<Result>
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? CustomerId { get; set; }

    public string PONumber { get; set; } = string.Empty;
    public Guid? JobType { get; set; }

    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result>
{
    private readonly IDbContext _db;

    public CreateJobCommandHandler(IDbContext db)
    {
        _db = db;
    }

    public async ValueTask<Result> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var validator = await new CreateJobValidator(_db).ValidateAsync(request, cancellationToken);
        if (validator?.IsValid == true)
        {
            var entity = request.MapToModel();

            _db.Jobs.Add(entity);

            await _db.SaveChangesAsync(cancellationToken);
        }

        return new Result { Error = validator?.GetError(), Errors = validator?.GetErrors() };
    }
}