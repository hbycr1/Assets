using Domain.Enums;

namespace Domain.Entities;

public class User : BaseEntity
{
	// Keys
	public Guid CompanyId { get; set; }

	// General Info
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;

	// Password
	public string? Username { get; set; }
	public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
	public string Salt { get; set; } = string.Empty;

	// Statuses
	public UserRoles Role { get; set; }
	public UserStatus Status { get; set; }

	public virtual Company? Company { get; set; }
}
