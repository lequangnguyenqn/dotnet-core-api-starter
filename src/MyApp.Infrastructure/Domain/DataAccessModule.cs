using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Configuration.Data;
using MyApp.Domain;
using MyApp.Domain.Customers;
using MyApp.Infrastructure.Database;
using MyApp.Infrastructure.Domain.Customers;
namespace MyApp.Infrastructure.Domain
{
    public static class DataAccessModule
    {
        public static void AddDataAccessModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyAppContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ISqlConnectionFactory>(p => new SqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
