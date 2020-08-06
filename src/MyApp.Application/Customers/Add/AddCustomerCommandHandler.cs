using MediatR;
using MyApp.Domain;
using MyApp.Domain.Customers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Name,
                CreatedDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };

            await _customerRepository.AddAsync(customer);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new CustomerDto { Id = customer.Id };
        }
    }
}
