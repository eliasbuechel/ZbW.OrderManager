﻿using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Customers.Commands;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Validation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.Customers.ViewModels
{
    public class CustomerListingViewModel : BaseLoadableViewModel, ICustomerUpdatable, IMainNavigationViewModel
    {
        public static CustomerListingViewModel LoadViewModel(ManagerStore managerStore, NavigationStore navigationStore, ICustomerValidator customerValidator, IDialogService fileDialogue)
        {
            CustomerListingViewModel viewModel = new CustomerListingViewModel(managerStore, navigationStore, customerValidator, fileDialogue);
            viewModel.LoadCustomersCommand.Execute(null);
            return viewModel;
        }

        private CustomerListingViewModel(ManagerStore managerStore, NavigationStore navigationStore, ICustomerValidator customerValidator, IDialogService fileDialogue)
        {
            _managerStore = managerStore;
            _navigationStore = navigationStore;

            _customerListingViweModelNavigateBackService = new FromSubNavigationService<CustomerListingViewModel>(_navigationStore, this);

            NavigateToCreateCustomerCommand = new NavigateCommand(new ToSubNavigationService<CreateCustomerViewModel>(navigationStore, CreateCreateCustomerViewModel));
            LoadCustomersCommand = new LoadCustomersCommand(managerStore, this);
            ImportCustomersCommand = new ImportCustomersCommand(managerStore, fileDialogue);
            ExportCustomersCommand = new ExportCustomersCommand(managerStore, fileDialogue);

            managerStore.CustomerCreated += OnCustomerCreated;
            managerStore.CustomerDeleted += OnCustomerDeleted;
            _customerValidator = customerValidator;
        }

        public IEnumerable<CustomerViewModel> Customers => _customers;
        public ICommand NavigateToCreateCustomerCommand { get; }
        public ICommand LoadCustomersCommand { get; }
        public ICommand ImportCustomersCommand { get; }
        public ICommand ExportCustomersCommand { get; }

        public void UpdateCustomers(IEnumerable<CustomerDTO> customers)
        {
            _customers.Clear();

            foreach (CustomerDTO customer in customers)
            {
                CustomerViewModel customerViewModel = new CustomerViewModel(_managerStore, customer, CreateEditCustomerViewModelNavigationService(customer));
                _customers.Add(customerViewModel);
            }
        }
        public override void Dispose(bool disposing)
        {
            _managerStore.CustomerCreated -= OnCustomerCreated;
            _managerStore.CustomerDeleted -= OnCustomerDeleted;
        }

        private void OnCustomerCreated(CustomerDTO customer)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel(_managerStore, customer, CreateEditCustomerViewModelNavigationService(customer));
            _customers.Add(customerViewModel);
        }
        private void OnCustomerDeleted(CustomerDTO customer)
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
        private ToSubNavigationService<EditCustomerViewModel> CreateEditCustomerViewModelNavigationService(CustomerDTO customer)
        {
            return new ToSubNavigationService<EditCustomerViewModel>(
                _navigationStore,
                () => new EditCustomerViewModel(
                    _managerStore,
                    customer,
                    _customerListingViweModelNavigateBackService,
                    _customerValidator
                    )
                );
        }
        private CreateCustomerViewModel CreateCreateCustomerViewModel()
        {
            return new CreateCustomerViewModel(
                _managerStore,
                _customerListingViweModelNavigateBackService,
                _customerValidator
                );
        }

        private readonly ManagerStore _managerStore;
        private readonly NavigationStore _navigationStore;
        private readonly ICustomerValidator _customerValidator;
        private readonly FromSubNavigationService<CustomerListingViewModel> _customerListingViweModelNavigateBackService;
        private readonly ObservableCollection<CustomerViewModel> _customers = new ObservableCollection<CustomerViewModel>();
    }
}
