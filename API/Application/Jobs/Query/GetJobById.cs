using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Jobs.Mappers;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Query;

public class GetJobByIdQuery : IRequest<Result<JobDetailDto>>
{
	public Guid Id { get; set; }
	public Guid CompanyId { get; set; }

}

public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Result<JobDetailDto>>
{
    private readonly IDbContext _db;

    public GetJobByIdQueryHandler(IDbContext db)
    {
        _db = db;
    }

    public async ValueTask<Result<JobDetailDto>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // init query
            var query = await _db.Jobs.Where(x => x.CompanyId == request.CompanyId && x.Id == request.Id)
                                      .Select(x => JobMapper.MapToDetailDto(x))
                                      .FirstOrDefaultAsync();

            // return model
            return new Result<JobDetailDto>
            {
                Model = query,
                Error = query == null ? "Could not find job" : null
            };
        }
        catch (Exception e)
        {
            return new Result<JobDetailDto>
            {
                Error = e.InnerException?.Message ?? e.Message
            };
        }
    }
}
