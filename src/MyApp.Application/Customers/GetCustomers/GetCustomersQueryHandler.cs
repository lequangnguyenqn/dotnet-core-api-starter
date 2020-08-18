using Dapper;
using MediatR;
using MyApp.Application.Configuration.Data;
using MyApp.Application.Configuration.Queries;
using System.Linq;
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
            string query = "SELECT " +
                            "[Customer].[Id], " +
                            "[Customer].[Email] " +
                            "FROM Customers AS [Customer] " +
                            "WHERE [Customer].IsDeleted = 0 " +
                            (string.IsNullOrEmpty(request.Email) ? "" : $" AND [Customer].Email = '{request.Email}' ") +
                            "ORDER BY [Customer].CreatedDate DESC " +
                            _sqlConnectionFactory.GetSQLPaging(request.CurrentPage, request.PageSize) +

                            "SELECT COUNT(*) " +
                            "FROM Customers AS [Customer] " +
                            "WHERE [Customer].IsDeleted = 0";
            var multi = await connection.QueryMultipleAsync(query);

            return new PagedList<CustomerDto>(request)
            {
                Items = multi.Read<CustomerDto>().ToList(),
                TotalCount = multi.ReadFirst<int>()
            };
        }
    }
}
