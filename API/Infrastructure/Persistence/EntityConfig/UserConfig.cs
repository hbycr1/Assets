using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig;

public class UserConfig : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.Property(x => x.FirstName).HasMaxLength(255).IsRequired();
		builder.Property(x => x.LastName).HasMaxLength(255).IsRequired();
		builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
		builder.Property(x => x.Phone).HasMaxLength(255).IsRequired();

		builder.Property(x => x.Username).HasMaxLength(255).IsRequired(false);
		builder.Property(x => x.PasswordHash).IsRequired();
		builder.Property(x => x.Salt).IsRequired();

		builder.Property(x => x.Created).IsRequired();
		builder.Property(x => x.Updated).IsRequired(false);
	}
}
