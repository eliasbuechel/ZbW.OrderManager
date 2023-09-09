using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Exceptions;
using DataLayer.ArticleGroups.Models;
using DataLayer.ArticleGroups.Validation;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ArticleGroups.Services.ArticleGroupCreators
{
    public class DatabaseArticleGroupCreator : IArticleGroupCreator
    {
        public DatabaseArticleGroupCreator(ManagerDbContextFactory dbContextFactory, IArticleGroupValidator articleGroupValidator)
        {
            _dbContextFactory = dbContextFactory;
            _articleGroupValidator = articleGroupValidator;
        }

        public async Task CreateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO creatingArticleGroupDTO)
        {
            ManagerDbContext context = _dbContextFactory.CreateDbContext();
            ArticleGroup? superiorArticleGroup = null;

            ValidateArticleGroup(creatingArticleGroupDTO);
            
            if (creatingArticleGroupDTO.SuperiorArticleGroup != null)
            {
                superiorArticleGroup = await context.ArticleGroups
                                                   .Where(a => a.Id == creatingArticleGroupDTO.SuperiorArticleGroup.Id)
                                                   .SingleOrDefaultAsync()
                                                   ?? throw new NotContainingArticleGroupInDatabaseException("Superior article group not in database!", creatingArticleGroupDTO.SuperiorArticleGroup);
            }

            ArticleGroup articleGroup = new ArticleGroup()
            {
                Id = creatingArticleGroupDTO.Id,
                Name = creatingArticleGroupDTO.Name,
                SuperiorArticleGroup = superiorArticleGroup
            };

            await context.ArticleGroups.AddAsync(articleGroup);
            await context.SaveChangesAsync();
        }
        public async Task<int> GetNextFreeArticleGroupIdAsync()
        {
            ManagerDbContext context = _dbContextFactory.CreateDbContext();

            int maxId = 0;
            if (await context.ArticleGroups.AnyAsync())
                maxId = await context.ArticleGroups.MaxAsync(x => x.Id);

            return maxId + 1;
        }

        private void ValidateArticleGroup(IValidatableArticleGroup articleGroup)
        {
            if (!_articleGroupValidator.Validate(articleGroup))
                throw new ValidationException();
        }

        private readonly ManagerDbContextFactory _dbContextFactory;
        private readonly IArticleGroupValidator _articleGroupValidator;
    }
}