using DataLayer.ArticleGroups.DTOs;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.ArticleGroups.Services.ArticleGroupDeletors
{
    public class DatabaseArticleGroupDeletor : IArticleGroupDeletor
    {
        private readonly ManagerDbContextFactory _managerDbContextFactory;

        public DatabaseArticleGroupDeletor(ManagerDbContextFactory managerDbContextFactory)
        {
            _managerDbContextFactory = managerDbContextFactory;
        }

        public async Task DeleteArticleGroup(ArticleGroupDTO articleGroup)
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();
            await context.ArticleGroups.Where(a => a.Id == articleGroup.Id).ExecuteDeleteAsync();
        }
    }
}
