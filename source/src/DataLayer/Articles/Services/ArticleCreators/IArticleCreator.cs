using DataLayer.Articles.DTOs;

namespace DataLayer.Articles.Services.ArticleCreators
{
    public interface IArticleCreator
    {
        Task CreateArticleAsync(ArticleDTO articleDTO);
        Task<int> GetNextFreeArticleIdAsync();
    }
}
