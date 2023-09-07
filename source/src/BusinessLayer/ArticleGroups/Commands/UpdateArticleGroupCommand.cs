using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Exceptions;
using System.ComponentModel;

namespace BusinessLayer.ArticleGroups.Commands
{
    public class UpdateArticleGroupCommand : BaseAsyncCommand, IDisposable
    {
        public UpdateArticleGroupCommand(ManagerStore managerStore, EditArticleGroupViewModel editArticleGroupViewModel, ArticleGroupDTO initialArticleGroup, NavigationService<ArticleGroupListingViewModel> articleGroupListingNavigationService)
        {
            _managerStore = managerStore;
            _editArticleGroupViewModel = editArticleGroupViewModel;
            _initialArticleGroup = initialArticleGroup;
            _articleGroupListingNavigationService = articleGroupListingNavigationService;
            _editArticleGroupViewModel.ErrorsChanged += OnEditArticleGroupViewModelErrorsChanged;
            _editArticleGroupViewModel.PropertyChanged += OnEditArticleGroupViewModelPropertyChanged;
        }


        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && !_editArticleGroupViewModel.HasErrors
                && HasArticleGroupDataChanged();
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            CreatedOrUpdatedArticleGroupDTO articleGroup = new CreatedOrUpdatedArticleGroupDTO(_editArticleGroupViewModel.Id, _editArticleGroupViewModel.Name, _editArticleGroupViewModel.IsRootElement ? null : _editArticleGroupViewModel.SuperiorArticleGroup);

            try
            {
                await _managerStore.UpdateArticleGroupAsync(articleGroup, _initialArticleGroup);
            }
            catch (NotAffectedDataStorageException e)
            {
                string errorMessage = e.Message;

                if (e.InnerException != null && !string.IsNullOrEmpty(e.InnerException.Message))
                {
                    errorMessage += $"{Environment.NewLine}{Environment.NewLine}More information:{Environment.NewLine}{e.InnerException.Message}";
                }

                _editArticleGroupViewModel.ErrorMessage = errorMessage;
                return;
            }

            _articleGroupListingNavigationService.Navigate();
        }
        public void Dispose()
        {
            _editArticleGroupViewModel.ErrorsChanged -= OnEditArticleGroupViewModelErrorsChanged;
        }

        private void OnEditArticleGroupViewModelErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
        private void OnEditArticleGroupViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
        private bool HasArticleGroupDataChanged()
        {
            if (_editArticleGroupViewModel.Name != _initialArticleGroup.Name)
                return true;

            if (_editArticleGroupViewModel.SuperiorArticleGroup == null || _editArticleGroupViewModel.IsRootElement)
                return _initialArticleGroup.SuperiorArticleGroup != null;

            if (_initialArticleGroup.SuperiorArticleGroup == null)
                return true;

            return _editArticleGroupViewModel.SuperiorArticleGroup.Id != _initialArticleGroup.SuperiorArticleGroup.Id;
        }

        private readonly ManagerStore _managerStore;
        private readonly ArticleGroupDTO _initialArticleGroup;
        private readonly NavigationService<ArticleGroupListingViewModel> _articleGroupListingNavigationService;
        private readonly EditArticleGroupViewModel _editArticleGroupViewModel;
    }
}
