using MyApp.Domain.Customers;
using MyApp.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Domain
{
    public static class DbInitializer
    {
        public static void Initialize(MyAppContext context)
        {
            context.Customers.Add(new Customer
            {
                Id = Guid.NewGuid(),
                Email = "info@microsoft.com",
                Name = "Microsoft",
                CreatedDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            });

            context.Customers.Add(new Customer
            {
                Id = Guid.NewGuid(),
                Email = "info@google.com",
                Name = "Google",
                CreatedDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            });

            context.SaveChanges();

            context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Email = "qc@gmail.com",
                FirstName = "QC",
                LastName = "Manager",
                CreatedDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            });

            context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Email = "dev@gmail.com",
                FirstName = "DEV",
                LastName = "Manager",
                CreatedDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            });

            context.SaveChanges();
        }
    }
}
