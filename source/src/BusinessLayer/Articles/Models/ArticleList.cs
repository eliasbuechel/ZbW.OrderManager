using DataLayer.ArticleGroups.Models;
using DataLayer.Articles.DTOs;
using DataLayer.Articles.Services.ArticleCreators;
using DataLayer.Articles.Services.ArticleDeletors;
using DataLayer.Articles.Services.ArticleEditors;
using DataLayer.Articles.Services.ArticleProviders;
using DataLayer.Articles.Validation;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Articles.Models
{
    public class ArticleList
    {
        public ArticleList(IArticleProvider articleProvider, IArticleCreator articleCreator, IArticleDeletor articleDeletor, IArticleEditor articleEditor, IArticleValidator articleValidator)
        {
            _articleProvider = articleProvider;
            _articleCreator = articleCreator;
            _articleDeletor = articleDeletor;
            _articleEditor = articleEditor;
            _articleValidator = articleValidator;
        }

        public async Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync()
        {
            return await _articleProvider.GetAllArticlesAsync();
        }
        public async Task CreateArticleAsync(ArticleDTO article)
        {
            ValidateArticle(article);
            await _articleCreator.CreateArticleAsync(article);
        }
        public async Task<int> GetNextFreeArticleIdAsync()
        {
            return await _articleCreator.GetNextFreeArticleIdAsync();
        }
        public async Task DeleteArticleAsync(ArticleDTO article)
        {
            await _articleDeletor.DeleteArticleAsync(article);
        }
        public async Task SaveChangesToArticleAsync(ArticleDTO initialArticle, ArticleDTO editedArticle)
        {
            ValidateArticle(editedArticle);
            await _articleEditor.SaveChangesToArticleAsync(initialArticle, editedArticle);
        }
        private void ValidateArticle(ArticleDTO article)
        {
            if (!_articleValidator.Validate(article))
                throw new ValidationException();
                
        }

        private readonly IArticleProvider _articleProvider;
        private readonly IArticleCreator _articleCreator;
        private readonly IArticleDeletor _articleDeletor;
        private readonly IArticleEditor _articleEditor;
        private readonly IArticleValidator _articleValidator;
    }
}
