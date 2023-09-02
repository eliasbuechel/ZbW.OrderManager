using DataLayer.ArticleGroups.Validation;

namespace DataLayer.ArticleGroups.DTOs
{
    public class CreatedOrUpdatedArticleGroupDTO : IValidatableArticleGroup
    {
        public CreatedOrUpdatedArticleGroupDTO(int id, string name, ArticleGroupDTO? superiorArticleGroup)
        {
            Id = id;
            Name = name;
            SuperiorArticleGroup = superiorArticleGroup;
        }
        public CreatedOrUpdatedArticleGroupDTO(int id, string name) : this(id, name, null)
        { }

        public int Id { get; }
        public string Name { get; }
        public ArticleGroupDTO? SuperiorArticleGroup { get; }

    }
}
