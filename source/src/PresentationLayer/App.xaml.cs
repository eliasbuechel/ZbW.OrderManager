using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Customers.ViewModels;
using BusinessLayer.Dashboard.ViewModels;
using DataLayer.Base.DatabaseContext;
using DataLayer.Base.Models;
using DataLayer.Customers.Models;
using DataLayer.Customers.Services.CustomerCreators;
using DataLayer.Customers.Services.CustomerDeletors;
using DataLayer.Customers.Services.CustomerEditors;
using DataLayer.Customers.Services.CustomerProviders;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace PresentationLayer
{
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Server=.\\SQLSERVER_EB;Database=OrderManagerTestV2;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False;";
        private readonly Manager _manager;
        private readonly ManagerStore _managerStore;
        private readonly NavigationStore _navigationStore;
        private readonly OrderDbContextFactory _orderDbContextFactory;
        private readonly NavigationService _dashboardViewModelNavigationService;
        private readonly NavigationService _customerListingViewModelNavigationService;

        public App()
        {
            _orderDbContextFactory = new OrderDbContextFactory(CONNECTION_STRING);

            ICustomerProvider customerProvider = new DatabaseCustomerProvider(_orderDbContextFactory);
            ICustomerCreator customerCreator = new DatabaseCustomerCreator(_orderDbContextFactory);
            ICustomerDeletor cusotmerDeletor = new DatabaseCustomerDeletor(_orderDbContextFactory);
            ICustomerEditor customerEditor = new DatabaseCustomerEditor(_orderDbContextFactory);

            CustomerList customerList = new CustomerList(customerProvider, customerCreator, cusotmerDeletor, customerEditor);

            _manager = new Manager(customerList);
            _managerStore = new ManagerStore(_manager);
            _navigationStore = new NavigationStore();

            _dashboardViewModelNavigationService = new NavigationService(_navigationStore, CreateDashboardViewModel);
            _customerListingViewModelNavigationService = new NavigationService(_navigationStore, CreateCustomerListingViewModel);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using OrderDbContext dbContext = _orderDbContextFactory.CreateDbContext();
            dbContext.Database.Migrate();

            _navigationStore.CurrentViewModel = CreateCustomerListingViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore,_dashboardViewModelNavigationService, _customerListingViewModelNavigationService)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private DashboardViewModel CreateDashboardViewModel()
        {
            return DashboardViewModel.LoadViewModel();
        }

        private CustomerListingViewModel CreateCustomerListingViewModel()
        {
            return CustomerListingViewModel.LoadViewModel(_managerStore, _navigationStore, new NavigationService(_navigationStore, CreateCreateCustomerViewModel), _customerListingViewModelNavigationService);
        }

        private CreateCustomerViewModel CreateCreateCustomerViewModel()
        {
            return new CreateCustomerViewModel(_managerStore, _customerListingViewModelNavigationService);
        }
    }
}
