using DataLayer.ArticleGroups.Exceptions;
using DataLayer.ArticleGroups.Models;
using DataLayer.Articles.DTOs;
using DataLayer.Articles.Models;
using DataLayer.Articles.Validation;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Articles.Services.ArticleCreators
{
    public class DatabaseArticleCreator : IArticleCreator
    {
        public DatabaseArticleCreator(ManagerDbContextFactory managerDbContextFactory, IArticleValidator articleValidator)
        {
            _managerDbContextFactory = managerDbContextFactory;
            _articleValidator = articleValidator;
        }
        public async Task CreateArticleAsync(ArticleDTO articleDTO)
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            ValidateArticle(articleDTO);

            ArticleGroup? articleGroupDTO = await context.ArticleGroups
                .Where(ag => ag.Id == articleDTO.ArticleGroup.Id)
                .FirstOrDefaultAsync()
                ?? throw new NotContainingArticleGroupInDatabaseException("Database does not contain ArticleGroup reffered by the Article", articleDTO.ArticleGroup);

            Article article = new Article()
            {
                Id = articleDTO.Id,
                Name = articleDTO.Name,
                ArticleGroup = articleGroupDTO
            };

            context.Articles.Add(article);
            await context.SaveChangesAsync();
        }
        public async Task<int> GetNextFreeArticleIdAsync()
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            int maxId = 0;
            if (await context.Articles.AnyAsync())
                maxId = await context.Articles.MaxAsync(x => x.Id);

            return maxId + 1;
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
