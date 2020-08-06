using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Customers;
using MyApp.Domain.Users;

namespace MyApp.Infrastructure.Domain
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyAppContext).Assembly);
        }
    }
}
