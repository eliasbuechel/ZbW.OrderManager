using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public class ArticleGroupListingViewModel : BaseViewModel, IArticleGroupUpdatable
    {

        public ArticleGroupListingViewModel(ManagerStore managerStore)
        {
            _articleGroups = new ObservableCollection<ArticleGroupViewModel>();

            _managerStore = managerStore;
            _managerStore.RootArticleGroupCreated += OnRootArticleGroupCreated;
            _managerStore.RootArticleGroupDeleted += OnRootArticleGroupDeleted;

            LoadArticleGroupsCommand = new LoadArticleGroupsCommand(managerStore, this);
            CreateRootArticleGroupCommand = new CreateRootArticleGroupCommand(managerStore, this);
        }
        public static ArticleGroupListingViewModel LoadViewModel(ManagerStore managerStore)
        {
            ArticleGroupListingViewModel viewModel = new ArticleGroupListingViewModel(managerStore);
            viewModel.LoadArticleGroupsCommand.Execute(null);
            return viewModel;
        }
        public override void Dispose()
        {
            _managerStore.RootArticleGroupCreated -= OnRootArticleGroupCreated;
            _managerStore.RootArticleGroupDeleted -= OnRootArticleGroupDeleted;

            base.Dispose();
        }


        public event Action? IsEditingEnabledChanged;

        public IEnumerable<ArticleGroupViewModel> ArticleGroups => _articleGroups;
        public ICommand LoadArticleGroupsCommand { get; }
        public ICommand CreateRootArticleGroupCommand { get; }
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        public bool IsEditingEnabled
        {
            get => _isEditingEnabled;
            set
            {
                _isEditingEnabled = value;
                OnPropertyChanged();
                OnIsEditingEnabledChanged();
            }
        }
        public string NameOfToAddingArticleGroup
        {
            get => _nameOfToAddingArticleGroup;
            set
            {
                _nameOfToAddingArticleGroup = value;
                OnPropertyChanged();
            }
        }

        public void UpdateArticleGroups(IEnumerable<ArticleGroup> articleGroups)
        {
            _articleGroups.Clear();

            foreach (ArticleGroup articleGroup in articleGroups)
            {
                ArticleGroupViewModel articleGropViewModel = new ArticleGroupViewModel(_managerStore, this, articleGroup);
                _articleGroups.Add(articleGropViewModel);
            }
        }

        private void OnIsEditingEnabledChanged()
        {
            IsEditingEnabledChanged?.Invoke();
        }
        private void OnRootArticleGroupCreated(ArticleGroup createdArticleGroup)
        {
            NameOfToAddingArticleGroup = string.Empty;
            ArticleGroupViewModel viewModel = CreateArticleGroupViewModel(createdArticleGroup);
            _articleGroups.Add(viewModel);

            OnPropertyChanged(nameof(ArticleGroups));
        }

        private ArticleGroupViewModel CreateArticleGroupViewModel(ArticleGroup articleGroup)
        {
            return new ArticleGroupViewModel(_managerStore, this, new ArticleGroup(articleGroup.Id, articleGroup.Name));
        }

        private void OnRootArticleGroupDeleted(ArticleGroup articleGroup)
        {
            ArticleGroupViewModel? articleGroupViewModel = _articleGroups.FirstOrDefault(a => a.GetArticleGroup().Id == articleGroup.Id);

            if (articleGroupViewModel != null)
            {
                _articleGroups.Remove(articleGroupViewModel);
                OnPropertyChanged(nameof(ArticleGroups));
            }
        }
        private ArticleGroupViewModel? GetMatchingArticleGroupViewModel(ArticleGroupViewModel viewModel, ArticleGroup articleGroup)
        {
            if (viewModel.GetArticleGroup().Id == articleGroup.Id)
                return viewModel;

            foreach (ArticleGroupViewModel subordinateArticleGroup in viewModel.SubordinateArticleGroups)
            {
                ArticleGroupViewModel? foundArticleGroupViewModel = GetMatchingArticleGroupViewModel(subordinateArticleGroup, articleGroup);
                
                if (foundArticleGroupViewModel != null)
                    return foundArticleGroupViewModel;
            }

            return null;
        }
        private bool ContainsArticleGroup(ArticleGroupViewModel articleGroupViewModel, ArticleGroup articleGroup)
        {
            if (articleGroupViewModel.GetArticleGroup().Id == articleGroup.Id)
                return true;

            foreach (ArticleGroupViewModel viewModel in articleGroupViewModel.SubordinateArticleGroups)
            {
                if (ContainsArticleGroup(viewModel, articleGroup))
                    return true;
            }

            return false;
        }

        private bool _isEditingEnabled;
        private bool _isLoading;
        private string _errorMessage = string.Empty;
        private string _nameOfToAddingArticleGroup = string.Empty;

        private readonly ObservableCollection<ArticleGroupViewModel> _articleGroups;
        private readonly ManagerStore _managerStore;
    }
}
