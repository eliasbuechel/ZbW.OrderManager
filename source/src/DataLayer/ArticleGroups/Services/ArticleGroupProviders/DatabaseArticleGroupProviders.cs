using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Models;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.ArticleGroups.Services.ArticleGroupProviders
{
    public class DatabaseArticleGroupProviders : IArticleGroupProvider
    {
        private readonly ManagerDbContextFactory _dbContextFactory;

        public DatabaseArticleGroupProviders(ManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<ArticleGroup>> GetAllArticleGroups()
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            var articleGroups = await context.ArticleGroups
                    .Where(a => a.SuperiorArticleGroup == null)
                    .Include(a => a.SubordinateArticleGroups)
                    .Select(a => new ArticleGroup(a.Id, a.Name, GetSubordinateArticleGroupRecursive(context, a)))
                    .ToListAsync();

            return articleGroups;
        }

        private static ICollection<ArticleGroup> GetSubordinateArticleGroupRecursive(ManagerDbContext context, ArticleGroupDTO parent)
        {
            return context.ArticleGroups
                .Where(a => a.SuperiorArticleGroup.Id == parent.Id)
                .Select(a => new ArticleGroup(a.Id, a.Name, GetSubordinateArticleGroupRecursive(context, a)))
                .ToList();
        }
    }
}
