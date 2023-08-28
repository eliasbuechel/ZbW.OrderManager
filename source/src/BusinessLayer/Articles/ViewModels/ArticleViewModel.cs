using BusinessLayer.Articles.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.Articles.Models;
using System.Windows.Input;

namespace BusinessLayer.Articles.ViewModels
{
    public class ArticleViewModel : BaseViewModel
    {
        public ArticleViewModel(ManagerStore managerStore, Article article, NavigationService editArticleViewModelNavigationService)
        {
            _article = article;
            NavigateToEditArticleCommand = new NavigateCommand(editArticleViewModelNavigationService);
            DeleteArticleCommand = new DeleteArticleCommand(managerStore, article);
        }

        public ICommand NavigateToEditArticleCommand { get; }
        public ICommand DeleteArticleCommand { get; }

        public string Id => _article.Id.ToString();
        public string Name => _article.Name;
        public string ArticleGroup => _article.ArticleGroup.Name;

        public bool RepresentsArticle(Article article)
        {
            return ReferenceEquals(article, _article);
        }

        private readonly Article _article;
    }
}