namespace DataLayer.ArticleGroups.DTOs
{
    public class ArticleGroupForArticleDTO
    {
        public ArticleGroupForArticleDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; }
    }
}
