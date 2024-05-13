using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig;

public class JobConfig : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.Property(x => x.PONumber).IsRequired(false);

        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.CompanyId).IsRequired();
        builder.Property(x => x.CustomerId).IsRequired(false);

        builder.Property(x => x.JobTypeId).IsRequired(false);
        builder.Property(x => x.Description).IsRequired();

        builder.Property(x => x.Start).IsRequired();
        builder.Property(x => x.End).IsRequired();

        builder.Property(x => x.Created).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();

        builder.Property(x => x.Updated).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false);
    }
}
