﻿using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Exceptions;
using DataLayer.Articles.DTOs;
using DataLayer.Articles.Models;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Articles.Services.ArticleCreator
{
    public class DatabaseArticleCreator : IArticleCreator
    {
        private readonly ManagerDbContextFactory _managerDbContextFactory;

        public DatabaseArticleCreator(ManagerDbContextFactory managerDbContextFactory)
        {
            _managerDbContextFactory = managerDbContextFactory;
        }
        public async Task CreateArticleAsync(Article article)
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            ArticleGroupDTO? articleGroupDTO = await context.ArticleGroups.Where(ag => ag.Id == article.ArticleGroup.Id).FirstOrDefaultAsync();

            if (articleGroupDTO == null)
            {
                throw new NotContainingArticleGroupInDatabaseException("Database does not contain ArticleGroup reffered by the Article", article.ArticleGroup);
            }

            ArticleDTO articleDto = new ArticleDTO()
            {
                Id = article.Id,
                Name = article.Name,
                ArticleGroup = articleGroupDTO
            };

            context.Articles.Add(articleDto);
            await context.SaveChangesAsync();
        }

        public async Task<int> GetNextFreeArticleIdAsync()
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            int maxId = 0;
            if (await context.Articles.CountAsync() != 0)
                maxId = await context.Articles.MaxAsync(x => x.Id);

            return maxId + 1;
        }
    }
}
