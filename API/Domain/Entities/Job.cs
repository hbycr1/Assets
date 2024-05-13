using Domain.Enums;

namespace Domain.Entities;

public class Job : BaseEntity
{
	public Guid CompanyId { get; set; }
	public Guid? JobTypeId { get; set; }
    public Guid? CustomerId { get; set; }

    public string JobCode { get; set; } = string.Empty;
    public string PONumber { get; set; } = string.Empty;

    public DateTime Start { get; set; }
	public DateTime End { get; set; }
	public JobStatus Status { get; set; }

	public string Address { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	public virtual JobType? JobType { get; set; }
	public virtual Company? Company { get; set; }
	public virtual Customer? Customer { get; set; }
}
