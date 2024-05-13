using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class APIDbContext : DbContext, IDbContext
{
    // Construct
    public APIDbContext(DbContextOptions options)
        : base(options)
    { }

    // Entities
    public DbSet<Company> Company => Set<Company>();
    public DbSet<User> User => Set<User>();
    public DbSet<Job> Jobs => Set<Job>();

    // Overrides
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await base.SaveChangesAsync(cancellationToken);
}
