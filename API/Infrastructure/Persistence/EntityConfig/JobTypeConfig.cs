using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig;

public class JobTypeConfig : IEntityTypeConfiguration<JobType>
{
	public void Configure(EntityTypeBuilder<JobType> builder)
	{
		builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
		builder.Property(x => x.CompanyId).IsRequired();


		builder.HasMany(x => x.Jobs).WithOne(x => x.JobType).HasForeignKey(x => x.JobTypeId);
	}
}
