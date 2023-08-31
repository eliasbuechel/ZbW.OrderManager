using BusinessLayer.Articles.Commands;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using System.Windows.Input;

namespace BusinessLayer.Articles.ViewModels
{
    public class CreateArticleViewModel : BaseCreateEditArticleViewModel
    {
        public static CreateArticleViewModel LoadViewModel(ManagerStore managerStore, NavigationService createArticleViewModelNavigationService)
		{
			CreateArticleViewModel viewModel = new CreateArticleViewModel(managerStore, createArticleViewModelNavigationService);
			viewModel.LoadArticleGroupsCommand?.Execute(null);
			return viewModel;
		}

        private CreateArticleViewModel(ManagerStore managerStore, NavigationService articleListingViewMoelNavigationService)
			: base(managerStore, string.Empty)
        {
            CreateArticleCommand = new CreateArticleCommand(managerStore, this, articleListingViewMoelNavigationService);
            CancelCreateArticleCommand = new NavigateCommand(articleListingViewMoelNavigationService);
        }

        public ICommand CreateArticleCommand { get; }
        public ICommand CancelCreateArticleCommand { get; }
    }
}
