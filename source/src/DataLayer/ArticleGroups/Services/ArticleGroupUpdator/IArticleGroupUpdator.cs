using DataLayer.ArticleGroups.DTOs;

namespace DataLayer.ArticleGroups.Services.ArticleGroupUpdator
{
    public interface IArticleGroupUpdator
    {
        Task UpdateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO articleGroup);
    }
}
