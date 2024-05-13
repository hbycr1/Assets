using Application.Common.Models;
using Application.Jobs;
using Application.Jobs.Command;
using Application.Jobs.Query;
using Domain.Enums;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Jobs;

public class JobsController : ControllerBase
{
	private readonly IMediator _mediator;

	public JobsController(IMediator mediator)
	{
		_mediator=mediator;
	}

	[HttpGet]
	public async Task<ResultList<JobListDto>> Get([FromQuery] int page = 1,
												  [FromQuery] int pageSize = 25,
												  [FromQuery] string? search = null,
												  [FromQuery] string? sort = null,
												  [FromQuery] DateTime? startDate = null,
												  [FromQuery] DateTime? endDate = null,
												  [FromQuery] JobStatus[]? status = null)
	{
		return await _mediator.Send(new GetJobsQuery
		{
			CompanyId = GetCurrentCompanyId(),
			Page = page,
			PageSize = pageSize,
			StartDate = startDate,
			EndDate = endDate,
			Search = search,
			Sort = sort,
			JobStatus = status
        });
	}

	[HttpGet("{id}")]
	public async Task<Result<JobDetailDto>> GetJob([FromRoute] Guid id)
	{
		return await _mediator.Send(new GetJobByIdQuery { CompanyId = GetCurrentCompanyId(), Id = id });
	}

	[HttpPost]
	public async Task<Result> CreateJob([FromBody] CreateJobCommand command)
	{
		command.UserId = GetCurrentUserId();
		command.CompanyId = GetCurrentCompanyId();

        return await _mediator.Send(command);
	}

	[HttpPut]
	public async Task<Result> UpdateJob([FromBody] UpdateJobCommand command)
    {
        //command.CompanyId = GetCurrentCompanyId();

        return await _mediator.Send(command);
	}
}
