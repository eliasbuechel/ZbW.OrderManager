using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.Commands;
using DataLayer.Customers.DTOs;
using System.Windows.Input;

namespace BusinessLayer.Customers.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel(ManagerStore managerStore, CustomerDTO customer, NavigationService editCustomerViewModelNavigationService)
        {
            _customer = customer;
            DeleteCustomerCommand = new DeleteCustomeCommand(managerStore, _customer);
            NavigateToEditCustomerCommand = new NavigateCommand(editCustomerViewModelNavigationService);
        }

        public ICommand DeleteCustomerCommand { get; }
        public ICommand NavigateToEditCustomerCommand { get; }
        public string Id => _customer.Id.ToString();
        public string Name => _customer.FirstName + " " + _customer.LastName;
        public string Location => _customer.PostalCode + " " + _customer.City;

        public bool RepresentsCustomer(CustomerDTO customer)
        {
            return ReferenceEquals(_customer, customer);
        }

        private readonly CustomerDTO _customer;
    }
}
