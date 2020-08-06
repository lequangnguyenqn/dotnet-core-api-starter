using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Customers;

namespace MyApp.Infrastructure.Domain.Customers
{
    internal sealed class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(b => b.Id);
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            builder.Property(p => p.Email).HasColumnName("Email").IsRequired().HasMaxLength(100);
        }
    }
}
