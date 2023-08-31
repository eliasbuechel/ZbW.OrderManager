using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Models;
using DataLayer.Articles.DTOs;
using DataLayer.Articles.Models;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Articles.Services.ArticleProviders
{
    public class DatabaseArticleProvider : IArticleProvider
    {
        public DatabaseArticleProvider(ManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
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

        private readonly ManagerDbContextFactory _dbContextFactory;
    }
}
