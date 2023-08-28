using DataLayer.Articles.Models;

namespace DataLayer.Articles.Services.ArticleDeletors
{
    public interface IArticleDeletor
    {
        Task DeleteArticleAsync(Article article);
    }
}
