using DataLayer.ArticleGroups.DTOs;

namespace DataLayer.ArticleGroups.Services.ArticleGroupCreators
{
    public interface IArticleGroupCreator
    {
        Task CreateArticleGroup(CreatedOrUpdatedArticleGroupDTO creatingArticleGroup);
        Task<int> GetNextFreeArticleGroupIdAsync();
    }
}
