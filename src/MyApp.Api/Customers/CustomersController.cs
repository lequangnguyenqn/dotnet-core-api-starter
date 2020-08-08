using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Configuration.Queries;
using MyApp.Application.Customers;
using MyApp.Application.Customers.Add;
using MyApp.Application.Customers.GetCustomers;
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
        /// Get customers.
        /// </summary>
        /// <returns>List of customers.</returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerOrders([FromQuery] GetCustomersQuery request)
        {
            var orders = await _mediator.Send(request);

            return Ok(orders);
        }

        /// <summary>
        /// Register customer.
        /// </summary>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerCommand request)
        {
            var customer = await _mediator.Send(request);

            return Created(string.Empty, customer);
        }
    }
}
