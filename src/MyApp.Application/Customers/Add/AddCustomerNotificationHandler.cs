using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerNotificationHandler : INotificationHandler<AddCustomerNotification>
    {
        public async Task Handle(AddCustomerNotification notification, CancellationToken cancellationToken)
        {
            // Send welcome e-mail message...
        }
    }
}
