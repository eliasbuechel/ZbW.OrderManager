using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using System.Diagnostics;

namespace BusinessLayer.Base.Services
{
    public class SubNavigationService<TBaseViewModel, TSubViewModel> : INavigationService where TBaseViewModel : BaseViewModel where TSubViewModel : BaseViewModel
    {
        public SubNavigationService(NavigationStore navigationStore, TBaseViewModel currentViewModel, Func<TSubViewModel> subViewModelCreationMethode)
        {
            _navigationStore = navigationStore;
            _currentViewModel = currentViewModel;
            _subViewModelCreationMethode = subViewModelCreationMethode;
        }

        public void Navigate()
        {
            if (_navigationStore.CurrentViewModel.Equals(_currentViewModel))
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
            _navigationStore.CurrentViewModel?.Dispose();
            _navigationStore.CurrentViewModel = _currentViewModel;
        }

        private readonly NavigationStore _navigationStore;
        private readonly TBaseViewModel _currentViewModel;
        private readonly Func<TSubViewModel> _subViewModelCreationMethode;
    }
}
