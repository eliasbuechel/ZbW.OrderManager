using DataLayer.ArticleGroups.DTOs;
using DataLayer.Articles.Validation;

namespace DataLayer.Articles.DTOs
{
    public class ArticleDTO : IValidatableArticle
    {
        public int Id { get; }
        public string Name { get; }
        public ArticleGroupDTO ArticleGroup { get; }

        public ArticleDTO(int id, string name, ArticleGroupDTO articleGroup)
        {
            Id = id;
            Name = name;
            ArticleGroup = articleGroup;
        }
    }
}
