using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Models;
using DataLayer.ArticleGroups.Services.ArticleGroupProviders;
using DataLayer.Articles.DTOs;
using DataLayer.Articles.Models;
using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Articles.Services.ArticleProviders
{
    public class DatabaseArticleProvider : IArticleProvider
    {
        public DatabaseArticleProvider(ManagerDbContextFactory dbContextFactory, IArticleGroupProvider articleGroupProvider)
        {
            _dbContextFactory = dbContextFactory;
            _articleGroupProvider = articleGroupProvider;
        }
        public async Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync()
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            ICollection<Article> articles = await context.Articles.Include(a => a.ArticleGroup).ToListAsync();
            ICollection<ArticleDTO> articleDTOs = new List<ArticleDTO>();

            foreach (Article a in articles)
            {
                ArticleDTO articleDTO = new ArticleDTO(a.Id, a.Name, new ArticleGroupDTO(a.ArticleGroup.Id, a.ArticleGroup.Name));

                articleDTOs.Add(articleDTO);
            }

            return articleDTOs;
        }

        public ArticleDTO GetArticle(int id)
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            return context.Articles
                .Where(a => a.Id == id)
                .Include(a => a.ArticleGroup)
                .Select(a => new ArticleDTO(
                    a.Id,
                    a.Name,
                    _articleGroupProvider.GetArticleGroup(a.ArticleGroup.Id)
                    )
                )
                .FirstOrDefault()
                ?? throw new NotInDatabaseException($"Not fount article with id '{id}' in database!");
        }

        private readonly ManagerDbContextFactory _dbContextFactory;
        private readonly IArticleGroupProvider _articleGroupProvider;
    }
}
