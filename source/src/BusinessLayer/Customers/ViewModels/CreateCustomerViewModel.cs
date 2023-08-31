using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.Commands;
using System.Windows.Input;

namespace BusinessLayer.Customers.ViewModels
{
    public class CreateCustomerViewModel : BaseCustomerListingViewModel
    {
        public CreateCustomerViewModel(ManagerStore managerStore, NavigationService customerListingViewModelNavigationService)
        {
            CreateCustomerCommand = new CreateCustomerCommand(managerStore, this, customerListingViewModelNavigationService);
            CancelCreateCustomerCommand = new NavigateCommand(customerListingViewModelNavigationService);
        }

        public ICommand CreateCustomerCommand { get; }
        public ICommand CancelCreateCustomerCommand { get; }

    }
}