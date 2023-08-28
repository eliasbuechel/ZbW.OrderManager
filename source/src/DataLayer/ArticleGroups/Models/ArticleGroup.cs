namespace DataLayer.ArticleGroups.Models
{
    public class ArticleGroup
    {
        public ArticleGroup(int id, string name, ICollection<ArticleGroup> subordinateArticleGroups)
        {
            Id = id;
            Name = name;
            SubordinateArticleGroups = subordinateArticleGroups;
        }
        public ArticleGroup(int id, string name)
            : this(id, name, new List<ArticleGroup>())
        {
        }

        public int Id { get; }
        public string Name { get; }
        public ICollection<ArticleGroup> SubordinateArticleGroups { get; }
    }
}