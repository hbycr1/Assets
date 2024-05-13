using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
	public ControllerBase() { }

	public Guid GetCurrentUserId()
	{
		var sub = User.Claims.FirstOrDefault(x => x.Type ==ClaimTypes.NameIdentifier)?.Value;

		if (!string.IsNullOrEmpty(sub))
			return Guid.Parse(sub);

		throw new UnauthorizedAccessException("Could not get Authenticated User");
	}

	public Guid GetCurrentCompanyId()
	{
		var sub = User.Claims.FirstOrDefault(x => x.Type == "company")?.Value;

		if (!string.IsNullOrEmpty(sub))
			return Guid.Parse(sub);

		throw new UnauthorizedAccessException("Could not get Authenticated User");
	}
}