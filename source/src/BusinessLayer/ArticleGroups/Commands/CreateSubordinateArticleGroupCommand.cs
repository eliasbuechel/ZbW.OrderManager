using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;
using DataLayer.ArticleGroups.Exceptions;
using DataLayer.ArticleGroups.Models;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

namespace BusinessLayer.ArticleGroups.Commands
{
    public class CreateSubordinateArticleGroupCommand : BaseAsyncCommand
    {
        public CreateSubordinateArticleGroupCommand(ManagerStore managerStore, ArticleGroupListingViewModel articleGroupListingViewModel, ArticleGroupViewModel superiorArticleGroupViewModel)
        {
            _managerStore = managerStore;
            _articleGroupListingViewModel = articleGroupListingViewModel;
            _articleGroupListingViewModel.PropertyChanged += OnArticleGroupListingViewModelPropertyChanged;
            _superiorArticleGroupViewModel = superiorArticleGroupViewModel;

            _superiorArticleGroupViewModel.PropertyChanged += OnArticleGroupViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !_articleGroupListingViewModel.NameOfToAddingArticleGroup.IsNullOrEmpty() && base.CanExecute(parameter);
        }
        public async override Task ExecuteAsync(object? parameter)
        {
            try
            {
                CreatingArticleGroup articleGroup = new CreatingArticleGroup(
                    await _managerStore.GetNextFreeArticleGroupIdAsync(),
                    _articleGroupListingViewModel.NameOfToAddingArticleGroup,
                    _superiorArticleGroupViewModel == null ? null : _superiorArticleGroupViewModel.GetArticleGroup()
                    );

                await _managerStore.CreateArticleGroup(articleGroup);
                _articleGroupListingViewModel.NameOfToAddingArticleGroup = String.Empty;
            }
            catch (NotContainingCreatingArticleGroupInDatabaseException notContainingCreatingArticleGroupInDatabaseException)
            {
                // displaying error message to the user
            }
        }

        private void OnArticleGroupListingViewModelPropertyChanged(object? sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(ArticleGroupListingViewModel.NameOfToAddingArticleGroup))
                OnCanExecuteChanged();
        }
        private void OnArticleGroupViewModelPropertyChanged(object? sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(ArticleGroupViewModel.SubordinateArticleGroups))
                OnCanExecuteChanged();
        }

        private readonly ManagerStore _managerStore;
        private readonly ArticleGroupListingViewModel _articleGroupListingViewModel;
        private readonly ArticleGroupViewModel? _superiorArticleGroupViewModel;
    }
}
