using Application.Common.Interfaces;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Identity;

public class IdentityServerProfileService : IProfileService
{
	private readonly IDbContext _db;

	public IdentityServerProfileService(IDbContext db)
	{
		_db=db;
	}

	public async Task GetProfileDataAsync(ProfileDataRequestContext context)
	{
		var clientId = context?.Client?.ClientId;
		if (context != null && !string.IsNullOrEmpty(clientId))
		{
			var user = await GetUserAsync(context.Subject.GetSubjectId(), clientId);
			if (user != null)
				context.IssuedClaims.AddRange(user.Claims);
		}
	}

	public async Task IsActiveAsync(IsActiveContext context)
	{
		var clientId = context?.Client?.ClientId;
		if (context != null && !string.IsNullOrEmpty(clientId))
		{
			var user = await GetUserAsync(context.Subject.GetSubjectId(), clientId);
			if (user != null)
				context.IsActive = user.Status == Domain.Enums.UserStatus.Active;
			else
				context.IsActive = false;
		}
	}

	public async Task<ApplicationUser?> GetUserAsync(string subjectId, string clientId)
	{
		if (Guid.TryParse(subjectId, out Guid userId))
		{
			var user = await _db.User.Where(u => u.Id == userId)
									 .Select(u => new ApplicationUser
									 {
										 Status = u.Status,
										 Claims = new Claim[]
										 {
											 new("company", $"{u.CompanyId}")
										 }
									 })
									 .FirstOrDefaultAsync();

			if (user != null)
			{
				// validate client id against the user role

				return user;
			}
		}

		return null;
	}
}
