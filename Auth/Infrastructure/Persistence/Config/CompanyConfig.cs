using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Config;

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
	}
}