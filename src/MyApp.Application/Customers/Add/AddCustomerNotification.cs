﻿using MediatR;
using System;

namespace MyApp.Application.Customers.Add
{
    public class AddCustomerNotification : INotification
    {
        public Guid CustomerId { get; set; }
    }
}
