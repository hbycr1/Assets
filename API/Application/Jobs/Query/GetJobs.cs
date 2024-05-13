using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Jobs.Mappers;
using Domain.Enums;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Query;

public class GetJobsQuery : IRequest<ResultList<JobListDto>>
{
    public Guid CompanyId { get; set; }

    public int Page { get; set; }
    public int PageSize { get; set; }

    public string? Search { get; set; }
    public string? Sort { get; set; }

    public JobStatus[]? JobStatus { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, ResultList<JobListDto>>
{
    private readonly IDbContext _db;

    public GetJobsQueryHandler(IDbContext db)
    {
        _db = db;
    }

    public async ValueTask<ResultList<JobListDto>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // init query
            var query = _db.Jobs.Where(x => x.CompanyId == request.CompanyId);

            // date filter
            if (request.StartDate.HasValue &&
                request.EndDate.HasValue)
            {
                query = query.Where(x => (x.Start >= request.StartDate && x.Start <= request.EndDate) ||
                                         (x.End <= request.EndDate && x.End >= request.StartDate) ||
                                         (x.Start <= request.StartDate && x.End >= request.EndDate));
            }

            // search filter
            if (!string.IsNullOrEmpty(request.Search))
            {
                string search = request.Search.Trim(' ').TrimStart('#').ToLower();

                query = query.Where(x => x.PONumber.ToLower().Contains(search) ||
                                         x.Address.ToLower().Contains(search) ||
                                         x.JobCode.ToLower().Contains(search) ||
                                         (x.JobType != null && x.JobType.Name.ToLower().Contains(search)) ||
                                         (x.Customer != null && x.Customer.Name.ToLower().Contains(search)));
            }

            // get total jobs in range
            var total = await query.CountAsync(cancellationToken);

            // apply paging
            query = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            // map results
            var results = await query.Select(x => x.MapToListDto()).ToListAsync(cancellationToken);

            // return model
            return new ResultList<JobListDto>
            {
                Results = results,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalResults = total
            };
        }
        catch (Exception e)
        {
            return new ResultList<JobListDto>
            {
                Error = e.InnerException?.Message ?? e.Message
            };
        }
    }
}
