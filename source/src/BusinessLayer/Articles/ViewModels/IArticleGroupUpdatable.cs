using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.DTOs;

namespace BusinessLayer.Articles.ViewModels
{
    public interface IArticleGroupUpdatable : ILoadable, IErrorInfo
    {
        void UpdateArticleGroups(IEnumerable<ArticleGroupDTO> articleGroups);
    }
}
