using DataLayer.ArticleGroups.Models;

namespace DataLayer.ArticleGroups.Services.ArticleGroupDeletors
{
    public interface IArticleGroupDeletor
    {
        Task DeleteArticleGroup(ArticleGroup articleGroup);
    }
}
