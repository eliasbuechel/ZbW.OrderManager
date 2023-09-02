using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.ArticleGroups.Exceptions;
using DataLayer.Articles.DTOs;
using System.ComponentModel;

namespace BusinessLayer.Articles.Commands
{
    public class CreateArticleCommand : BaseAsyncCommand, IDisposable
    {
        public CreateArticleCommand(ManagerStore managerStore, CreateArticleViewModel createArticleViewModel, NavigationService articleListingViewMoelNavigationService)
        {
            _managerStore = managerStore;
            _createArticleViewModel = createArticleViewModel;
            _articleListingViewMoelNavigationService = articleListingViewMoelNavigationService;

            createArticleViewModel.ErrorsChanged += OnCreateArticleViewModelErrorsChanged;
        }


        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter) &&
                !_createArticleViewModel.HasErrors;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            if (_createArticleViewModel.ArticleGroup == null)
            {
                _createArticleViewModel.ErrorMessage = "Not able to create article with no article group seledted!";
                return;
            }

            ArticleDTO article = new ArticleDTO(
                await _managerStore.GetNextFreeArticleIdAsync(),
                _createArticleViewModel.Name,
                _createArticleViewModel.ArticleGroup
                );

            try
            {
                await _managerStore.CreateArticleAsync(article);
            }
            catch (NotContainingArticleGroupInDatabaseException e)
            {
                _createArticleViewModel.ErrorMessage = $"Not able to create article! {e.Message}";
                return;
            }

            _articleListingViewMoelNavigationService.Navigate();
        }
        public void Dispose()
        {
            _createArticleViewModel.ErrorsChanged -= OnCreateArticleViewModelErrorsChanged;
        }

        private void OnCreateArticleViewModelErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        private readonly ManagerStore _managerStore;
        private readonly CreateArticleViewModel _createArticleViewModel;
        private readonly NavigationService _articleListingViewMoelNavigationService;
    }
}
