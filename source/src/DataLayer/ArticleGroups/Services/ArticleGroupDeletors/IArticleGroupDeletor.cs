using DataLayer.ArticleGroups.DTOs;

namespace DataLayer.ArticleGroups.Services.ArticleGroupDeletors
{
    public interface IArticleGroupDeletor
    {
        Task DeleteArticleGroup(ArticleGroupDTO articleGroup);
    }
}
