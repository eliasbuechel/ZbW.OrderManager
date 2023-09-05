using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.DTOs;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public interface IArticleGroupUpdatable : IUpdatable
    {
        void UpdateArticleGroups(IEnumerable<ArticleGroupDTO> articleGroups);
    }
}
