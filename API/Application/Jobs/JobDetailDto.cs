using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Jobs;

public class JobDetailDto : BaseModel
{
    public Guid? JobTypeId { get; set; }
    public Guid? CustomerId { get; set; }

    public string JobCode { get; set; } = string.Empty;
    public string PONumber { get; set; } = string.Empty;

    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public JobStatus Status { get; set; }

    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
