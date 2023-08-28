using BusinessLayer.Articles.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.Articles.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.Articles.ViewModels
{
    public class ArticleListingViewModel : BaseViewModel
    {
        public ArticleListingViewModel(ManagerStore managerStore, NavigationStore navigationStore, NavigationService articleListingViewModelNavigationService, NavigationService createArticleViewModelNavigationService)
        {
            NavigateToCreateArticleCommand = new NavigateCommand(createArticleViewModelNavigationService);
            LoadArticlesCommand = new LoadArticlesCommand(managerStore, this);
            _managerStore = managerStore;
            _navigationStore = navigationStore;
            _articleListingViewModelNavigationService = articleListingViewModelNavigationService;
            managerStore.ArticleCreated += OnArticleCreated;
            managerStore.ArticleDeleted += OnArticleDeleted;
        }
        public static ArticleListingViewModel LoadViewModel(ManagerStore managerStore, NavigationStore navigationStore, NavigationService articleListingViewModelNavigationService,  NavigationService createArticleViewModelNavigationService)
        {
            ArticleListingViewModel viewModel = new ArticleListingViewModel(managerStore,navigationStore, articleListingViewModelNavigationService, createArticleViewModelNavigationService);
            viewModel.LoadArticlesCommand?.Execute(null);

            return viewModel;
        }

        public IEnumerable<ArticleViewModel> Articles => _articles;
        public ICommand NavigateToCreateArticleCommand { get; }
        public ICommand LoadArticlesCommand { get; }
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
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
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);


        public void UpdateArticles(IEnumerable<Article> articles)
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

        private NavigationService CreateEditArticleNavigationService(Article article)
        {
            return new NavigationService(_navigationStore, () => new EditArticleViewModel(_managerStore, article, _articleListingViewModelNavigationService));
        }

        private void OnArticleDeleted(Article article)
        {
            foreach (ArticleViewModel a in _articles)
            {
                if (a.RepresentsArticle(article))
                {
                    _articles.Remove(a);
                    return;
                }
            }
        }
        private void OnArticleCreated(Article article)
        {
            ArticleViewModel articleViewModel = new ArticleViewModel(_managerStore, article, CreateEditArticleNavigationService(article));
            _articles.Add(articleViewModel);
        }

        private ObservableCollection<ArticleViewModel> _articles = new ObservableCollection<ArticleViewModel>();
        private bool _isLoading = false;
        private string _errorMessage = string.Empty;
        private readonly ManagerStore _managerStore;
        private readonly NavigationStore _navigationStore;
        private readonly NavigationService _articleListingViewModelNavigationService;
    }
}
