using DataLayer.Articles.Models;

namespace DataLayer.Articles.Services.ArticleCreator
{
    public interface IArticleCreator
    {
        Task CreateArticleAsync(Article article);
        Task<int> GetNextFreeArticleIdAsync();
    }
}
