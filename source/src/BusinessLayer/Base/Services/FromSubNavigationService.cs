using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;

namespace BusinessLayer.Base.Services
{
    public class FromSubNavigationService<TParentViewModel> : INavigationService where TParentViewModel : BaseViewModel
    {
        public FromSubNavigationService(NavigationStore navigationStore, TParentViewModel parentViewModel)
        {
            _navigationStore = navigationStore;
            _parentViewModel = parentViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel?.Dispose();
            _navigationStore.CurrentViewModel = _parentViewModel;
        }

        private readonly NavigationStore _navigationStore;
        private readonly TParentViewModel _parentViewModel;
    }
}