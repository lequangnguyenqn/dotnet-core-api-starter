﻿using MediatR;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerCommand : IRequest<AddCustomerRespone>
    {
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
