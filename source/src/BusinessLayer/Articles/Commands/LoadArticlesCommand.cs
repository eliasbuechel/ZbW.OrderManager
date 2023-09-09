using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;

namespace BusinessLayer.Articles.Commands
{
    public class LoadArticlesCommand : BaseAsyncCommand
    {
        public LoadArticlesCommand(ManagerStore managerStore, IArticleUpdatable articleUpdatable)
        {
            _managerStore = managerStore;
            _articleUpdatable = articleUpdatable;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _articleUpdatable.ErrorMessage = string.Empty;
            _articleUpdatable.IsLoading = true;

            try
            {
                await _managerStore.Load();

                _articleUpdatable.UpdateArticles(_managerStore.Articles);
            }
            catch (Exception)
            {
                _articleUpdatable.ErrorMessage = "Failed to load articles.";
            }
            finally
            {
                _articleUpdatable.IsLoading = false;
            }
        }

        private readonly ManagerStore _managerStore;
        private readonly IArticleUpdatable _articleUpdatable;
    }
}
