using DataLayer.ArticleGroups.Exceptions;
using DataLayer.ArticleGroups.Models;
using DataLayer.Articles.DTOs;
using DataLayer.Articles.Exceptions;
using DataLayer.Articles.Models;
using DataLayer.Articles.Validation;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DataLayer.Articles.Services.ArticleEditors
{
    public class DatabaseArticleEditor : IArticleEditor
    {
        public DatabaseArticleEditor(ManagerDbContextFactory managerDbContextFactory, IArticleValidator articleValidator)
        {
            _managerDbContextFactory = managerDbContextFactory;
            _articleValidator = articleValidator;
        }
        public async Task SaveChangesToArticleAsync(ArticleDTO initialArticleDTO, ArticleDTO editedArticleDTO)
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            ValidateArticle(editedArticleDTO);

            Article? article = await context.Articles
                .Where(a => a.Id == initialArticleDTO.Id)
                .FirstOrDefaultAsync()
                ?? throw new NotContainingArticleInDatabaseException(initialArticleDTO);

            article.Name = editedArticleDTO.Name;

            ArticleGroup? articleGroup = await context.ArticleGroups
                .Where(ag => ag.Id == editedArticleDTO.ArticleGroup.Id)
                .FirstOrDefaultAsync()
                ?? throw new NotContainingArticleGroupInDatabaseException(editedArticleDTO.ArticleGroup);

            article.ArticleGroup = articleGroup;

            await context.SaveChangesAsync();
        }

        private void ValidateArticle(IValidatableArticle article)
        {
            if (!_articleValidator.Validate(article))
                throw new ValidationException();
        }

        private readonly ManagerDbContextFactory _managerDbContextFactory;
        private readonly IArticleValidator _articleValidator;
    }
}
