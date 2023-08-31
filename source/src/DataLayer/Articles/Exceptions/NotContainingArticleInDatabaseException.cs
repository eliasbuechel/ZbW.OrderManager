using DataLayer.Articles.DTOs;

namespace DataLayer.Articles.Exceptions
{
    public class NotContainingArticleInDatabaseException : Exception
    {
        public NotContainingArticleInDatabaseException(ArticleDTO article)
        {
            _article = article;
        }
        public NotContainingArticleInDatabaseException(string? message, ArticleDTO article) : base(message)
        {
            _article = article;
        }
        public NotContainingArticleInDatabaseException(string? message, Exception? innerException, ArticleDTO article) : base(message, innerException)
        {
            _article = article;
        }

        private readonly ArticleDTO _article;
    }
}
