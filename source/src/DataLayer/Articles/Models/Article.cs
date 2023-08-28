using DataLayer.ArticleGroups.Models;

namespace DataLayer.Articles.Models
{
    public class Article
    {
        public int Id { get; }
        public string Name { get; }
        public ArticleGroup ArticleGroup { get; }

        public Article(int id, string name, ArticleGroup articleGroup)
        {
            Id = id;
            Name = name;
            ArticleGroup = articleGroup;
        }
    }
}
