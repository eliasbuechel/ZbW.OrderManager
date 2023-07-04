using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;

namespace BusinessLayer.Base.Services
{
    public class NavigationService
    {
        public NavigationService(NavigationStore navigationStore, Func<BaseViewModel> createViewModel)
        {
            _NavigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _NavigationStore.CurrentViewModel.Dispose();
            _NavigationStore.CurrentViewModel = _createViewModel();
        }

        private readonly NavigationStore _NavigationStore;
        private readonly Func<BaseViewModel> _createViewModel;
    }
}
