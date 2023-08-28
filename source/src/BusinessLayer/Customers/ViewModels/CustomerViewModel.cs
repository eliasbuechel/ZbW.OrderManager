using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.Commands;
using DataLayer.Customers.Models;
using System.Windows.Input;

namespace BusinessLayer.Customers.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel(ManagerStore managerStore, Customer customer, NavigationService editCustomerViewModelNavigationService)
        {
            _customer = customer;
            DeleteCustomerCommand = new DeleteCustomeCommand(managerStore, _customer);
            NavigateToEditCustomerCommand = new NavigateCommand(editCustomerViewModelNavigationService);
        }

        public string Id => _customer.Id.ToString();
        public string Name => _customer.FirstName + " " + _customer.LastName;
        public string Location => _customer.PostalCode + " " + _customer.City;
        public ICommand DeleteCustomerCommand { get; }
        public ICommand NavigateToEditCustomerCommand { get; }

        public bool RepresentsCustomer(Customer customer)
        {
            return ReferenceEquals(_customer, customer);
        }

        private readonly Customer _customer;
    }
}
