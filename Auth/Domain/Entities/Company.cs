using Domain.Enums;

namespace Domain.Entities;

public class Company
{
	// Keys
	public Guid Id { get; set; }

	// General Info
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public string ABN { get; set; } = string.Empty;
	public string? Logo { get; set; }

	// Status
	public CompanyStatus Status { get; set; }

	// Modifications
	public DateTime Created { get; set; }
	public DateTime? Updated { get; set; }

	// Users
	public IList<User> Users { get; set; } = new List<User>();
}
