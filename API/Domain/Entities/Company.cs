using Domain.Enums;

namespace Domain.Entities;

public class Company : BaseEntity
{
	// General Info
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public string ABN { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string? Logo { get; set; }

	// Status & Type
	public CompanyStatus Status { get; set; }
	public CompanyType Type { get; set; }

	// Relationships
	public IList<User> Users { get; set; } = new List<User>();
	public IList<Customer> Customers { get; set; } = new List<Customer>();
	public IList<Job> Jobs { get; set; } = new List<Job>();
}
