using System;

namespace MyApp.Application.Customers.GetCustomers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
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
