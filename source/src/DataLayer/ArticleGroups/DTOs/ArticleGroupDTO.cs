using DataLayer.ArticleGroups.Validation;

namespace DataLayer.ArticleGroups.DTOs
{
    public class ArticleGroupDTO : IValidatableArticleGroup
    {
        public ArticleGroupDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name
        {
            get => _name;
            private set
            {
                if (_name == value)
                    return;

                _name = value;
            }
        }
        public ArticleGroupDTO? SuperiorArticleGroup
        {
            get => _superiorArticleGroup;
            set
            {
                if (AreSame(_superiorArticleGroup, value))
                    return;

                _superiorArticleGroup?._subordinateArticleGroups.Remove(this);
                value?._subordinateArticleGroups.Add(this);

                _superiorArticleGroup = value;
            }
        }
        public ICollection<ArticleGroupDTO> SubordinateArticleGroups => _subordinateArticleGroups;
        public void Update(CreatedOrUpdatedArticleGroupDTO modifyedArticleGroup)
        {
            if (Id != modifyedArticleGroup.Id)
                return;

            Name = modifyedArticleGroup.Name;
            SuperiorArticleGroup = modifyedArticleGroup.SuperiorArticleGroup;
        }
        public void AddSubordinateArticleGroup(ArticleGroupDTO articleGroup)
        {
            articleGroup._superiorArticleGroup = this;
            _subordinateArticleGroups.Add(articleGroup);
        }

        private static bool AreSame(ArticleGroupDTO? a, ArticleGroupDTO? b)
        {
            if (a == null)
            {
                return b == null;
            }

            if (b == null)
                return false;

            return a.Id == b.Id;
        }

        private string _name = string.Empty;
        private ArticleGroupDTO? _superiorArticleGroup;
        private readonly ICollection<ArticleGroupDTO> _subordinateArticleGroups = new List<ArticleGroupDTO>();
    }
}