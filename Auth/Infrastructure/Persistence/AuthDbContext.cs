using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AuthDbContext : DbContext, IDbContext
{
	// Construct
	public AuthDbContext(DbContextOptions options)
		: base(options) { }

	// Entities
	public DbSet<Company> Company => Set<Company>();
	public DbSet<User> User => Set<User>();

	// Overrides
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		=> await base.SaveChangesAsync(cancellationToken);
}
