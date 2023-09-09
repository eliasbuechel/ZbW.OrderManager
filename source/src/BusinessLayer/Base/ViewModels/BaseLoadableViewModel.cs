namespace BusinessLayer.Base.ViewModels
{
    public abstract class BaseLoadableViewModel : BaseErrorHandlingViewModel, ILoadable
    {
        protected BaseLoadableViewModel()
        {}

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
    }
}
