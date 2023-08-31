using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Exceptions;
using DataLayer.ArticleGroups.Models;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.ArticleGroups.Services.ArticleGroupCreators
{
    public class DatabaseArticleGroupCreator : IArticleGroupCreator
    {
        private readonly ManagerDbContextFactory _dbContextFactory;

        public DatabaseArticleGroupCreator(ManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateArticleGroup(CreatedOrUpdatedArticleGroupDTO creatingArticleGroup)
        {
            ManagerDbContext context = _dbContextFactory.CreateDbContext();

            if (creatingArticleGroup.SuperiorArticleGroup == null)
            {
                ArticleGroup creatingRootArticleGroupDTO = new ArticleGroup()
                {
                    Id = creatingArticleGroup.Id,
                    Name = creatingArticleGroup.Name
                };

                await context.AddAsync(creatingRootArticleGroupDTO);
                await context.SaveChangesAsync();
                return;
            }

            ArticleGroup? superiorArticleGroupDTO = await context.ArticleGroups.Where(a => a.Id == creatingArticleGroup.SuperiorArticleGroup.Id).SingleOrDefaultAsync();
            if (superiorArticleGroupDTO == null)
                throw new NotContainingCreatingArticleGroupInDatabaseException(creatingArticleGroup);

            ArticleGroup creatingArticleGroupDTO = new ArticleGroup()
            {
                Id = creatingArticleGroup.Id,
                Name = creatingArticleGroup.Name,
                SuperiorArticleGroup = superiorArticleGroupDTO
            };

            await context.AddAsync(creatingArticleGroupDTO);
            await context.SaveChangesAsync();
        }

        public async Task<int> GetNextFreeArticleGroupIdAsync()
        {
            ManagerDbContext context = _dbContextFactory.CreateDbContext();

            int maxId = 0;
            if (await context.ArticleGroups.CountAsync() != 0)
                maxId = await context.ArticleGroups.MaxAsync(x => x.Id);

            return maxId + 1;
        }
    }
}
