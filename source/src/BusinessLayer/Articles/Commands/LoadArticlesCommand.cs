using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;

namespace BusinessLayer.Articles.Commands
{
    public class LoadArticlesCommand : BaseAsyncCommand
    {
        private readonly ManagerStore _managerStore;
        private ArticleListingViewModel _articleListingViewModel;

        public LoadArticlesCommand(ManagerStore managerStore, ArticleListingViewModel articleListingViewModel)
        {
            _managerStore = managerStore;
            _articleListingViewModel = articleListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _articleListingViewModel.ErrorMessage = string.Empty;
            _articleListingViewModel.IsLoading = true;

            try
            {
                await _managerStore.Load();

                _articleListingViewModel.UpdateArticles(_managerStore.Articles);
            }
            catch (Exception)
            {
                _articleListingViewModel.ErrorMessage = "Failed to load articles.";
            }
            finally
            {
                _articleListingViewModel.IsLoading = false;
            }
        }
    }
}
