using DataLayer.Base.DataValidators;
using System.Security;

namespace DataLayer.Customers.Validation
{
    public class CustomerValidator : ICustomerValidator
    {
        public bool Validate(IValidatableCustomer customer)
        {
            bool isValide = ValidateCustomerNr(customer.CustomerNr)
                && ValidateFirstName(customer.FirstName)
                && ValidateLastName(customer.LastName)
                && ValidateStreetName(customer.StreetName)
                && ValidateHouseNumer(customer.HouseNumber)
                && ValidatePostalCode(customer.PostalCode)
                && ValidateCity(customer.City)
                && ValidateEmailAddress(customer.EmailAddress)
                && ValidateWebsiteUrl(customer.WebsiteURL)
                && ValidateHashedPassword(customer.HashedPassword);

            return isValide;
        }

        public bool ValidateCustomerNr(string customerNr)
        {
            bool isValide = StringValidator.Validate(customerNr, CUSTOMER_NR_VALIDATION_PATTERN);
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
        public bool ValidateHashedPassword(string hashedPassword)
        {
            bool isValide = StringValidator.Validate(hashedPassword, HASHED_PASSWORD_VALIDATION_PATTERN);
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

        private const string CUSTOMER_NR_VALIDATION_PATTERN = @"^CU\d{5}$";
        private const string FIRST_NAME_VALIDATION_PATTERN = @"^(.{1,100})$";
        private const string LAST_NAME_VALIDATION_PATTERN = @"^(.{1,100})$";
        private const string STREET_NAME_VALIDATION_PATTERN = @"^(.{1,200})$";
        private const string HOUSE_NUMBER_VALIDATION_PATTERN = @"^(.{1,10})$";
        private const string POSTAL_CODE_VALIDATION_PATTERN = @"^(.{1,10})$";
        private const string CITY_VALIDATION_PATTERN = @"^(.{1,100})$";
        private const string EMAIL_ADDRESS_VALIDATION_PATTERN = @"^(?=[\S\s]{1,200}$)[a-zA-Z0-9!#$&_*?^{}-]+(\.[a-zA-Z0-9!#$&_*?^{}-]+)*@([a-zA-Z0-9]+([a-zA-Z0-9-]*)\.)+[a-zA-Z]+";
        private const string WEBSITE_URL_VALIDATION_PATTERN = @"^[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,253}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&\/=]*){4,255}$";
        private const string PASSWORD_VALIDATION_PATTERN = @"(?=.*[0-9])(?=.*[a-zäöü])(?=.*[A-Z])[a-zäöüA-ZÄÖÜ0-9!#$&_*?^{}-]{8,255}";
        private const string HASHED_PASSWORD_VALIDATION_PATTERN = @"^.+$";
    }
}
