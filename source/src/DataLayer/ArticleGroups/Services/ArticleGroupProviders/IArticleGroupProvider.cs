using DataLayer.ArticleGroups.DTOs;

namespace DataLayer.ArticleGroups.Services.ArticleGroupProviders
{
    public interface IArticleGroupProvider
    {
        Task<IEnumerable<ArticleGroupDTO>> GetAllArticleGroupsAsync();
        ArticleGroupDTO GetArticleGroup(int id);
    }
}