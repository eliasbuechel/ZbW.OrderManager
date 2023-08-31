using DataLayer.Articles.DTOs;

namespace DataLayer.Articles.Services.ArticleCreator
{
    public interface IArticleCreator
    {
        Task CreateArticleAsync(ArticleDTO article);
        Task<int> GetNextFreeArticleIdAsync();
    }
}
