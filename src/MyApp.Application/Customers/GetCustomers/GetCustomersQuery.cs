using MyApp.Application.Configuration.Queries;

namespace MyApp.Application.Customers.GetCustomers
{
    public class GetCustomersQuery : PagingQuery<PagedList<CustomerDto>>
    {
        public string Email { get; set; }
    }
}
