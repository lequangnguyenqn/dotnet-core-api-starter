using MediatR;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerCommand : IRequest<AddCustomerRespone>
    {
        /// <summary>
        /// The email of the customer
        /// </summary>
        /// <example>terry.le@example.com</example>
        public string Email { get; set; }

        /// <summary>
        /// The name of the customer
        /// </summary>
        /// <example>Terry Le</example>
        public string Name { get; set; }
    }
}
