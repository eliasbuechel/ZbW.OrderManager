using DataLayer.Base.DataValidators;

namespace DataLayer.Customers.Validation
{
    public class CustomerValidator : ICustomerValidator
    {
        public bool Validate(IValidatableCustomer customer)
        {
            return ValidateFirstName(customer.FirstName)
                && ValidateLastName(customer.LastName)
                && ValidateStreetName(customer.StreetName)
                && ValidateHouseNumer(customer.HouseNumber)
                && ValidatePostalCode(customer.PostalCode)
                && ValidateCity(customer.City)
                && ValidateEmailAddress(customer.EmailAddress)
                && ValidateWebsiteUrl(customer.WebsiteURL)
                && ValidatePassword(customer.Password);
        }
        public bool ValidateCity(string city)
        {
            return StringValidator.Validate(city, CITY_VALIDATION_PATTERN);
        }
        public bool ValidateEmailAddress(string emailAddress)
        {
            return StringValidator.Validate(emailAddress, EMAIL_ADDRESS_VALIDATION_PATTERN);

        }
        public bool ValidateFirstName(string firstName)
        {
            return StringValidator.Validate(firstName, FIRST_NAME_VALIDATION_PATTERN);
        }
        public bool ValidateHouseNumer(string houseNumber)
        {
            return StringValidator.Validate(houseNumber, HOUSE_NUMBER_VALIDATION_PATTERN);
        }
        public bool ValidateLastName(string lastName)
        {
            return StringValidator.Validate(lastName, LAST_NAME_VALIDATION_PATTERN);

        }
        public bool ValidatePassword(string password)
        {
            return StringValidator.Validate(password, PASSWORD_VALIDATION_PATTERN);
        }
        public bool ValidatePostalCode(string postalCode)
        {
            return StringValidator.Validate(postalCode, POSTAL_CODE_VALIDATION_PATTERN);
        }
        public bool ValidateStreetName(string streetName)
        {
            return StringValidator.Validate(streetName, STREET_NAME_VALIDATION_PATTERN);
        }
        public bool ValidateWebsiteUrl(string websiteURL)
        {
            return StringValidator.Validate(websiteURL, WEBSITE_URL_VALIDATION_PATTERN);
        }

        private const string FIRST_NAME_VALIDATION_PATTERN = "/^[^\\s].{0,39}$";
        private const string LAST_NAME_VALIDATION_PATTERN = "/^[^\\s].{0,39}$";
        private const string STREET_NAME_VALIDATION_PATTERN = "/^[^\\s].{0,39}$";
        private const string HOUSE_NUMBER_VALIDATION_PATTERN = "/^[^\\s].{0,39}$";
        private const string POSTAL_CODE_VALIDATION_PATTERN = "/^[^\\s].{0,39}$";
        private const string CITY_VALIDATION_PATTERN = "/^[^\\s].{0,39}$";
        private const string EMAIL_ADDRESS_VALIDATION_PATTERN = "/^[^\\s].{0,39}$";
        private const string WEBSITE_URL_VALIDATION_PATTERN = "/^[^\\s].{0,39}$";
        private const string PASSWORD_VALIDATION_PATTERN = "/^[^\\s].{0,39}$";
    }
}
