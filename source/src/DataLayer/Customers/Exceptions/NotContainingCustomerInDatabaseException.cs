﻿using DataLayer.Customers.DTOs;

namespace DataLayer.Customers.Exceptions
{
    public class NotContainingCustomerInDatabaseException : Exception
    {
        public NotContainingCustomerInDatabaseException(CustomerDTO lookingForCutomer)
        {
            Customer = lookingForCutomer;
        }
        public NotContainingCustomerInDatabaseException(string? message, CustomerDTO lookingForCutomer) : base(message)
        {
            Customer = lookingForCutomer;
        }
        public NotContainingCustomerInDatabaseException(string? message, Exception? innerException, CustomerDTO lookingForCutomer) : base(message, innerException)
        {
            Customer = lookingForCutomer;
        }

        public CustomerDTO Customer { get; }
    }
}
