using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;

namespace BusinessLayer.Base.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService(NavigationStore navigationStore, Func<BaseViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel.Dispose();
            _navigationStore.CurrentViewModel = _createViewModel();
        }

        private readonly NavigationStore _navigationStore;
        private readonly Func<BaseViewModel> _createViewModel;
    }
}
