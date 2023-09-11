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
    public class ArticleListingViewModel : BaseLoadableViewModel, IArticleUpdatable, IMainNavigationViewModel
    {
        public static ArticleListingViewModel LoadViewModel(ManagerStore managerStore, NavigationStore navigationStore, NavigationService<ArticleListingViewModel> articleListingViewModelNavigationService,  NavigationService<CreateArticleViewModel> createArticleViewModelNavigationService, IArticleValidator articleValidator)
        {
            ArticleListingViewModel viewModel = new ArticleListingViewModel(managerStore,navigationStore, articleListingViewModelNavigationService, createArticleViewModelNavigationService, articleValidator);
            viewModel.LoadArticlesCommand?.Execute(null);

            return viewModel;
        }

        private ArticleListingViewModel(ManagerStore managerStore, NavigationStore navigationStore, NavigationService<ArticleListingViewModel> articleListingViewModelNavigationService, NavigationService<CreateArticleViewModel> createArticleViewModelNavigationService, IArticleValidator articleValidator)
        {
            _managerStore = managerStore;
            _navigationStore = navigationStore;
            _articleListingViewModelNavigationService = articleListingViewModelNavigationService;
            _articleValidator = articleValidator;

            Articles = new CollectionView<ArticleViewModel>(_articles);
            Articles.Filter = o => o.Name.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            Articles.OrderKeySelector = o => Convert.ToInt32(o.Id);

            NavigateToCreateArticleCommand = new NavigateCommand(createArticleViewModelNavigationService);
            LoadArticlesCommand = new LoadArticlesCommand(managerStore, this);

            managerStore.ArticleCreated += OnArticleCreated;
            managerStore.ArticleDeleted += OnArticleDeleted;
        }

        public CollectionView<ArticleViewModel> Articles { get; }
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
            Articles.Update();
        }
        public override void Dispose(bool disposing)
        {
            _managerStore.ArticleCreated -= OnArticleCreated;
            _managerStore.ArticleDeleted -= OnArticleDeleted;
        }
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                OnPropertyChanged();
                Articles.Update();
            }
        }

        private NavigationService<EditArticleViewModel> CreateEditArticleNavigationService(ArticleDTO article)
        {
            return new NavigationService<EditArticleViewModel>(_navigationStore, () => EditArticleViewModel.LoadViewModel(_managerStore, article, _articleListingViewModelNavigationService, _articleValidator));
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
            Articles.Update();
        }
        private void OnArticleCreated(ArticleDTO article)
        {
            ArticleViewModel articleViewModel = new ArticleViewModel(_managerStore, article, CreateEditArticleNavigationService(article));
            _articles.Add(articleViewModel);
            Articles.Update();
        }

        private readonly ManagerStore _managerStore;
        private readonly NavigationStore _navigationStore;
        private readonly NavigationService<ArticleListingViewModel> _articleListingViewModelNavigationService;
        private readonly ObservableCollection<ArticleViewModel> _articles = new ObservableCollection<ArticleViewModel>();
        private readonly IArticleValidator _articleValidator;
        private string _filter = string.Empty;
    }
}
