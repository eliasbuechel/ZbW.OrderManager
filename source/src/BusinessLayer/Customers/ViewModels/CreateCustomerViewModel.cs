using BusinessLayer.Base.Command;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.Commands;
using System.Windows.Input;

namespace BusinessLayer.Customers.ViewModels
{
    public class CreateCustomerViewModel : BaseCustomerFieldsViewModel
    {
        public ICommand CreateCustomerCommand { get; }
        public ICommand CancelCreateCustomerCommand { get; }

        public CreateCustomerViewModel(ManagerStore managerStore, NavigationService customerListingViewModelNavigationService)
        {
            CreateCustomerCommand = new CreateCustomerCommand(managerStore, this, customerListingViewModelNavigationService);
            CancelCreateCustomerCommand = new NavigateCommand(customerListingViewModelNavigationService);

            FirstName = string.Empty;
            LastName = string.Empty;
            StreetName = string.Empty;
            HouseNumber = string.Empty;
            PostalCode = string.Empty;
            City = string.Empty;
            EmailAddress = string.Empty;
            WebsiteUrl = string.Empty;
            Password = string.Empty;
        }
    }
}