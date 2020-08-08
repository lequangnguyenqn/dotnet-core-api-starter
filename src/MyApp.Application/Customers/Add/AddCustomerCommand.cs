using MediatR;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerCommand : IRequest<CustomerDto>
    {
        public string Email { get; set; }

        public string Name { get; set; }
    }
}
