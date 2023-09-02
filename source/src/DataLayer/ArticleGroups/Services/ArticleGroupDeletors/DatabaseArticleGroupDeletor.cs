using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Exceptions;
using DataLayer.ArticleGroups.Models;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.ArticleGroups.Services.ArticleGroupDeletors
{
    public class DatabaseArticleGroupDeletor : IArticleGroupDeletor
    {
        public DatabaseArticleGroupDeletor(ManagerDbContextFactory managerDbContextFactory)
        {
            _managerDbContextFactory = managerDbContextFactory;
        }

        public async Task DeleteArticleGroupAsync(ArticleGroupDTO articleGroupDTO)
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            ArticleGroup? articleGroup = await context.ArticleGroups
                                                      .Where(a => a.Id == articleGroupDTO.Id)
                                                      .FirstOrDefaultAsync()
                                                      ?? throw new NotContainingArticleGroupInDatabaseException("Not in database!", articleGroupDTO);

            await context.ArticleGroups.Where(a => a.Id == articleGroupDTO.Id).ExecuteDeleteAsync();
        }

        private readonly ManagerDbContextFactory _managerDbContextFactory;
    }
}
