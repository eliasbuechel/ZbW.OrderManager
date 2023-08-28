using DataLayer.Articles.Models;

namespace DataLayer.Articles.Exceptions
{
    public class NotContainingArticleInDatabaseException : Exception
    {
        public NotContainingArticleInDatabaseException(Article article)
        {
            _article = article;
        }
        public NotContainingArticleInDatabaseException(string? message, Article article) : base(message)
        {
            _article = article;
        }
        public NotContainingArticleInDatabaseException(string? message, Exception? innerException, Article article) : base(message, innerException)
        {
            _article = article;
        }

        private readonly Article _article;
    }
}
