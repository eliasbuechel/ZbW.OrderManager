using DataLayer.ArticleGroups.Models;

namespace DataLayer.ArticleGroups.Services.ArticleGroupCreators
{
    public interface IArticleGroupCreator
    {
        Task CreateArticleGroup(CreatingArticleGroup creatingArticleGroup);
        Task<int> GetNextFreeArticleGroupIdAsync();
    }
}
