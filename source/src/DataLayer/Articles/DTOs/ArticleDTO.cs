using DataLayer.ArticleGroups.DTOs;

namespace DataLayer.Articles.DTOs
{
    public class ArticleDTO
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
