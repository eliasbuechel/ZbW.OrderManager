﻿using DataLayer.Customers.Validation;

namespace DataLayer.Customers.DTOs
{
    public class CustomerDTO : IValidatableCustomer
    {
        public CustomerDTO(int id, string customerNr, string firstName, string lastName, string streetName, string houseNumber, string city, string postalCode, string emailAddress, string websiteURL, string password)
        {
            Id = id;
            CustomerNr = customerNr;
            FirstName = firstName;
            LastName = lastName;
            StreetName = streetName;
            HouseNumber = houseNumber;
            City = city;
            PostalCode = postalCode;
            EmailAddress = emailAddress;
            WebsiteURL = websiteURL;
            HashedPassword = password;
        }

        public int Id { get; }
        public string CustomerNr { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string StreetName { get; }
        public string HouseNumber { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string EmailAddress { get; }
        public string WebsiteURL { get; }
        public string HashedPassword { get; }
    }
}
