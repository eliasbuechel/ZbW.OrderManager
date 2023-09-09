using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Validation;
using System.Windows.Input;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public class ArticleGroupViewModel : BaseViewModel
    {
        public ArticleGroupViewModel(ManagerStore managerStore, ArticleGroupDTO articleGroup, NavigationStore navigationStore, NavigationService<ArticleGroupListingViewModel> articleGroupListingViewModelNavigationService, IArticleGroupValidator articleGroupValidator)
        {
            _managerStore = managerStore;
            _managerStore.SubordinateArticleGroupCreated += OnSubordinateArticleGroupCreated;
            _managerStore.SubordinateArticleGroupDeleted += OnSubordinateArticleGroupDeleted;


            _articleGroup = articleGroup;
            _navigationStore = navigationStore;
            _articleGroupListingViewModelNavigationService = articleGroupListingViewModelNavigationService;
            _articleGroupValidator = articleGroupValidator;
            EditArticleGroupCommand = new NavigateCommand(CreateEditArticleGroupNavigationService());
            CreateArticleGroupCommand = new NavigateCommand(CreateCreateArticleGroupNavigationService());
            DeleteArticleGroupCommand = new DeleteArticleGroupCommand(_managerStore, _articleGroup, this);
        }

        public ICommand EditArticleGroupCommand { get; }
        public ICommand CreateArticleGroupCommand { get; }
        public ICommand DeleteArticleGroupCommand { get; }
        public string Name => _articleGroup.Name;
        public IEnumerable<ArticleGroupViewModel> SubordinateArticleGroups => GetSubordinateArticleGroups(_articleGroup);

        public ArticleGroupDTO GetArticleGroup()
        {
            return _articleGroup;
        }
        public override void Dispose()
        {
            _managerStore.SubordinateArticleGroupCreated -= OnSubordinateArticleGroupCreated;
            _managerStore.SubordinateArticleGroupDeleted -= OnSubordinateArticleGroupDeleted;

            base.Dispose();
        }

        private void OnSubordinateArticleGroupCreated(ArticleGroupDTO createdArticleGroup, ArticleGroupDTO superiorArticleGroup)
        {
            if (superiorArticleGroup.Id == _articleGroup.Id)
                OnPropertyChanged(nameof(SubordinateArticleGroups));
        }
        private void OnSubordinateArticleGroupDeleted(ArticleGroupDTO articleGroup, ArticleGroupDTO superiorArticleGroup)
        {
            if (_articleGroup.Id == superiorArticleGroup.Id)
                OnPropertyChanged(nameof(SubordinateArticleGroups));
        }
        private NavigationService<CreateArticleGroupViewModel> CreateCreateArticleGroupNavigationService()
        {
            return new NavigationService<CreateArticleGroupViewModel>(_navigationStore, () => CreateArticleGroupViewModel.LoadViewModel(_managerStore, _articleGroupListingViewModelNavigationService, _articleGroupValidator, _articleGroup));
        }
        private NavigationService<EditArticleGroupViewModel> CreateEditArticleGroupNavigationService()
        {
            return new NavigationService<EditArticleGroupViewModel>(_navigationStore, () => EditArticleGroupViewModel.LoadViewModel(_managerStore, _articleGroup, _articleGroupListingViewModelNavigationService, _articleGroupValidator));
        }

        private static IEnumerable<ArticleGroupViewModel> GetSubordinateArticleGroups(ArticleGroupDTO articlGroup)
        {
            ICollection<ArticleGroupDTO> subordinateArticleGroups = articlGroup.SubordinateArticleGroups;

            

            ICollection<ArticleGroupViewModel> articleGroupViewModels = subordinateArticleGroups
                .Select(articleGroupViewModel => new ArticleGroupViewModel(
                    _managerStore,
                    articleGroupViewModel,
                    _navigationStore,
                    _articleGroupListingViewModelNavigationService,
                    _articleGroupValidator)
                )
                .ToList();

            return articleGroupViewModels;
        }

        private readonly ManagerStore _managerStore;
        private readonly ArticleGroupDTO _articleGroup;
        private readonly NavigationStore _navigationStore;
        private readonly IArticleGroupValidator _articleGroupValidator;
        private readonly NavigationService<ArticleGroupListingViewModel> _articleGroupListingViewModelNavigationService;
    }
}
