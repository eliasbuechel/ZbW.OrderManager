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
        }

        public ICommand CreateCustomerCommand { get; }
        public ICommand CancelCreateCustomerCommand { get; }

        public override void Dispose(bool disposing)
        {
            _createCustomerCommand.Dispose();
        }

        private readonly CreateCustomerCommand _createCustomerCommand;
    }
}