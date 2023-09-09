﻿using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.ViewModels;
using DataLayer.Customers.DTOs;
using System.ComponentModel;

namespace BusinessLayer.Customers.Commands
{
    public sealed class SaveChangesToCustomerCommand : BaseAsyncCommand, IDisposable
    {
        public SaveChangesToCustomerCommand(ManagerStore managerStore, CustomerDTO initialCustomer, EditCustomerViewModel editCustomerViewModel, FromSubNavigationService<CustomerListingViewModel> customerListingViweModelNavigateBackService)
        {
            _managerStore = managerStore;
            _initialCustomer = initialCustomer;
            _editedCustomerViewModel = editCustomerViewModel;
            _customerListingViweModelNavigateBackService = customerListingViweModelNavigateBackService;

            _editedCustomerViewModel.ErrorsChanged += OnHasCustomerPropertyErrorChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !_editedCustomerViewModel.HasErrors && CustomerDataChanged() && base.CanExecute(parameter);
        }
        public async override Task ExecuteAsync(object? parameter)
        {
            CustomerDTO editedCustomer = new CustomerDTO(
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

            await _managerStore.EditCustomerAsync(_initialCustomer, editedCustomer);
            _customerListingViweModelNavigateBackService.Navigate();
        }
        public void Dispose()
        {
            _editedCustomerViewModel.ErrorsChanged -= OnHasCustomerPropertyErrorChanged;
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
        private void OnHasCustomerPropertyErrorChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        private readonly CustomerDTO _initialCustomer;
        private readonly ManagerStore _managerStore;
        private readonly EditCustomerViewModel _editedCustomerViewModel;
        private readonly FromSubNavigationService<CustomerListingViewModel> _customerListingViweModelNavigateBackService;
    }
}
