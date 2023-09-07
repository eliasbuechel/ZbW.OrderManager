using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.Articles.DTOs;
using System.ComponentModel;

namespace BusinessLayer.Articles.Commands
{
    public class SaveChangesToArticleCommand : BaseAsyncCommand
    {
        public SaveChangesToArticleCommand(ManagerStore managerStore, ArticleDTO initialArticle, EditArticleViewModel editArticleViewModel, NavigationService<ArticleListingViewModel> articleListingViewModelNavigationService)
        {
            _managerStore = managerStore;
            _initialArticle = initialArticle;
            _editArticleViewModel = editArticleViewModel;
            _articleListingViewModelNavigationService = articleListingViewModelNavigationService;

            _editArticleViewModel.ErrorsChanged += OnEditArticleViewModelErrorsChanged;
            _editArticleViewModel.PropertyChanged += OnEditArticleViewModelPropertyChanged;
        }


        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter) &&
                !_editArticleViewModel.HasErrors &&
                ArticleDataHasChanged();
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            if (_editArticleViewModel.ArticleGroup == null)
                return;

            ArticleDTO editedArticle = new ArticleDTO(
                _initialArticle.Id,
                _editArticleViewModel.Name,
                _editArticleViewModel.ArticleGroup
                );
            await _managerStore.SaveChangesToArticleAsync(_initialArticle, editedArticle);

            _articleListingViewModelNavigationService.Navigate();
        }

        private void OnEditArticleViewModelErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
        private void OnEditArticleViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
        private bool ArticleDataHasChanged()
        {
            return _initialArticle.Name != _editArticleViewModel.Name ||
                _initialArticle.ArticleGroup.Id != _editArticleViewModel.ArticleGroup.Id;
        }

        private readonly ManagerStore _managerStore;
        private readonly ArticleDTO _initialArticle;
        private readonly EditArticleViewModel _editArticleViewModel;
        private readonly NavigationService<ArticleListingViewModel> _articleListingViewModelNavigationService;
    }
}
