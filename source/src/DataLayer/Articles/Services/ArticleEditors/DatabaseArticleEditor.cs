using DataLayer.ArticleGroups.Exceptions;
using DataLayer.ArticleGroups.Models;
using DataLayer.Articles.DTOs;
using DataLayer.Articles.Exceptions;
using DataLayer.Articles.Models;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataLayer.Articles.Services.ArticleEditors
{
    public class DatabaseArticleEditor : IArticleEditor
    {
        private readonly ManagerDbContextFactory _managerDbContextFactory;

        public DatabaseArticleEditor(ManagerDbContextFactory managerDbContextFactory)
        {
            _managerDbContextFactory = managerDbContextFactory;
        }
        public async Task SaveChangesToArticleAsync(ArticleDTO initialArticle, ArticleDTO editedArticle)
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            Article? articleDTO = await context.Articles.Where(a => a.Id == initialArticle.Id).FirstOrDefaultAsync();

            if (articleDTO == null)
                throw new NotContainingArticleInDatabaseException(initialArticle);

            articleDTO.Name = editedArticle.Name;

            ArticleGroup? articleGroupDTO = await context.ArticleGroups.Where(ag => ag.Id == editedArticle.ArticleGroup.Id).FirstOrDefaultAsync();
            if (articleGroupDTO == null)
                throw new NotContainingArticleGroupInDatabaseException(editedArticle.ArticleGroup);

            articleDTO.ArticleGroup = articleGroupDTO;

            await context.SaveChangesAsync();
        }
    }
}
