using DataLayer.Customers.Models;

namespace DataLayer.Customers.Exceptions
{
    class NotContainingCustomerInDatabaseException : Exception
    {
        public Customer customer { get; }

        public NotContainingCustomerInDatabaseException(Customer lookingForCutomer)
        {
            customer = lookingForCutomer;
        }

        public NotContainingCustomerInDatabaseException(string? message, Customer lookingForCutomer) : base(message)
        {
            customer = lookingForCutomer;
        }

        public NotContainingCustomerInDatabaseException(string? message, Exception? innerException, Customer lookingForCutomer) : base(message, innerException)
        {
            customer = lookingForCutomer;
        }
    }
}
