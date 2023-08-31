using DataLayer.ArticleGroups.DTOs;

namespace DataLayer.ArticleGroups.Exceptions
{
    public class NotContainingArticleGroupInDatabaseException : Exception
    {
        public NotContainingArticleGroupInDatabaseException(ArticleGroupDTO articleGroup)
        {
            ArticleGroup = articleGroup;
        }

        public NotContainingArticleGroupInDatabaseException(string? message, ArticleGroupDTO articleGroup) : base(message)
        {
            ArticleGroup = articleGroup;
        }

        public NotContainingArticleGroupInDatabaseException(string? message, Exception? innerException, ArticleGroupDTO articleGroup) : base(message, innerException)
        {
            ArticleGroup = articleGroup;
        }

        public ArticleGroupDTO ArticleGroup { get; }
    }
}
