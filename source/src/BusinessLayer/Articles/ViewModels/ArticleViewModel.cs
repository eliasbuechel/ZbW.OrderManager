using BusinessLayer.Articles.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.Articles.DTOs;
using System.Windows.Input;

namespace BusinessLayer.Articles.ViewModels
{
    public class ArticleViewModel : BaseViewModel
    {
        public ArticleViewModel(ManagerStore managerStore, ArticleDTO article, NavigationService editArticleViewModelNavigationService)
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

        public bool Represents(ArticleDTO article)
        {
            return article.Id == _article.Id;
        }

        private readonly ArticleDTO _article;
    }
}