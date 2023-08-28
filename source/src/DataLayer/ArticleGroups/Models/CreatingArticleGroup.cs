namespace DataLayer.ArticleGroups.Models
{
    public class CreatingArticleGroup
    {
        public CreatingArticleGroup(int id, string name, ArticleGroup? superiorArticleGroup)
        {
            Id = id;
            Name = name;
            SuperiorArticleGroup = superiorArticleGroup;
        }

        public CreatingArticleGroup(int id, string name) : this(id, name, null)
        { }

        public int Id { get; }
        public string Name { get; }
        public ArticleGroup? SuperiorArticleGroup { get; }

    }
}
