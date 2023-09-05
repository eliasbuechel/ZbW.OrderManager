using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Models;
using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.ArticleGroups.Services.ArticleGroupProviders
{
    public class DatabaseArticleGroupProviders : IArticleGroupProvider
    {
        public DatabaseArticleGroupProviders(ManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<ArticleGroupDTO>> GetAllArticleGroupsAsync()
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            IEnumerable<ArticleGroupDTO> articleGroupsDTOs = await context.ArticleGroups
                                                                        .Where(a => a.SuperiorArticleGroup == null)
                                                                        .Select(a => new ArticleGroupDTO(a.Id, a.Name))
                                                                        .ToListAsync();

            foreach (var articleGroupDTO in articleGroupsDTOs)
                AddSubordinateArticleGroupRec(context, articleGroupDTO);

            return articleGroupsDTOs;
        }
        public ArticleGroupDTO GetArticleGroup(int id)
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            ArticleGroupDTO articleGroupDTO = context.ArticleGroups
                .Where(a => a.SuperiorArticleGroup == null)
                .Select(a => new ArticleGroupDTO(a.Id, a.Name))
                .FirstOrDefault()
                ?? throw new NotInDatabaseException($"Not found article group with id '{id}' in database!");

            AddSubordinateArticleGroupRec(context, articleGroupDTO);

            return articleGroupDTO;
        }

        private static IEnumerable<ArticleGroup> IncludeArticleGroupRec(ArticleGroup articleGroup)
        {
            IEnumerable<ArticleGroup> subordinateArticleGroups = articleGroup.SubordinateArticleGroups;
            
            foreach (ArticleGroup subordinateArticleGroup in subordinateArticleGroups)
            {
                IncludeArticleGroupRec(subordinateArticleGroup);
            }

            return subordinateArticleGroups;
        }
        private static void AddSubordinateArticleGroupRec(ManagerDbContext context, ArticleGroupDTO articleGroup)
        {
            IEnumerable<ArticleGroup> subordinateArticleGroups = context.ArticleGroups.Where(a => a.SuperiorArticleGroup != null && a.SuperiorArticleGroup.Id == articleGroup.Id).ToList();

            foreach (ArticleGroup subordinateArticleGroup in subordinateArticleGroups)
            {
                ArticleGroupDTO subordinateArticleGroupDTO = new ArticleGroupDTO(subordinateArticleGroup.Id, subordinateArticleGroup.Name);
                articleGroup.AddSubordinateArticleGroup(subordinateArticleGroupDTO);
                AddSubordinateArticleGroupRec(context, subordinateArticleGroupDTO);
            }
        }

        private readonly ManagerDbContextFactory _dbContextFactory;
    }
}
