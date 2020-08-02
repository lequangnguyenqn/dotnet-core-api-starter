using System;
using System.Threading.Tasks;

namespace MyApp.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(Guid id);

        Task AddAsync(Customer customer);
    }
}
