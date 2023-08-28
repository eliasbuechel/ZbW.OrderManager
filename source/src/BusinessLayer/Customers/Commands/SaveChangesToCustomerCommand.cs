using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.ViewModels;
using DataLayer.Customers.Models;
using System.ComponentModel;

namespace BusinessLayer.Customers.Commands
{
    internal class SaveChangesToCustomerCommand : BaseAsyncCommand
    {

        public SaveChangesToCustomerCommand(ManagerStore managerStore, Customer initialCustomer, EditCustomerViewModel editCustomerViewModel, NavigationService customerListingViewModelNavigationService)
        {
            _managerStore = managerStore;
            _initialCustomer = initialCustomer;
            _editedCustomerViewModel = editCustomerViewModel;
            _customerListingViewModelNavigationService = customerListingViewModelNavigationService;

            _editedCustomerViewModel.ErrorsChanged += OnHasCustomerPropertyErrorChanged;
        }

        private void OnHasCustomerPropertyErrorChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return !_editedCustomerViewModel.HasErrors && CustomerDataChanged() && base.CanExecute(parameter);
        }

        public async override Task ExecuteAsync(object? parameter)
        {
            Customer editedCustomer = new Customer(
                _initialCustomer.Id,
                _editedCustomerViewModel.FirstName,
                _editedCustomerViewModel.LastName,
                _editedCustomerViewModel.StreetName,
                _editedCustomerViewModel.HouseNumber,
                _editedCustomerViewModel.City,
                _editedCustomerViewModel.PostalCode,
                _editedCustomerViewModel.EmailAddress,
                _editedCustomerViewModel.WebsiteUrl,
                _editedCustomerViewModel.Password
                );

            await _managerStore.EditCustomer(_initialCustomer, editedCustomer);
            _customerListingViewModelNavigationService.Navigate();
        }

        private bool CustomerDataChanged()
        {
            return _initialCustomer.FirstName != _editedCustomerViewModel.FirstName ||
                _initialCustomer.LastName != _editedCustomerViewModel.LastName ||
                _initialCustomer.StreetName != _editedCustomerViewModel.StreetName ||
                _initialCustomer.HouseNumber != _editedCustomerViewModel.HouseNumber ||
                _initialCustomer.City != _editedCustomerViewModel.City ||
                _initialCustomer.PostalCode != _editedCustomerViewModel.PostalCode ||
                _initialCustomer.EmailAddress != _editedCustomerViewModel.EmailAddress ||
                _initialCustomer.WebsiteURL != _editedCustomerViewModel.WebsiteUrl ||
                _initialCustomer.Password != _editedCustomerViewModel.Password;


        }

        private readonly Customer _initialCustomer;
        private readonly ManagerStore _managerStore;
        private readonly NavigationService _customerListingViewModelNavigationService;
        private readonly EditCustomerViewModel _editedCustomerViewModel;
    }
}
