using DataLayer.ArticleGroups.Models;

namespace BusinessLayer.Articles.ViewModels
{
    public interface IArticleGroupUpdatable
    {
        void UpdateArticleGroups(IEnumerable<ArticleGroup> articleGroups);
        string ErrorMessage { set; }
        bool IsLoading { set; }
    }
}
