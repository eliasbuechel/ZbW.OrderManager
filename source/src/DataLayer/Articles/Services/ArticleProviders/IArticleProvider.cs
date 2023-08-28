using DataLayer.Articles.Models;

namespace DataLayer.Articles.Services.ArticleProviders
{
    public interface IArticleProvider
    {
        public Task<IEnumerable<Article>> GetAllArticlesAsync();
    }
}
