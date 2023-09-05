using BusinessLayer.Base.ViewModels;
using DataLayer.Articles.DTOs;

namespace BusinessLayer.Articles.ViewModels
{
    public interface IArticleUpdatable : IUpdatable
    {
        void UpdateArticles(IEnumerable<ArticleDTO> articles);
    }
}
