using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Validation;
using System.Windows.Input;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public class CreateArticleGroupViewModel : BaseCreateAndDeleteArticleGroupViewModel
    {
        public static CreateArticleGroupViewModel LoadViewModel(ManagerStore managerStore, NavigationService<ArticleGroupListingViewModel> articleGroupListingViewModelNavigationService, IArticleGroupValidator articleGroupValidator, ArticleGroupDTO? suggestedSuperiorArticleGroup = null)
        {
            CreateArticleGroupViewModel viewModel = new CreateArticleGroupViewModel(managerStore, articleGroupListingViewModelNavigationService, articleGroupValidator, suggestedSuperiorArticleGroup);
            viewModel.LoadArticleGroupsCommand?.Execute(null);
            return viewModel;
        }

        private CreateArticleGroupViewModel(ManagerStore managerStore, NavigationService<ArticleGroupListingViewModel> articleGroupListingViewModelNavigationService, IArticleGroupValidator articleGroupValidator, ArticleGroupDTO? suggestedSuperiorArticleGroup = null)
			: base(managerStore, articleGroupValidator)
        {
			CreateArticleGroupCommand = new CreateArticleGroupCommand(managerStore, this, articleGroupListingViewModelNavigationService);
			CancelCreateArticleGroupCommand = new NavigateCommand(articleGroupListingViewModelNavigationService);

            Name = string.Empty;
            SuperiorArticleGroup = suggestedSuperiorArticleGroup;
            IsRootElement = SuperiorArticleGroup == null;

        }

        public ICommand CreateArticleGroupCommand { get; }
        public ICommand CancelCreateArticleGroupCommand { get; }
    }
}