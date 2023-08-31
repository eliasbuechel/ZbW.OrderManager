using DataLayer.Articles.DTOs;

namespace DataLayer.Articles.Services.ArticleProviders
{
    public interface IArticleProvider
    {
        public Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync();
    }
}
