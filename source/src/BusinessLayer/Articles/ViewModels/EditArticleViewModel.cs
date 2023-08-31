using BusinessLayer.Articles.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.Articles.DTOs;
using System.Windows.Input;

namespace BusinessLayer.Articles.ViewModels
{
    public class EditArticleViewModel : BaseCreateEditArticleViewModel
    {
        public static EditArticleViewModel LoadViewModel(ManagerStore managerStore, ArticleDTO article, NavigationService articleListingViewModelNavigationService)
        {
            EditArticleViewModel viewModel = new EditArticleViewModel(managerStore, article, articleListingViewModelNavigationService);
            viewModel.LoadArticleGroupsCommand?.Execute(null);
            return viewModel;
        }

        private EditArticleViewModel(ManagerStore managerStore, ArticleDTO article, NavigationService articleListingViewModelNavigationService)
            : base(managerStore, article.Name, article.ArticleGroup)
        {
            _initialArticle = article;

            SaveChangesToArticleCommand = new SaveChangesToArticleCommand(managerStore, article, this, articleListingViewModelNavigationService);
            CancelEditArticleCommand = new NavigateCommand(articleListingViewModelNavigationService);
        }

        public ICommand SaveChangesToArticleCommand { get; }
        public ICommand CancelEditArticleCommand { get; }
        public int Id => _initialArticle.Id;

        private readonly ArticleDTO _initialArticle;
    }
}
