using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;

namespace BusinessLayer.ArticleGroups.Commands
{
    public class LoadArticleGroupsCommand : BaseAsyncCommand
    {
        public LoadArticleGroupsCommand(ManagerStore managerStore, IArticleGroupUpdatable articleGroupUpdatableViewModel)
        {
            _managerStore = managerStore;
            _articleGroupListingViewModel = articleGroupUpdatableViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _articleGroupListingViewModel.ErrorMessage = string.Empty;
            _articleGroupListingViewModel.IsLoading = true;
            try
            {
                await _managerStore.Load();

                _articleGroupListingViewModel.UpdateArticleGroups(_managerStore.ArticleGroups);
            }
            catch (Exception)
            {
                _articleGroupListingViewModel.ErrorMessage = "Failed to load article groups.";
            }
            finally
            {
                _articleGroupListingViewModel.IsLoading = false;
            }
        }

        private readonly ManagerStore _managerStore;
        private readonly IArticleGroupUpdatable _articleGroupListingViewModel;
    }
}
