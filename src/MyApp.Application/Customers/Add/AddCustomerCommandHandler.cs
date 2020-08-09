using MediatR;
using MyApp.Domain;
using MyApp.Domain.Customers;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, CustomerDto>
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

        public async Task<CustomerDto> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Email, request.Name);

            await _customerRepository.AddAsync(customer);

            await _mediator.Publish(new AddCustomerEvent(), cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new CustomerDto { Id = customer.Id, Email = customer.Email, Name = customer.Name };
        }
    }
}
