using BusinessLayer.Base.ViewModels;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BusinessLayer.Customers.ViewModels
{

    public class BaseCustomerFieldsViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private string _firstName;
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

        private string _lastName;
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

        private string _streetName;
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

        private string _houseNumber;
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

        private string _city;
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

        private string _postalCode;
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

        private string _emailAddress;
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

        private string _websiteUrl;
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

        private string _password;
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

        private void AddError(string errorMessage, [CallerMemberName] string propertyName = null)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        private void ClearErrors([CallerMemberName] string propertyName = null)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null)
                return new List<string>();

            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private string ToLongErrorMessage(int maxSize)
        {
            return $"Cannot be larger than {maxSize.ToString()} characters.";
        }

        private const string EMPTY_MESSAGE = "Cannot be empty.";
    }
}
