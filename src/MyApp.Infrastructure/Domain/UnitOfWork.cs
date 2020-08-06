using MyApp.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyAppContext _dbContext;

        public UnitOfWork(
            MyAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
