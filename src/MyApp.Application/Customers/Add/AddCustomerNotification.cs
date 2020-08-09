using MediatR;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerNotification : INotification
    {
        public int CustomerId { get; }
    }
}
