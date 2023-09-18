using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.Commands;
using DataLayer.Customers.Validation;
using System.Windows.Input;

namespace BusinessLayer.Customers.ViewModels
{
    public class CreateCustomerViewModel : BaseCreateEditCustomerViewModel
    {
        public CreateCustomerViewModel(ManagerStore managerStore, FromSubNavigationService<CustomerListingViewModel> customerListingViweModelNavigateBackService, ICustomerValidator customerValidator)
            : base(customerValidator)
        {
            CreateCustomerCommand = _createCustomerCommand = new CreateCustomerCommand(managerStore, this, customerListingViweModelNavigateBackService);
            CancelCreateCustomerCommand = new NavigateCommand(customerListingViweModelNavigateBackService);

            Password = string.Empty;
        }

        public ICommand CreateCustomerCommand { get; }
        public ICommand CancelCreateCustomerCommand { get; }
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
                    AddError(EMPTY_MESSAGE);
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
            _createCustomerCommand.Dispose();
        }

        private readonly CreateCustomerCommand _createCustomerCommand;
    }
}