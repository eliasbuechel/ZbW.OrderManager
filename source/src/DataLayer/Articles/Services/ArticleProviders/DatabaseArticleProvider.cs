using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Models;
using DataLayer.Articles.Models;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Articles.Services.ArticleProviders
{
    public class DatabaseArticleProvider : IArticleProvider
    {
        private readonly ManagerDbContextFactory _dbContextFactory;

        public DatabaseArticleProvider(ManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            IEnumerable<Article> articles = await context
                .Articles
                .Include(a => a.ArticleGroup)
                .Select(a => new Article(a.Id, a.Name, new ArticleGroup(a.Id, a.ArticleGroup.Name, GetSubordinateArticleGroupRecursive(context, a.ArticleGroup))))
                .ToListAsync();

            return articles;
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
