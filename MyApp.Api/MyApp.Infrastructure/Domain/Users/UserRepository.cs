using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Users;
using System;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Domain.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly MyAppContext _dbContext;

        public UserRepository(MyAppContext context)
        {
            this._dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(User user)
        {
            await this._dbContext.Users.AddAsync(user);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await this._dbContext.Users
                .SingleAsync(x => x.Id == id);
        }
    }
}
