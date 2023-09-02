using BusinessLayer.Articles.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.Articles.Validation;
using System.Windows.Input;

namespace BusinessLayer.Articles.ViewModels
{
    public class CreateArticleViewModel : BaseCreateEditArticleViewModel
    {
        public static CreateArticleViewModel LoadViewModel(ManagerStore managerStore, NavigationService createArticleViewModelNavigationService, IArticleValidator articleValidator)
		{
			CreateArticleViewModel viewModel = new CreateArticleViewModel(managerStore, createArticleViewModelNavigationService, articleValidator);
			viewModel.LoadArticleGroupsCommand?.Execute(null);
			return viewModel;
		}

        private CreateArticleViewModel(ManagerStore managerStore, NavigationService articleListingViewMoelNavigationService, IArticleValidator articleValidator)
			: base(managerStore, articleValidator, string.Empty)
        {
            CreateArticleCommand = new CreateArticleCommand(managerStore, this, articleListingViewMoelNavigationService);
            CancelCreateArticleCommand = new NavigateCommand(articleListingViewMoelNavigationService);
        }

        public ICommand CreateArticleCommand { get; }
        public ICommand CancelCreateArticleCommand { get; }
    }
}
