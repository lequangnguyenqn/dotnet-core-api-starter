using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Api.Exceptions;
using MyApp.Application.Configuration.Queries;
using MyApp.Application.Customers.Add;
using MyApp.Application.Customers.GetCustomerDetails;
using MyApp.Application.Customers.GetCustomers;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MyApp.Api.Customers
{
    /// <summary>
    /// Customers endpoints
    /// </summary>
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
        /// <response code="200">OK</response>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomers([FromQuery] GetCustomersQuery request)
        {
            var customers = await _mediator.Send(request);

            return Ok(customers);
        }

        /// <summary>
        /// Get customer details.
        /// </summary>
        /// <returns>Customer details.</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Customer not found</response>
        [Route("{CustomerId}")]
        [HttpGet]
        [ProducesResponseType(typeof(CustomerDetailsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCustomer([FromRoute] GetCustomeDetailsQuery request)
        {
            var customer = await _mediator.Send(request);
            if(customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        /// <summary>
        /// Create a customer.
        /// </summary>
        /// <response code="201">Customer created</response>
        /// <response code="400">Customer has missing/invalid values</response>
        /// <response code="500">Oops! Something went wrong. Please try again later.</response>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(AddCustomerRespone), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsSwaggerResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsSwaggerResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerCommand request)
        {
            var customer = await _mediator.Send(request);

            return Created(string.Empty, customer);
        }
    }
}
