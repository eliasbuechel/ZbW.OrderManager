using BusinessLayer.Base.Command;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Customers.Commands;
using DataLayer.Customers.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.Customers.ViewModels
{
    public class CustomerListingViewModel : BaseViewModel
    {
        public CustomerListingViewModel(ManagerStore managerStore, NavigationStore navigationStore, NavigationService createCustomerViewModelNavigationService, NavigationService customerListingViewModelNavigationService)
        {
            _customers = new ObservableCollection<CustomerViewModel>();
            _errorMessage = string.Empty;

            _navigationStore = navigationStore;
            _managerStore = managerStore;
            _managerStore.CustomerCreated += OnCustomerCreated;
            _managerStore.CustomerDeleted += OnCustomerDeleted;

            _customerListingViewModelNavigationService = customerListingViewModelNavigationService;

            NavigateToCreateCustomerCommand = new NavigateCommand(createCustomerViewModelNavigationService);
            LoadCustomersCommand = new LoadCustomersCommand(managerStore, this);
        }

        public IEnumerable<CustomerViewModel> Customers => _customers;
        public ICommand NavigateToCreateCustomerCommand { get; }
        public ICommand LoadCustomersCommand { get; }
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);


        public void UpdateCustomers(IEnumerable<Customer> customers)
        {
            _customers.Clear();

            foreach (Customer customer in customers)
            {
                CustomerViewModel customerViewModel = new CustomerViewModel(_managerStore, customer, CreateEditCustomerViewModelNavigationService(customer));
                _customers.Add(customerViewModel);
            }
        }
        public override void Dispose()
        {
            _managerStore.CustomerCreated -= OnCustomerCreated;
            _managerStore.CustomerDeleted -= OnCustomerDeleted;
            base.Dispose();
        }

        public static CustomerListingViewModel LoadViewModel(ManagerStore managerStore, NavigationStore navigationStore, NavigationService createCustomerViewModelNavigationService, NavigationService customerListingViewModelNavigationService)
        {
            CustomerListingViewModel viewModel = new CustomerListingViewModel(managerStore, navigationStore, createCustomerViewModelNavigationService, customerListingViewModelNavigationService);
            viewModel.LoadCustomersCommand.Execute(null);
            return viewModel;
        }

        private void OnCustomerCreated(Customer customer)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel(_managerStore, customer, CreateEditCustomerViewModelNavigationService(customer));
            _customers.Add(customerViewModel);
        }
        private void OnCustomerDeleted(Customer customer)
        {
            CustomerViewModel? customerViewModel = null;
            foreach (CustomerViewModel c in _customers)
            {
                if (c.RepresentsCustomer(customer))
                {
                    customerViewModel = c;
                    break;
                }
            }

            if (customerViewModel == null)
                return;

            _customers.Remove(customerViewModel);
        }
        private NavigationService CreateEditCustomerViewModelNavigationService(Customer customer)
        {
            return new NavigationService(_navigationStore, () => new EditCustomerViewModel(_managerStore, customer, _customerListingViewModelNavigationService));
        }

        private bool _isLoading;
        private string _errorMessage;

        private readonly ObservableCollection<CustomerViewModel> _customers;
        private readonly NavigationStore _navigationStore;
        private readonly ManagerStore _managerStore;
        private readonly NavigationService _customerListingViewModelNavigationService;
    }
}
