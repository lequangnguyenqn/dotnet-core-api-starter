﻿using MyApp.Domain.Customers;
using MyApp.Domain.Users;
using System;
using System.Linq;

namespace MyApp.Infrastructure.Domain
{
    public static class DbInitializer
    {
        public static void Initialize(MyAppContext context)
        {
            if(!context.Customers.Any(p => p.Email == "info@microsoft.com"))
            {
                context.Customers.Add(new Customer("info@microsoft.com", "Microsoft"));
            }

            if (!context.Customers.Any(p => p.Email == "info@google.com"))
            {
                context.Customers.Add(new Customer("info@google.com", "Google"));
            }

            context.SaveChanges();

            if (!context.Users.Any(p => p.Email == "qc@gmail.com"))
            {
                context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Email = "qc@gmail.com",
                    FirstName = "QC",
                    LastName = "Manager",
                    CreatedDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                });
            }

            if (!context.Users.Any(p => p.Email == "dev@gmail.com"))
            {
                context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Email = "dev@gmail.com",
                    FirstName = "DEV",
                    LastName = "Manager",
                    CreatedDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                });
            }

            context.SaveChanges();
        }
    }
}
