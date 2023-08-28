using DataLayer.Articles.Models;
using DataLayer.Articles.Services.ArticleCreator;
using DataLayer.Articles.Services.ArticleDeletors;
using DataLayer.Articles.Services.ArticleEditors;
using DataLayer.Articles.Services.ArticleProviders;

namespace BusinessLayer.Articles.Models
{
    public class ArticleList
    {
        public ArticleList(IArticleProvider articleProvider, IArticleCreator articleCreator, IArticleDeletor articleDeletor, IArticleEditor articleEditor)
        {
            _articleProvider = articleProvider;
            _articleCreator = articleCreator;
            _articleDeletor = articleDeletor;
            _articleEditor = articleEditor;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _articleProvider.GetAllArticlesAsync();
        }
        public async Task CreateArticleAsync(Article article)
        {
            await _articleCreator.CreateArticleAsync(article);
        }
        public async Task<int> GetNextFreeArticleIdAsync()
        {
            return await _articleCreator.GetNextFreeArticleIdAsync();
        }
        public async Task DeleteArticleAsync(Article article)
        {
            await _articleDeletor.DeleteArticleAsync(article);
        }
        public async Task SaveChangesToArticleAsync(Article initialArticle, Article editedArticle)
        {
            await _articleEditor.SaveChangesToArticleAsync(initialArticle, editedArticle);
        }

        private readonly IArticleProvider _articleProvider;
        private readonly IArticleCreator _articleCreator;
        private readonly IArticleDeletor _articleDeletor;
        private readonly IArticleEditor _articleEditor;
    }
}
