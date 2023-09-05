using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using Microsoft.Identity.Client;
using System.Windows.Input;

namespace BusinessLayer.Base.ViewModels
{
    public class MainViewModel : BaseViewModel, IDisposable
    {
        public MainViewModel(NavigationStore navigationStore, NavigationService dashboardViewModelNavigationService, NavigationService customerListingViewModelNavigationService, NavigationService articleGroupListingViewModelNavigationService, NavigationService articleListingViewModelNavigationService, NavigationService orderListingViewModelNavigationService)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateToDashboardViewCommand = new NavigateCommand(dashboardViewModelNavigationService);
            NavigateToCustomerListingViewCommand = new NavigateCommand(customerListingViewModelNavigationService);
            NavigateToArticleGroupListingViewCommand = new NavigateCommand(articleGroupListingViewModelNavigationService);
            NavigateToArticleListingViewCommand = new NavigateCommand(articleListingViewModelNavigationService);
            NavigateToOrderListingViewCommand = new NavigateCommand(orderListingViewModelNavigationService);
        }

        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand NavigateToDashboardViewCommand { get; }
        public ICommand NavigateToCustomerListingViewCommand { get; }
        public ICommand NavigateToArticleGroupListingViewCommand { get; }
        public ICommand NavigateToArticleListingViewCommand { get; }
        public ICommand NavigateToOrderListingViewCommand { get; }

        public void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public override void Dispose()
        {
            _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
            base.Dispose();
        }


        private readonly NavigationStore _navigationStore;
    }
}
