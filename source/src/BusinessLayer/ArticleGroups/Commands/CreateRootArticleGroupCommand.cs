using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;
using DataLayer.ArticleGroups.Exceptions;
using DataLayer.ArticleGroups.Models;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

namespace BusinessLayer.ArticleGroups.Commands
{
    public class CreateRootArticleGroupCommand : BaseAsyncCommand
    {
        public CreateRootArticleGroupCommand(ManagerStore managerStore, ArticleGroupListingViewModel articleGroupListingViewModel)
        {
            _managerStore = managerStore;
            _articleGroupListingViewModel = articleGroupListingViewModel;

            _articleGroupListingViewModel.PropertyChanged += OnArticleGroupListingViewModelPropertyChanged;
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
                    _articleGroupListingViewModel.NameOfToAddingArticleGroup
                    );

                await _managerStore.CreateArticleGroup(articleGroup);
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

        private readonly ManagerStore _managerStore;
        private readonly ArticleGroupListingViewModel _articleGroupListingViewModel;
    }
}
