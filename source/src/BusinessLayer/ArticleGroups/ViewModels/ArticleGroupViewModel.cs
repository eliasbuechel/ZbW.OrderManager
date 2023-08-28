using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.Models;
using System.Windows.Input;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public class ArticleGroupViewModel : BaseViewModel
    {
        public ArticleGroupViewModel(ManagerStore managerStore, ArticleGroupListingViewModel articleGroupListingViewModel, ArticleGroup articleGroup)
        {
            _managerStore = managerStore;
            _managerStore.SubordinateArticleGroupCreated += OnSubordinateArticleGroupCreated;
            _managerStore.SubordinateArticleGroupDeleted += OnSubordinateArticleGroupDeleted;

            _articleGroupListingViewModel = articleGroupListingViewModel;
            _articleGroupListingViewModel.IsEditingEnabledChanged += OnIsEditingEnabledChanged;
            
            _articleGroup = articleGroup;

            Name = _articleGroup.Name;

            CreateSubordinateArticleGroupCommand = new CreateSubordinateArticleGroupCommand(_managerStore, _articleGroupListingViewModel, this);
            DeleteArticleGroupCommand = new DeleteArticleGroupCommand(_managerStore, _articleGroup, this);
        }
        public override void Dispose()
        {
            _managerStore.SubordinateArticleGroupCreated -= OnSubordinateArticleGroupCreated;
            _managerStore.SubordinateArticleGroupDeleted -= OnSubordinateArticleGroupDeleted;

            _articleGroupListingViewModel.IsEditingEnabledChanged -= OnIsEditingEnabledChanged;
            base.Dispose();
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<ArticleGroupViewModel> SubordinateArticleGroups => _articleGroup.SubordinateArticleGroups.Select(a =>  new ArticleGroupViewModel(_managerStore, _articleGroupListingViewModel, a)).ToList();
        public bool IsEditingEnabled => _articleGroupListingViewModel.IsEditingEnabled;
        public ICommand CreateSubordinateArticleGroupCommand { get; }
        public ICommand DeleteArticleGroupCommand { get; }
        public ICommand RenameArticleGroupCommand { get; }

        public void OnIsEditingEnabledChanged()
        {
            OnPropertyChanged(nameof(IsEditingEnabled));

            if (!IsEditingEnabled)
                Name = _articleGroup.Name;
        }
        public ArticleGroup GetArticleGroup()
        {
            return _articleGroup;
        }

        private void OnSubordinateArticleGroupCreated(ArticleGroup createdArticleGroup, ArticleGroup superiorArticleGroup)
        {
            if (superiorArticleGroup.Id == _articleGroup.Id)
                OnPropertyChanged(nameof(SubordinateArticleGroups));
        }
        private void OnSubordinateArticleGroupDeleted(ArticleGroup articleGroup, ArticleGroup superiorArticleGroup)
        {
            OnPropertyChanged(nameof(SubordinateArticleGroups));
        }

        private readonly ManagerStore _managerStore;
        private readonly ArticleGroupListingViewModel _articleGroupListingViewModel;
        private readonly ArticleGroup _articleGroup;
        private string _name;
    }
}
