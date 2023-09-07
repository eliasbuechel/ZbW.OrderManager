using BusinessLayer.Base.ViewModels;

namespace BusinessLayer.Base.Stores
{
    public class NavigationStore
    {
        public event Action? CurrentViewModelChanged;
        public BaseViewModel? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        private BaseViewModel? _currentViewModel;
    }
}