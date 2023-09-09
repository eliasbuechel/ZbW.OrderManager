using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Validation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public class ArticleGroupListingViewModel : BaseLoadableViewModel, IArticleGroupUpdatable
    {
        public static ArticleGroupListingViewModel LoadViewModel(ManagerStore managerStore, NavigationStore navigationStore, NavigationService<ArticleGroupListingViewModel> articleGroupListingViewModelNavigationService, NavigationService<CreateArticleGroupViewModel> createArticleGorupViewModelNavigationService, IArticleGroupValidator articleGroupValidator)
        {
            ArticleGroupListingViewModel viewModel = new ArticleGroupListingViewModel(managerStore, navigationStore, articleGroupListingViewModelNavigationService, createArticleGorupViewModelNavigationService, articleGroupValidator);
            viewModel.LoadArticleGroupsCommand.Execute(null);
            return viewModel;
        }

        private ArticleGroupListingViewModel(ManagerStore managerStore, NavigationStore navigationStore, NavigationService<ArticleGroupListingViewModel> articleGroupListingViewModelNavigationService, NavigationService<CreateArticleGroupViewModel> createArticleGorupViewModelNavigationService, IArticleGroupValidator articleGroupValidator)
        {
            _managerStore = managerStore;
            _navigationStore = navigationStore;
            _articleGroupListingViewModelNavigationService = articleGroupListingViewModelNavigationService;
            _managerStore.RootArticleGroupCreated += OnRootArticleGroupCreated;
            _managerStore.RootArticleGroupDeleted += OnRootArticleGroupDeleted;

            NavigateToCreateArticleGroup = new NavigateCommand(createArticleGorupViewModelNavigationService);
            LoadArticleGroupsCommand = new LoadArticleGroupsCommand(managerStore, this);
            _articleGroupValidator = articleGroupValidator;
        }

        public ICommand NavigateToCreateArticleGroup { get; }
        public ICommand LoadArticleGroupsCommand { get; }
        public IEnumerable<ArticleGroupViewModel> ArticleGroups => _articleGroups;

        public void UpdateArticleGroups(IEnumerable<ArticleGroupDTO> articleGroups)
        {
            _articleGroups.Clear();

            foreach (ArticleGroupDTO articleGroup in articleGroups)
            {
                ArticleGroupViewModel articleGropViewModel = new ArticleGroupViewModel(_managerStore,
                                                                                       articleGroup,
                                                                                       _navigationStore,
                                                                                       _articleGroupListingViewModelNavigationService,
                                                                                       _articleGroupValidator);

                _articleGroups.Add(articleGropViewModel);
            }
        }
        public override void Dispose(bool disposing)
        {
            _managerStore.RootArticleGroupCreated -= OnRootArticleGroupCreated;
            _managerStore.RootArticleGroupDeleted -= OnRootArticleGroupDeleted;
        }

        private void OnRootArticleGroupCreated(ArticleGroupDTO createdArticleGroup)
        {
            ArticleGroupViewModel viewModel = new ArticleGroupViewModel(_managerStore,
                                                                        createdArticleGroup,
                                                                        _navigationStore,
                                                                        _articleGroupListingViewModelNavigationService,
                                                                        _articleGroupValidator);
            _articleGroups.Add(viewModel);

            OnPropertyChanged(nameof(ArticleGroups));
        }
        private void OnRootArticleGroupDeleted(ArticleGroupDTO articleGroup)
        {
            foreach (ArticleGroupViewModel a in _articleGroups)
            {
                if (a.GetArticleGroup().Id == articleGroup.Id)
                {
                    _articleGroups.Remove(a);
                    OnPropertyChanged(nameof(_articleGroups));
                    return;
                }
            }
        }

        private readonly ManagerStore _managerStore;
        private readonly NavigationStore _navigationStore;
        private readonly NavigationService<ArticleGroupListingViewModel> _articleGroupListingViewModelNavigationService;
        private readonly ObservableCollection<ArticleGroupViewModel> _articleGroups = new ObservableCollection<ArticleGroupViewModel>();
        private readonly IArticleGroupValidator _articleGroupValidator;
    }
}
