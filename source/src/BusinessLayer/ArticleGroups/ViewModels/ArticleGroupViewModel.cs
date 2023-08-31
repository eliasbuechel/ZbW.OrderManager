using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.DTOs;
using System.Windows.Input;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public class ArticleGroupViewModel : BaseViewModel
    {
        public ArticleGroupViewModel(ManagerStore managerStore, ArticleGroupDTO articleGroup, NavigationStore navigationStore, NavigationService articleGroupListingViewModelNavigationService)
        {
            _managerStore = managerStore;
            _managerStore.SubordinateArticleGroupCreated += OnSubordinateArticleGroupCreated;
            _managerStore.SubordinateArticleGroupDeleted += OnSubordinateArticleGroupDeleted;


            _articleGroup = articleGroup;
            _navigationStore = navigationStore;
            _articleGroupListingViewModelNavigationService = articleGroupListingViewModelNavigationService;

            EditArticleGroupCommand = new NavigateCommand(CreateEditArticleGroupNavigationService());
            CreateArticleGroupCommand = new NavigateCommand(CreateCreateArticleGroupNavigationService());
            DeleteArticleGroupCommand = new DeleteArticleGroupCommand(_managerStore, _articleGroup, this);
        }
        public override void Dispose()
        {
            _managerStore.SubordinateArticleGroupCreated -= OnSubordinateArticleGroupCreated;
            _managerStore.SubordinateArticleGroupDeleted -= OnSubordinateArticleGroupDeleted;

            base.Dispose();
        }

        public string Name => _articleGroup.Name;
        public IEnumerable<ArticleGroupViewModel> SubordinateArticleGroups => _articleGroup.SubordinateArticleGroups.Select(a =>  new ArticleGroupViewModel(_managerStore, a, _navigationStore, _articleGroupListingViewModelNavigationService)).ToList();
        public ICommand EditArticleGroupCommand { get; }
        public ICommand CreateArticleGroupCommand { get; }
        public ICommand DeleteArticleGroupCommand { get; }

        public ArticleGroupDTO GetArticleGroup()
        {
            return _articleGroup;
        }

        private void OnSubordinateArticleGroupCreated(ArticleGroupDTO createdArticleGroup, ArticleGroupDTO superiorArticleGroup)
        {
            if (superiorArticleGroup.Id == _articleGroup.Id)
                OnPropertyChanged(nameof(SubordinateArticleGroups));
        }
        private void OnSubordinateArticleGroupDeleted(ArticleGroupDTO articleGroup, ArticleGroupDTO superiorArticleGroup)
        {
            OnPropertyChanged(nameof(SubordinateArticleGroups));
        }
        private NavigationService CreateCreateArticleGroupNavigationService()
        {
            return new NavigationService(_navigationStore, () => CreateArticleGroupViewModel.LoadViewModel(_managerStore, _articleGroupListingViewModelNavigationService, _articleGroup));
        }
        private NavigationService CreateEditArticleGroupNavigationService()
        {
            return new NavigationService(_navigationStore, () => EditArticleGroupViewModel.LoadViewModel(_managerStore, _articleGroup, _articleGroupListingViewModelNavigationService));
        }

        private readonly ManagerStore _managerStore;
        private readonly ArticleGroupDTO _articleGroup;
        private readonly NavigationStore _navigationStore;
        private readonly NavigationService _articleGroupListingViewModelNavigationService;
    }
}
