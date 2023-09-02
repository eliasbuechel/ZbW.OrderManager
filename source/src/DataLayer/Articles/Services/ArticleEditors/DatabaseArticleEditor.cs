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
        public async Task SaveChangesToArticleAsync(ArticleDTO initialArticle, ArticleDTO editedArticle)
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            ValidateArticle(editedArticle);

            Article? articleDTO = await context.Articles
                .Where(a => a.Id == initialArticle.Id)
                .FirstOrDefaultAsync()
                ?? throw new NotContainingArticleInDatabaseException(initialArticle);

            articleDTO.Name = editedArticle.Name;

            ArticleGroup? articleGroupDTO = await context.ArticleGroups
                .Where(ag => ag.Id == editedArticle.ArticleGroup.Id)
                .FirstOrDefaultAsync()
                ?? throw new NotContainingArticleGroupInDatabaseException(editedArticle.ArticleGroup);

            articleDTO.ArticleGroup = articleGroupDTO;

            await context.SaveChangesAsync();
        }

        private void ValidateArticle(ArticleDTO editedArticle)
        {
            if (!_articleValidator.Validate(editedArticle))
                throw new ValidationException();
        }

        private readonly ManagerDbContextFactory _managerDbContextFactory;
        private readonly IArticleValidator _articleValidator;
    }
}
