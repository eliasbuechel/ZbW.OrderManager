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
        public CreateCustomerViewModel(ManagerStore managerStore, NavigationService customerListingViewModelNavigationService, ICustomerValidator customerValidator)
            : base(customerValidator)
        {
            CreateCustomerCommand = new CreateCustomerCommand(managerStore, this, customerListingViewModelNavigationService);
            CancelCreateCustomerCommand = new NavigateCommand(customerListingViewModelNavigationService);
        }

        public ICommand CreateCustomerCommand { get; }
        public ICommand CancelCreateCustomerCommand { get; }

    }
}