using MediatR;
using MyApp.Domain.Customers;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerNotificationHandler : INotificationHandler<AddCustomerEvent>
    {
        public async Task Handle(AddCustomerEvent notification, CancellationToken cancellationToken)
        {
            // Send welcome e-mail message...
        }
    }
}
