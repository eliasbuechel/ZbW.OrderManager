using DataLayer.Articles.DTOs;

namespace DataLayer.Articles.Exceptions
{
    public class NotContainingArticleInDatabaseException : Exception
    {
        public NotContainingArticleInDatabaseException(ArticleDTO article)
        {
            Article = article;
        }
        public NotContainingArticleInDatabaseException(string? message, ArticleDTO article) : base(message)
        {
            Article = article;
        }
        public NotContainingArticleInDatabaseException(string? message, Exception? innerException, ArticleDTO article) : base(message, innerException)
        {
            Article = article;
        }

        public ArticleDTO Article { get; }
    }
}
