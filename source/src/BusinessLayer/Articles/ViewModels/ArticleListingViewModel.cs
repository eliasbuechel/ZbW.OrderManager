using BusinessLayer.Articles.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.Articles.DTOs;
using DataLayer.Articles.Validation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.Articles.ViewModels
{
    public class ArticleListingViewModel : BaseLoadableViewModel, IArticleUpdatable
    {
        public static ArticleListingViewModel LoadViewModel(ManagerStore managerStore, NavigationStore navigationStore, NavigationService articleListingViewModelNavigationService,  NavigationService createArticleViewModelNavigationService, IArticleValidator articleValidator)
        {
            ArticleListingViewModel viewModel = new ArticleListingViewModel(managerStore,navigationStore, articleListingViewModelNavigationService, createArticleViewModelNavigationService, articleValidator);
            viewModel.LoadArticlesCommand?.Execute(null);

            return viewModel;
        }

        private ArticleListingViewModel(ManagerStore managerStore, NavigationStore navigationStore, NavigationService articleListingViewModelNavigationService, NavigationService createArticleViewModelNavigationService, IArticleValidator articleValidator)
        {
            NavigateToCreateArticleCommand = new NavigateCommand(createArticleViewModelNavigationService);
            LoadArticlesCommand = new LoadArticlesCommand(managerStore, this);
            _managerStore = managerStore;
            _navigationStore = navigationStore;
            _articleListingViewModelNavigationService = articleListingViewModelNavigationService;
            managerStore.ArticleCreated += OnArticleCreated;
            managerStore.ArticleDeleted += OnArticleDeleted;
            _articleValidator = articleValidator;
        }

        public IEnumerable<ArticleViewModel> Articles => _articles;
        public ICommand NavigateToCreateArticleCommand { get; }
        public ICommand LoadArticlesCommand { get; }

        public void UpdateArticles(IEnumerable<ArticleDTO> articles)
        {
            _articles.Clear();

            foreach (var article in articles)
            {
                ArticleViewModel viewModel = new ArticleViewModel(_managerStore, article, CreateEditArticleNavigationService(article));
                _articles.Add(viewModel);
            }
        }
        public override void Dispose()
        {
            _managerStore.ArticleCreated -= OnArticleCreated;
            _managerStore.ArticleDeleted -= OnArticleDeleted;

            base.Dispose();
        }

        private NavigationService CreateEditArticleNavigationService(ArticleDTO article)
        {
            return new NavigationService(_navigationStore, () => EditArticleViewModel.LoadViewModel(_managerStore, article, _articleListingViewModelNavigationService, _articleValidator));
        }
        private void OnArticleDeleted(ArticleDTO article)
        {
            foreach (ArticleViewModel a in _articles)
            {
                if (a.Represents(article))
                {
                    _articles.Remove(a);
                    return;
                }
            }
        }
        private void OnArticleCreated(ArticleDTO article)
        {
            ArticleViewModel articleViewModel = new ArticleViewModel(_managerStore, article, CreateEditArticleNavigationService(article));
            _articles.Add(articleViewModel);
        }

        private readonly ManagerStore _managerStore;
        private readonly NavigationStore _navigationStore;
        private readonly NavigationService _articleListingViewModelNavigationService;
        private readonly ObservableCollection<ArticleViewModel> _articles = new ObservableCollection<ArticleViewModel>();
        private readonly IArticleValidator _articleValidator;
    }
}
