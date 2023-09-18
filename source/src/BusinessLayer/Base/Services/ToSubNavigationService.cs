using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using System.Diagnostics;

namespace BusinessLayer.Base.Services
{
    public class ToSubNavigationService<TSubViewModel> : INavigationService where TSubViewModel : BaseViewModel
    {
        public ToSubNavigationService(NavigationStore navigationStore, Func<TSubViewModel> createSubViewModel)
        {
            _navigationStore = navigationStore;
            _createSubViewModel = createSubViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createSubViewModel();
        }

        private readonly NavigationStore _navigationStore;
        private readonly Func<TSubViewModel> _createSubViewModel;
    }
}
