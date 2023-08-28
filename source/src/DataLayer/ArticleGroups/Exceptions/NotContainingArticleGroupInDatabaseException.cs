using DataLayer.ArticleGroups.Models;

namespace DataLayer.ArticleGroups.Exceptions
{
    public class NotContainingArticleGroupInDatabaseException : Exception
    {
        public NotContainingArticleGroupInDatabaseException(ArticleGroup articleGroup)
        {
            ArticleGroup = articleGroup;
        }

        public NotContainingArticleGroupInDatabaseException(string? message, ArticleGroup articleGroup) : base(message)
        {
            ArticleGroup = articleGroup;
        }

        public NotContainingArticleGroupInDatabaseException(string? message, Exception? innerException, ArticleGroup articleGroup) : base(message, innerException)
        {
            ArticleGroup = articleGroup;
        }

        public ArticleGroup ArticleGroup { get; }
    }
}
