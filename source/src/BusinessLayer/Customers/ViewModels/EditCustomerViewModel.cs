using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.Commands;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Validation;
using System.Windows.Input;

namespace BusinessLayer.Customers.ViewModels
{
    public class EditCustomerViewModel : BaseCreateEditCustomerViewModel
    {
        public EditCustomerViewModel(ManagerStore managerStore, CustomerDTO customer, FromSubNavigationService<CustomerListingViewModel> customerListingViweModelNavigateBackService, ICustomerValidator customerValidator)
            : base(customerValidator)
        {
            SaveChangedToCustomerCommand = _saveChangedToCustomerCommand = new UpdateCustomerCommand(managerStore, customer, this, customerListingViweModelNavigateBackService);
            CancelEditCustomerCommand = new NavigateCommand(customerListingViweModelNavigateBackService);

            Id = customer.Id.ToString();
            CustomerNr = customer.CustomerNr;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            StreetName = customer.StreetName;
            HouseNumber = customer.HouseNumber;
            PostalCode = customer.PostalCode;
            City = customer.City;
            EmailAddress = customer.EmailAddress;
            WebsiteUrl = customer.WebsiteURL;
            Password = string.Empty;
        }

        public string Id { get; }
        public ICommand SaveChangedToCustomerCommand { get; }
        public ICommand CancelEditCustomerCommand { get; }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;


                OnPropertyChanged();

                const int maxCharacterSize = 255;
                const int minCharacterSize = 8;

                ClearErrors();

                if (string.IsNullOrEmpty(value))
                    return;
                if (value.Length < minCharacterSize)
                    AddError($"Has to be at least 8 characters!");
                if (value.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
                if (!_customerValidator.ValidatePassword(value))
                    AddError(ValidationErrorMessage());
            }
        }

        public override void Dispose(bool disposing)
        {
            _saveChangedToCustomerCommand.Dispose();
        }

        private readonly UpdateCustomerCommand _saveChangedToCustomerCommand;
    }
}
