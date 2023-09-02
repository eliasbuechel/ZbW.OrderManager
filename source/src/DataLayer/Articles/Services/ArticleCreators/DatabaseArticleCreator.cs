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
        public async Task CreateArticleAsync(ArticleDTO article)
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            ValidateArticle(article);

            ArticleGroup? articleGroupDTO = await context.ArticleGroups
                .Where(ag => ag.Id == article.ArticleGroup.Id)
                .FirstOrDefaultAsync()
                ?? throw new NotContainingArticleGroupInDatabaseException("Database does not contain ArticleGroup reffered by the Article", article.ArticleGroup);

            Article articleDto = new Article()
            {
                Id = article.Id,
                Name = article.Name,
                ArticleGroup = articleGroupDTO
            };

            context.Articles.Add(articleDto);
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

        private void ValidateArticle(ArticleDTO article)
        {
            if (!_articleValidator.Validate(article))
                throw new ValidationException();
        }

        private readonly ManagerDbContextFactory _managerDbContextFactory;
        private readonly IArticleValidator _articleValidator;
    }
}
