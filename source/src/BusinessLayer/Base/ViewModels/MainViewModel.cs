using BusinessLayer.Base.Command;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using System.Windows.Input;

namespace BusinessLayer.Base.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(NavigationStore navigationStore, NavigationService dashboardViewModelNavigationService, NavigationService customerListingViewModelNavigationService)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateToDashboardViewCommand = new NavigateCommand(dashboardViewModelNavigationService);
            NavigateToCustomerListingViewCommand = new NavigateCommand(customerListingViewModelNavigationService);
        }

        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand NavigateToDashboardViewCommand { get; }
        public ICommand NavigateToCustomerListingViewCommand { get; }

        public void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


        private readonly NavigationStore _navigationStore;
    }
}
