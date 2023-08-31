using BusinessLayer.ArticleGroups.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.ArticleGroups.DTOs;
using System.Windows.Input;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public class CreateArticleGroupViewModel : BaseCreateAndDeleteArticleGroupViewModel
    {
        public static CreateArticleGroupViewModel LoadViewModel(ManagerStore managerStore, NavigationService articleGroupListingViewModelNavigationService, ArticleGroupDTO? suggestedSuperiorArticleGroup = null)
        {
            CreateArticleGroupViewModel viewModel = new CreateArticleGroupViewModel(managerStore, articleGroupListingViewModelNavigationService, suggestedSuperiorArticleGroup);
            viewModel.LoadArticleGroupsCommand?.Execute(null);
            return viewModel;
        }

        private CreateArticleGroupViewModel(ManagerStore managerStore, NavigationService articleGroupListingViewModelNavigationService, ArticleGroupDTO? suggestedSuperiorArticleGroup)
			: base(managerStore, string.Empty, suggestedSuperiorArticleGroup)
        {
			CreateArticleGroupCommand = new CreateArticleGroupCommand(managerStore, this, articleGroupListingViewModelNavigationService);
			CancelCreateArticleGroupCommand = new NavigateCommand(articleGroupListingViewModelNavigationService);
        }

        public ICommand CreateArticleGroupCommand { get; }
        public ICommand CancelCreateArticleGroupCommand { get; }
    }
}
