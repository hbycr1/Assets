using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IDbContext
{
	DbSet<Company> Company { get; }
	DbSet<User> User { get; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
