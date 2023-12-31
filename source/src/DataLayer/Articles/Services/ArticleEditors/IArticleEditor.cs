﻿using DataLayer.Articles.DTOs;

namespace DataLayer.Articles.Services.ArticleEditors
{
    public interface IArticleEditor
    {
        Task SaveChangesToArticleAsync(ArticleDTO initialArticleDTO, ArticleDTO editedArticleDTO);
    }
}
