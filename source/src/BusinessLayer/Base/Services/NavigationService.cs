using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;

namespace BusinessLayer.Base.Services
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : BaseViewModel
    {
        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel?.Dispose();
            _navigationStore.CurrentViewModel = _createViewModel();
        }

        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
    }
}
