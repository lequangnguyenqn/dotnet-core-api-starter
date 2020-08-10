using MediatR;
using MyApp.Domain;
using MyApp.Domain.Customers;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, AddCustomerRespone>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public AddCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<AddCustomerRespone> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Email, request.Name);

            await _customerRepository.AddAsync(customer);

            await _unitOfWork.CommitAsync(cancellationToken);

            await _mediator.Publish(new AddCustomerNotification { CustomerId = customer.Id }, cancellationToken);

            return new AddCustomerRespone { CustomerId = customer.Id };
        }
    }
}
