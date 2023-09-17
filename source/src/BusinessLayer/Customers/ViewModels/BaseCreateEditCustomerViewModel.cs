using BusinessLayer.Base.ViewModels;
using DataLayer.Customers.Validation;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Customers.ViewModels
{

    public class BaseCreateEditCustomerViewModel : BaseErrorHandlingViewModel
    {
        public BaseCreateEditCustomerViewModel(ICustomerValidator customerValidator)
        {
            _customerValidator = customerValidator;
            CustomerNr = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            StreetName = string.Empty;
            HouseNumber = string.Empty;
            City = string.Empty;
            PostalCode = string.Empty;
            EmailAddress = string.Empty;
            WebsiteUrl = string.Empty;
        }

        public string CustomerNr
        {
            get => _customerNr;
            set
            {
                _customerNr = value;

                OnPropertyChanged();

                ClearErrors();
                if (!_customerValidator.ValidateCustomerNr(_customerNr))
                    AddError("Customer does not match the patter 'CUXXXXX'");
            }
        }
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged();

                const int maxCharacterSize = 100;

                ClearErrors();
                if (string.IsNullOrEmpty(FirstName))
                    AddError(EMPTY_MESSAGE);
                if (FirstName.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
                if (!_customerValidator.ValidateFirstName(FirstName))
                    AddError(ValidationErrorMessage());
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged();

                const int maxCharacterSize = 100;

                ClearErrors();
                if (string.IsNullOrEmpty(LastName))
                    AddError(EMPTY_MESSAGE);
                if (LastName.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
                if (!_customerValidator.ValidateLastName(LastName))
                    AddError(ValidationErrorMessage());
            }
        }
        public string StreetName
        {
            get
            {
                return _streetName;
            }
            set
            {
                _streetName = value;
                OnPropertyChanged();

                const int maxCharacterSize = 200;

                ClearErrors();
                if (string.IsNullOrEmpty(StreetName))
                    AddError(EMPTY_MESSAGE);
                if (StreetName.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
                if (!_customerValidator.ValidateStreetName(StreetName))
                    AddError(ValidationErrorMessage());
            }
        }
        public string HouseNumber
        {
            get
            {
                return _houseNumber;
            }
            set
            {
                _houseNumber = value;
                OnPropertyChanged();

                const int maxCharacterSize = 10;

                ClearErrors();
                if (string.IsNullOrEmpty(HouseNumber))
                    AddError(EMPTY_MESSAGE);
                if (HouseNumber.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
                if (!_customerValidator.ValidateHouseNumer(HouseNumber))
                    AddError(ValidationErrorMessage());
            }
        }
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged();

                const int maxCharacterSize = 100;

                ClearErrors();
                if (string.IsNullOrEmpty(City))
                    AddError(EMPTY_MESSAGE);
                if (City.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
                if (!_customerValidator.ValidateCity(City))
                    AddError(ValidationErrorMessage());
            }
        }
        public string PostalCode
        {
            get
            {
                return _postalCode;
            }
            set
            {
                _postalCode = value;
                OnPropertyChanged();

                const int maxCharacterSize = 10;

                ClearErrors();
                if (string.IsNullOrEmpty(PostalCode))
                    AddError(EMPTY_MESSAGE);
                if (PostalCode.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
                if (!_customerValidator.ValidatePostalCode(PostalCode))
                    AddError(ValidationErrorMessage());
            }
        }
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
                OnPropertyChanged();

                const int maxCharacterSize = 200;

                ClearErrors();
                if (string.IsNullOrEmpty(EmailAddress))
                    AddError(EMPTY_MESSAGE);
                if (EmailAddress.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
                if (!_customerValidator.ValidateEmailAddress(EmailAddress))
                    AddError(ValidationErrorMessage());
            }
        }
        public string WebsiteUrl
        {
            get
            {
                return _websiteUrl;
            }
            set
            {
                _websiteUrl = value;
                OnPropertyChanged();

                const int maxCharacterSize = 255;

                ClearErrors();
                if (string.IsNullOrEmpty(WebsiteUrl))
                    AddError(EMPTY_MESSAGE);
                if (WebsiteUrl.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
                if (!_customerValidator.ValidateWebsiteUrl(WebsiteUrl))
                    AddError(ValidationErrorMessage());
            }
        }

        public string HashedPassword => BitConverter.ToString(SHA256.HashData(Encoding.UTF8.GetBytes(_password))).Replace("-", "").ToLower();

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _streetName = string.Empty;
        private string _houseNumber = string.Empty;
        private string _city = string.Empty;
        private string _postalCode = string.Empty;
        private string _emailAddress = string.Empty;
        private string _websiteUrl = string.Empty;
        protected string _password = string.Empty;
        private string _customerNr = string.Empty;
        protected readonly ICustomerValidator _customerValidator;
    }
}