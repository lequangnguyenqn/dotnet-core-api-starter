using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Customers;
using MyApp.Application.Customers.Add;
using System.Net;
using System.Threading.Tasks;

namespace MyApp.Api.Customers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Register customer.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerCommand request)
        {
            var customer = await _mediator.Send(request);

            return Created(string.Empty, customer);
        }
    }
}
