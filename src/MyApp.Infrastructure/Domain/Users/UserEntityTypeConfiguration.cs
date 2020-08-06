using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Users;

namespace MyApp.Infrastructure.Domain.Users
{
    internal sealed class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(b => b.Id);
            builder.Property(p => p.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(100);
            builder.Property(p => p.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(100);
            builder.Property(p => p.Email).HasColumnName("Email").IsRequired().HasMaxLength(100);
        }
    }
}
