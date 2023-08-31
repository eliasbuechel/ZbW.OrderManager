using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.Commands;
using DataLayer.Customers.DTOs;
using System.Windows.Input;

namespace BusinessLayer.Customers.ViewModels
{
    public class EditCustomerViewModel : BaseCustomerListingViewModel
    {
        public EditCustomerViewModel(ManagerStore managerStore, CustomerDTO customer, NavigationService createCustomerListingViewModelNavigationService)
        {
            SaveChangedToCustomerCommand = new SaveChangesToCustomerCommand(managerStore, customer, this, createCustomerListingViewModelNavigationService);
            CancelEditCustomerCommand = new NavigateCommand(createCustomerListingViewModelNavigationService);

            Id = customer.Id.ToString();
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            StreetName = customer.StreetName;
            HouseNumber = customer.HouseNumber;
            PostalCode = customer.PostalCode;
            City = customer.City;
            EmailAddress = customer.EmailAddress;
            WebsiteUrl = customer.WebsiteURL;
            Password = customer.Password;
        }

        public string Id { get; }
        public ICommand SaveChangedToCustomerCommand { get; }
        public ICommand CancelEditCustomerCommand { get; }
    }
}
