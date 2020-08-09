using MediatR;

namespace MyApp.Domain.Customers
{
    public class AddCustomerEvent : INotification
    {
        public int CustomerId { get; }
    }
}
