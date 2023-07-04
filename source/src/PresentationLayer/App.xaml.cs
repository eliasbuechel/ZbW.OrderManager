using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Customers.ViewModels;
using BusinessLayer.Dashboard.ViewModels;
using System.Windows;

namespace PresentationLayer
{
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly NavigationService _dashboardViewModelNavigationService;
        private readonly NavigationService _customerListingViewModelNavigationService;

        public App()
        {
            _navigationStore = new NavigationStore();

            _dashboardViewModelNavigationService = new NavigationService(_navigationStore, CreateDashboardViewModel);
            _customerListingViewModelNavigationService = new NavigationService(_navigationStore, CreateCustomerListingViewModel);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new DashboardViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, _dashboardViewModelNavigationService, _customerListingViewModelNavigationService)
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
            return CustomerListingViewModel.LoadViewModel();
        }
    }
}
