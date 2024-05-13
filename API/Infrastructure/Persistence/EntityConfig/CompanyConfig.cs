using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig;

public class CompanyConfig : IEntityTypeConfiguration<Company>
{
	public void Configure(EntityTypeBuilder<Company> builder)
	{
		builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
		builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
		builder.Property(x => x.Phone).HasMaxLength(255).IsRequired();
		builder.Property(x => x.ABN).HasMaxLength(20).IsRequired();

		builder.Property(x => x.Created).IsRequired();
		builder.Property(x => x.Updated).IsRequired(false);

		builder.HasMany(x => x.Users).WithOne(x => x.Company).HasForeignKey(x => x.CompanyId);
		builder.HasMany(x => x.Customers).WithOne(x => x.Company).HasForeignKey(x => x.CompanyId);
		builder.HasMany(x => x.Jobs).WithOne(x => x.Company).HasForeignKey(x => x.CompanyId);
	}
}
