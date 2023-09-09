using DataLayer.Customers.DTOs;

namespace DataLayer.Customers.Exceptions
{
    public class NotContainingCustomerInDatabaseException : Exception
    {
        public NotContainingCustomerInDatabaseException(CustomerDTO lookingForCutomer)
        {
            customer = lookingForCutomer;
        }
        public NotContainingCustomerInDatabaseException(string? message, CustomerDTO lookingForCutomer) : base(message)
        {
            customer = lookingForCutomer;
        }
        public NotContainingCustomerInDatabaseException(string? message, Exception? innerException, CustomerDTO lookingForCutomer) : base(message, innerException)
        {
            customer = lookingForCutomer;
        }

        public CustomerDTO customer { get; }
    }
}
