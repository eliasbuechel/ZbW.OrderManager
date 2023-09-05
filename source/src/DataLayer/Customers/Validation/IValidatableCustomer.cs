﻿namespace DataLayer.Customers.Validation
{
    public interface IValidatableCustomer
    {
        string FirstName { get; }
        string LastName { get; }
        string StreetName { get; }
        string HouseNumber { get; }
        string City { get; }
        string PostalCode { get; }
        string EmailAddress { get; }
        string WebsiteURL { get; }
        string Password { get; }
    }
}