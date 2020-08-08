using MediatR;
using MyApp.Application.Configuration.Queries;

namespace MyApp.Application.Customers.GetCustomers
{
    public class GetCustomersQuery : IRequest<PagedList<CustomerDto>>
    {
        public PageInfo Paging { get; set; }
        public string Email { get; set; }
    }
}
