using DataLayer.Articles.Models;

namespace DataLayer.Articles.Services.ArticleEditors
{
    public interface IArticleEditor
    {
        Task SaveChangesToArticleAsync(Article initialArticle, Article editedArticle);
    }
}
