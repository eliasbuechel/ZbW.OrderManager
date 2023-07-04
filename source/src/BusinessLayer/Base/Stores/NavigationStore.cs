using BusinessLayer.Base.ViewModels;

namespace BusinessLayer.Base.Stores
{
    public class NavigationStore
    {
        private BaseViewModel m_CurrentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => m_CurrentViewModel;
            set
            {
                m_CurrentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        public void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
