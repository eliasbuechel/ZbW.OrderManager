using DataLayer.Articles.DTOs;

namespace DataLayer.Articles.Services.ArticleDeletors
{
    public interface IArticleDeletor
    {
        Task DeleteArticleAsync(ArticleDTO articleDTO);
    }
}
