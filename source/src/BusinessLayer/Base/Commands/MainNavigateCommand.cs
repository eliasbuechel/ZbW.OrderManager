using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;

namespace BusinessLayer.Base.Commands
{
    public class MainNavigateCommand : NavigateCommand, IDisposable
    {

        public MainNavigateCommand(NavigationStore navigationStore, INavigationService navigationService) : base(navigationService)
        {
            _navigationStore = navigationStore;
            navigationStore.CurrentViewModelChanged += OnCanExecuteChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && _navigationStore.CurrentViewModel is IMainNavigationViewModel;
        }

        public void Dispose()
        {
            _navigationStore.CurrentViewModelChanged += OnCanExecuteChanged;
        }

        private readonly NavigationStore _navigationStore;
    }
}
