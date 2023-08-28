using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Articles.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.Models;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BusinessLayer.Articles.ViewModels
{
    public class CreateArticleViewModel : BaseViewModelWithErrorHandling, IArticleGroupUpdatable
    {
        public CreateArticleViewModel(ManagerStore managerStore, NavigationService articleListingViewMoelNavigationService)
        {
            CreateArticleCommand = new CreateArticleCommand(managerStore, this, articleListingViewMoelNavigationService);
            CancelCreateArticleCommand = new NavigateCommand(articleListingViewMoelNavigationService);
			LoadArticleGroupsCommand = new LoadArticleGroupsCommand(managerStore, this);

			Name = string.Empty;
			ArticleGroup = null;
        }
        public static CreateArticleViewModel LoadViewModel(ManagerStore managerStore, NavigationService createArticleViewModelNavigationService)
		{
			CreateArticleViewModel viewModel = new CreateArticleViewModel(managerStore, createArticleViewModelNavigationService);
			viewModel.LoadArticleGroupsCommand?.Execute(null);

			return viewModel;
		}


        public ICommand CreateArticleCommand { get; }
        public ICommand CancelCreateArticleCommand { get; }
		public ICommand LoadArticleGroupsCommand { get; }
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
			get
			{
				return _articleGroup;
			}
			set
			{
				_articleGroup = value;
				OnPropertyChanged();

                ClearErrors();
                if (_articleGroup == null)
                    AddError(EMPTY_MESSAGE);
            }
		}

		public IEnumerable<ArticleGroup> ArticleGroups => _articleGroups;

		public bool HasErrorMessage => ErrorMessage.IsNullOrEmpty();
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
        public bool IsLoading
		{
			get => _isLoading;
			set
			{
				_isLoading = value;
				OnPropertyChanged();
			}
		}

        public void UpdateArticleGroups(IEnumerable<ArticleGroup> articleGroups)
        {
            _articleGroups.Clear();

			foreach (var articleGroup in articleGroups)
			{
				_articleGroups.Add(articleGroup);
			}
        }

        private string _name = string.Empty;
		private bool _isLoading;
        private string _errorMessage = string.Empty;
		private ArticleGroup? _articleGroup;
        private ObservableCollection<ArticleGroup> _articleGroups = new ObservableCollection<ArticleGroup>();
    }
}
