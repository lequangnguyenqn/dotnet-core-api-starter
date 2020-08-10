using System;

namespace MyApp.Application.Customers.GetCustomers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// The email of the customer
        /// </summary>
        /// <example>youremail@example.com</example>
        public string Email { get; set; }

        /// <summary>
        /// The name of the customer
        /// </summary>
        /// <example>John Smith</example>
        public string Name { get; set; }
    }
}
