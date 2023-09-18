using BusinessLayer.ArticleGroups.Models;
using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Customers.ViewModels;
using BusinessLayer.Dashboard.ViewModels;
using BusinessLayer.Orders.ViewModels;
using Microsoft.Identity.Client;
using System.Windows.Input;

namespace BusinessLayer.Base.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(NavigationStore navigationStore, NavigationService<DashboardViewModel> dashboardViewModelNavigationService, NavigationService<CustomerListingViewModel> customerListingViewModelNavigationService, NavigationService<ArticleGroupListingViewModel> articleGroupListingViewModelNavigationService, NavigationService<ArticleListingViewModel> articleListingViewModelNavigationService, NavigationService<OrderListingViewModel> orderListingViewModelNavigationService)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateToDashboardViewCommand = _navigateToDashboardViewCommand = new MainNavigateCommand(navigationStore, dashboardViewModelNavigationService);
            NavigateToCustomerListingViewCommand = _navigateToCustomerListingViewCommand = new MainNavigateCommand(navigationStore, customerListingViewModelNavigationService);
            NavigateToArticleGroupListingViewCommand = _navigateToArticleGroupListingViewCommand = new MainNavigateCommand(navigationStore, articleGroupListingViewModelNavigationService);
            NavigateToArticleListingViewCommand = _navigateToArticleListingViewCommand = new MainNavigateCommand(navigationStore, articleListingViewModelNavigationService);
            NavigateToOrderListingViewCommand = _navigateToOrderListingViewCommand = new MainNavigateCommand(navigationStore, orderListingViewModelNavigationService);
        }

        public BaseViewModel? CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand NavigateToDashboardViewCommand { get; }
        public ICommand NavigateToCustomerListingViewCommand { get; }
        public ICommand NavigateToArticleGroupListingViewCommand { get; }
        public ICommand NavigateToArticleListingViewCommand { get; }
        public ICommand NavigateToOrderListingViewCommand { get; }

        public void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public override void Dispose(bool disposing)
        {
            _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;

            _navigateToDashboardViewCommand.Dispose();
            _navigateToCustomerListingViewCommand.Dispose();
            _navigateToArticleGroupListingViewCommand.Dispose();
            _navigateToArticleListingViewCommand.Dispose();
            _navigateToOrderListingViewCommand.Dispose();
        }

        private readonly NavigationStore _navigationStore;
        private readonly MainNavigateCommand _navigateToDashboardViewCommand;
        private readonly MainNavigateCommand _navigateToCustomerListingViewCommand;
        private readonly MainNavigateCommand _navigateToArticleGroupListingViewCommand;
        private readonly MainNavigateCommand _navigateToArticleListingViewCommand;
        private readonly MainNavigateCommand _navigateToOrderListingViewCommand;
    }
}
