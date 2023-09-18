using DataLayer.Articles.DTOs;

namespace DataLayer.Orders.DTOs
{
    public class CreatingPositionDTO
    {
        public CreatingPositionDTO(int number, int ammount, ArticleDTO article)
        {
            Number = number;
            Ammount = ammount;
            Article = article;
        }

        public int Number { get; private set; }
        public int Ammount { get; private set; }
        public ArticleDTO Article { get; private set; }

        public void Update(CreatingPositionDTO position)
        {
            if (position.Number != Number)
                return;

            Number = position.Number;
            Ammount = position.Ammount;
            Article = position.Article;
        }
    }
}
