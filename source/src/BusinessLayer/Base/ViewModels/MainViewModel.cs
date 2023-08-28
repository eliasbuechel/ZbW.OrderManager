using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using Microsoft.Identity.Client;
using System.Windows.Input;

namespace BusinessLayer.Base.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(NavigationStore navigationStore, NavigationService dashboardViewModelNavigationService, NavigationService customerListingViewModelNavigationService, NavigationService articleGroupListingViewModelNavigationService, NavigationService articleListingViewModelNavigationService)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateToDashboardViewCommand = new NavigateCommand(dashboardViewModelNavigationService);
            NavigateToCustomerListingViewCommand = new NavigateCommand(customerListingViewModelNavigationService);
            NavigateToArticleGroupListingViewCommand = new NavigateCommand(articleGroupListingViewModelNavigationService);
            NavigateToArticleListingViewCommand = new NavigateCommand(articleListingViewModelNavigationService);
        }

        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand NavigateToDashboardViewCommand { get; }
        public ICommand NavigateToCustomerListingViewCommand { get; }
        public ICommand NavigateToArticleGroupListingViewCommand { get; }
        public ICommand NavigateToArticleListingViewCommand { get; }

        public void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


        private readonly NavigationStore _navigationStore;
    }
}
