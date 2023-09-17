using System.Security;

namespace DataLayer.Customers.Validation
{
    public interface ICustomerValidator
    {
        bool Validate(IValidatableCustomer customer);
        bool ValidateCustomerNr(string customerNr);
        bool ValidateFirstName(string firstName);
        bool ValidateLastName(string lastName);
        bool ValidateStreetName(string streetName);
        bool ValidateHouseNumer(string houseNumber);
        bool ValidateCity(string city);
        bool ValidatePostalCode(string postalCode);
        bool ValidateEmailAddress(string emailAddress);
        bool ValidateWebsiteUrl(string websiteURL);
        bool ValidatePassword(string password);
    }
}
