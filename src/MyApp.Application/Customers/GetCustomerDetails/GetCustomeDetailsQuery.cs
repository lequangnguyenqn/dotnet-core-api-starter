using MediatR;
using MyApp.Application.Customers.GetCustomers;
using System;

namespace MyApp.Application.Customers.GetCustomerDetails
{
    public class GetCustomeDetailsQuery : IRequest<CustomerDetailsDto>
    {
        public Guid CustomerId { get; set; }
    }
}
