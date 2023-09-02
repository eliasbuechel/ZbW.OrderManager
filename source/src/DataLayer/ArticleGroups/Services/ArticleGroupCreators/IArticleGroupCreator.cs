using DataLayer.ArticleGroups.DTOs;

namespace DataLayer.ArticleGroups.Services.ArticleGroupCreators
{
    public interface IArticleGroupCreator
    {
        Task CreateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO creatingArticleGroup);
        Task<int> GetNextFreeArticleGroupIdAsync();
    }
}
