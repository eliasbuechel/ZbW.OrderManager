using DataLayer.Base.DataValidators;

namespace DataLayer.Customers.Validation
{
    public class CustomerValidator : ICustomerValidator
    {
        public bool Validate(IValidatableCustomer customer)
        {
            bool isValide = ValidateFirstName(customer.FirstName)
                && ValidateLastName(customer.LastName)
                && ValidateStreetName(customer.StreetName)
                && ValidateHouseNumer(customer.HouseNumber)
                && ValidatePostalCode(customer.PostalCode)
                && ValidateCity(customer.City)
                && ValidateEmailAddress(customer.EmailAddress)
                && ValidateWebsiteUrl(customer.WebsiteURL)
                && ValidatePassword(customer.Password);

            return isValide;
        }
        public bool ValidateCity(string city)
        {
            bool isValide = StringValidator.Validate(city, CITY_VALIDATION_PATTERN);
            return isValide;
        }
        public bool ValidateEmailAddress(string emailAddress)
        {
            return StringValidator.Validate(emailAddress, EMAIL_ADDRESS_VALIDATION_PATTERN);

        }
        public bool ValidateFirstName(string firstName)
        {
            bool isValide = StringValidator.Validate(firstName, FIRST_NAME_VALIDATION_PATTERN);
            return isValide;
        }
        public bool ValidateHouseNumer(string houseNumber)
        {
            bool isValide = StringValidator.Validate(houseNumber, HOUSE_NUMBER_VALIDATION_PATTERN);
            return isValide;
        }
        public bool ValidateLastName(string lastName)
        {
            bool isValide = StringValidator.Validate(lastName, LAST_NAME_VALIDATION_PATTERN);
            return isValide;
        }
        public bool ValidatePassword(string password)
        {
            bool isValide = StringValidator.Validate(password, PASSWORD_VALIDATION_PATTERN);
            return isValide;
        }
        public bool ValidatePostalCode(string postalCode)
        {
            bool isValide = StringValidator.Validate(postalCode, POSTAL_CODE_VALIDATION_PATTERN);
            return isValide;
        }
        public bool ValidateStreetName(string streetName)
        {
            bool isValide = StringValidator.Validate(streetName, STREET_NAME_VALIDATION_PATTERN);
            return isValide;
        }
        public bool ValidateWebsiteUrl(string websiteURL)
        {
            bool isValide = StringValidator.Validate(websiteURL, WEBSITE_URL_VALIDATION_PATTERN);
            return isValide;
        }

        private const string FIRST_NAME_VALIDATION_PATTERN = @"^.+$";
        private const string LAST_NAME_VALIDATION_PATTERN = @"^.+$";
        private const string STREET_NAME_VALIDATION_PATTERN = @"^.+$";
        private const string HOUSE_NUMBER_VALIDATION_PATTERN = @"^.+$";
        private const string POSTAL_CODE_VALIDATION_PATTERN = @"^.+$";
        private const string CITY_VALIDATION_PATTERN = @"^.+$";
        private const string EMAIL_ADDRESS_VALIDATION_PATTERN = @"^.+$";
        private const string WEBSITE_URL_VALIDATION_PATTERN = @"^.+$";
        private const string PASSWORD_VALIDATION_PATTERN = @"^.+$";
    }
}
