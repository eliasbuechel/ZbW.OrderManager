using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;
using DataLayer.Articles.DTOs;

namespace BusinessLayer.Articles.Commands
{
    public class DeleteArticleCommand : BaseAsyncCommand
    {
        private readonly ManagerStore _managerStore;
        private readonly ArticleDTO _article;

        public DeleteArticleCommand(ManagerStore managerStore, ArticleDTO article)
        {
            _managerStore = managerStore;
            _article = article;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            await _managerStore.DeleteArticleAsync(_article);
        }
    }
}
