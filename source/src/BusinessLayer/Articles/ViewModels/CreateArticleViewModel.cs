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
        public static CreateArticleViewModel LoadViewModel(ManagerStore managerStore, NavigationService<ArticleListingViewModel> articleListingViewModelNavigationService, IArticleValidator articleValidator)
		{
			CreateArticleViewModel viewModel = new CreateArticleViewModel(managerStore, articleListingViewModelNavigationService, articleValidator);
			viewModel.LoadArticleGroupsCommand?.Execute(null);
			return viewModel;
		}

        private CreateArticleViewModel(ManagerStore managerStore, NavigationService<ArticleListingViewModel> articleListingViewModelNavigationService, IArticleValidator articleValidator)
			: base(managerStore, articleValidator, string.Empty)
        {
            CreateArticleCommand = _createArticleCommand = new CreateArticleCommand(managerStore, this, articleListingViewModelNavigationService);
            CancelCreateArticleCommand = new NavigateCommand(articleListingViewModelNavigationService);
        }

        public override void Dispose(bool disposing)
        {
            _createArticleCommand.Dispose();
        }

        public ICommand CreateArticleCommand { get; }
        public ICommand CancelCreateArticleCommand { get; }

        private readonly CreateArticleCommand _createArticleCommand;
    }
}
