using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
	public UserStatus Status { get; set; }

	public Claim[] Claims { get; set; } = Array.Empty<Claim>();
}
