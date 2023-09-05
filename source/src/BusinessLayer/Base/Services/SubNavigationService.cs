using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using System.Diagnostics;

namespace BusinessLayer.Base.Services
{
    public class SubNavigationService : INavigationService
    {
        public SubNavigationService(NavigationStore navigationStore, BaseViewModel currentViewModel, Func<BaseViewModel> subViewModelCreationMethode)
        {
            _navigationStore = navigationStore;
            _currentViewModel = currentViewModel;
            _subViewModelCreationMethode = subViewModelCreationMethode;
        }

        public void Navigate()
        {
            if (_navigationStore.CurrentViewModel == _currentViewModel)
                NavigateToSub();
            else
                NavigateBack();
        }

        private void NavigateToSub()
        {
            _navigationStore.CurrentViewModel = _subViewModelCreationMethode();
        }
        private void NavigateBack()
        {
            _navigationStore.CurrentViewModel.Dispose();
            _navigationStore.CurrentViewModel = _currentViewModel;
        }

        private readonly NavigationStore _navigationStore;
        private readonly BaseViewModel _currentViewModel;
        private readonly Func<BaseViewModel> _subViewModelCreationMethode;
    }
}
