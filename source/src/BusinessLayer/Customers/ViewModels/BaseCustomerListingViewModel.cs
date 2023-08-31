using BusinessLayer.Base.ViewModels;
using System.Net.Mail;

namespace BusinessLayer.Customers.ViewModels
{

    public class BaseCustomerListingViewModel : BaseErrorHandlingViewModel
    {
        public BaseCustomerListingViewModel()
            : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        {}
        public BaseCustomerListingViewModel(string firstName, string lastName, string streetName, string houseNumber, string city, string postalCode, string emailAddress, string websiteUrl, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            StreetName = streetName;
            HouseNumber = houseNumber;
            City = city;
            PostalCode = postalCode;
            EmailAddress = emailAddress;
            WebsiteUrl = websiteUrl;
            Password = password;
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
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();

                const int maxCharacterSize = 255;

                ClearErrors();
                if (string.IsNullOrEmpty(Password))
                    AddError(EMPTY_MESSAGE);
                if (Password.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
            }
        }

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _streetName = string.Empty;
        private string _houseNumber = string.Empty;
        private string _city = string.Empty;
        private string _postalCode = string.Empty;
        private string _emailAddress = string.Empty;
        private string _websiteUrl = string.Empty;
        private string _password = string.Empty;
    }
}
