using System;
using System.Threading.Tasks;

namespace MyApp.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);

        Task AddAsync(User customer);
    }
}
