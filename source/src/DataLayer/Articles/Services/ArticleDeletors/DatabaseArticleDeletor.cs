﻿using DataLayer.Articles.DTOs;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Articles.Services.ArticleDeletors
{
    public class DatabaseArticleDeletor : IArticleDeletor
    {
        public DatabaseArticleDeletor(ManagerDbContextFactory managerDbContextFactory)
        {
            _managerDbContextFactory = managerDbContextFactory;
        }
        public async Task DeleteArticleAsync(ArticleDTO articleDTO)
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();
            context.Articles.Where(a => a.Id == articleDTO.Id).ExecuteDelete();
            await context.SaveChangesAsync();
        }

        private readonly ManagerDbContextFactory _managerDbContextFactory;
    }
}
