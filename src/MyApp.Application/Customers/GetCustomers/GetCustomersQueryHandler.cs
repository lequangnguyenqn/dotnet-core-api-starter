using Dapper;
using MediatR;
using MyApp.Application.Configuration.Data;
using MyApp.Application.Configuration.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Customers.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PagedList<CustomerDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetCustomersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<PagedList<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT " +
                               "[Customer].[Id], " +
                               "[Customer].[Email] " +
                               "FROM Customers AS [Customer] ";
            var orders = await connection.QueryAsync<CustomerDto>(sql);

            return new PagedList<CustomerDto>
            {
                Items = orders,
                Paging = request.Paging
            };
        }
    }
}
