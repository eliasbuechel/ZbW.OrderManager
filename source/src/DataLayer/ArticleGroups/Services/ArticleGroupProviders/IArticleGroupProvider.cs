using DataLayer.ArticleGroups.Models;

namespace DataLayer.ArticleGroups.Services.ArticleGroupProviders
{
    public interface IArticleGroupProvider
    {
        Task<IEnumerable<ArticleGroup>> GetAllArticleGroups();
    }
}