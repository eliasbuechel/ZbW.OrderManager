using DataLayer.Articles.DTOs;

namespace DataLayer.Orders.DTOs
{
    public class PositionDTO
    {
        public PositionDTO(int id, int number, ArticleDTO article, int ammount)
        {
            Id = id;
            Number = number;
            Article = article;
            Ammount = ammount;
        }

        public int Id { get; }
        public int Number { get; }
        public ArticleDTO Article { get; }
        public int Ammount { get; }
    }
}
