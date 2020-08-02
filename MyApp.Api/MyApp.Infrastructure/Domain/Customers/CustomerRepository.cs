using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Customers;
using System;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Domain.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyAppContext _dbContext;

        public CustomerRepository(MyAppContext context)
        {
            this._dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Customer customer)
        {
            await this._dbContext.Customers.AddAsync(customer);
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await this._dbContext.Customers
                .SingleAsync(x => x.Id == id);
        }
    }
}
