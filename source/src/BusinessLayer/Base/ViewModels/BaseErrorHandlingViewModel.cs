using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BusinessLayer.Base.ViewModels
{
    public abstract class BaseErrorHandlingViewModel : BaseViewModel, INotifyDataErrorInfo, IErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null)
                return new List<string>();

            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        protected void AddError(string errorMessage, [CallerMemberName] string propertyName = null)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }
        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        protected void ClearErrors([CallerMemberName] string propertyName = null)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }
        public bool HasErrorMessage => ErrorMessage.IsNullOrEmpty();
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        protected static string ToLongErrorMessage(int maxSize)
        {
            return $"Cannot be larger than {maxSize} characters.";
        }

        protected const string EMPTY_MESSAGE = "Cannot be empty.";

        private string _errorMessage = string.Empty;
        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
    }
}
