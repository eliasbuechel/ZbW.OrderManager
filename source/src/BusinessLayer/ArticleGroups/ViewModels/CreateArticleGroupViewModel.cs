using BusinessLayer.Base.ViewModels;
using DataLayer.ArticleGroups.Models;

namespace BusinessLayer.ArticleGroups.ViewModels
{
    public class CreateArticleGroupViewModel : BaseViewModelWithErrorHandling
    {
        public ArticleGroup ArticleGroup { get; set; }
    }
}
