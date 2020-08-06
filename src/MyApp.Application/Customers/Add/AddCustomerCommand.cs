using MediatR;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerCommand : IRequest<CustomerDto>
    {
        public string Email { get; }

        public string Name { get; }

        public AddCustomerCommand(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}
