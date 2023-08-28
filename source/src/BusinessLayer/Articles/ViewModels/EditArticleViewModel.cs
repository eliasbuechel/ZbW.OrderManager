using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Articles.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.Models;
using DataLayer.Articles.Models;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.Articles.ViewModels
{
    public class EditArticleViewModel : BaseViewModelWithErrorHandling, IArticleGroupUpdatable
    {
        public EditArticleViewModel(ManagerStore managerStore, Article article, NavigationService articleListingViewModelNavigationService)
        {
            _article = article;

            SaveChangesToArticleCommand = new SaveChangesToArticleCommand(managerStore, article, this, articleListingViewModelNavigationService);
            CancelEditArticleCommand = new NavigateCommand(articleListingViewModelNavigationService);
            LoadArticleGroupsCommand = new LoadArticleGroupsCommand(managerStore, this);

            Name = _article.Name;
            ArticleGroup = _article.ArticleGroup;

            LoadArticleGroupsCommand.Execute(null);
        }

        public ICommand SaveChangesToArticleCommand { get; }
        public ICommand CancelEditArticleCommand { get; }
        public ICommand LoadArticleGroupsCommand { get; }
        public int Id => _article.Id;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();

                const int maxCharacterSize = 100;

                ClearErrors();
                if (string.IsNullOrEmpty(Name))
                    AddError(EMPTY_MESSAGE);
                if (Name.Length > maxCharacterSize)
                    AddError(ToLongErrorMessage(maxCharacterSize));
            }
        }
        public ArticleGroup? ArticleGroup
        {
            get => _articleGroup;
            set
            {
                _articleGroup = value;
                OnPropertyChanged();

                ClearErrors();
                if (ArticleGroup == null)
                    AddError(EMPTY_MESSAGE);
            }
        }
        public IEnumerable<ArticleGroup> ArticleGroups => _articleGroups;
        public bool HasErrorMessage => !ErrorMessage.IsNullOrEmpty();
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
        public bool IsLoading { get; set; }

        public void UpdateArticleGroups(IEnumerable<ArticleGroup> articleGroups)
        {
            _articleGroups.Clear();
            foreach (ArticleGroup articleGroup in articleGroups)
            {
                _articleGroups.Add(articleGroup);
            }
        }

        private string _name = string.Empty;
        private string _errorMessage = string.Empty;
        private readonly Article _article;
        private ArticleGroup? _articleGroup;
        private readonly ObservableCollection<ArticleGroup> _articleGroups = new ObservableCollection<ArticleGroup>();
    }
}
