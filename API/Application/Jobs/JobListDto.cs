using Application.Common.Models;
using Domain.Enums;

namespace Application.Jobs;

public class JobListDto : BaseModel
{
	public string JobCode { get; set; } = string.Empty;
    public string PONumber { get; set; } = string.Empty;
    public string? Customer { get; set; }
	public string? JobType { get; set; }

	public string Address { get; set; } = string.Empty;

	public DateTime Start { get; set; }
	public DateTime End { get; set; }
	public JobStatus Status { get; set; }
}
