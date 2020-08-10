using Dapper;
using MediatR;
using MyApp.Application.Configuration.Data;
using MyApp.Application.Customers.GetCustomers;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Customers.GetCustomerDetails
{
    public class GetCustomeDetailsQueryHandler : IRequestHandler<GetCustomeDetailsQuery, CustomerDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetCustomeDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<CustomerDetailsDto> Handle(GetCustomeDetailsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string query = "SELECT " +
                            "[Customer].[Id], " +
                            "[Customer].[Email] " +
                            "FROM Customers AS [Customer] " +
                            "WHERE [Customer].IsDeleted = 0 AND [Customer].Id = @CustomerId ";
            var customer = await connection.QuerySingleOrDefaultAsync<CustomerDetailsDto>(query, new { request.CustomerId });

            return customer;
        }
    }
}
