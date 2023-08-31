using DataLayer.ArticleGroups.DTOs;

namespace DataLayer.ArticleGroups.Exceptions
{
    public class NotContainingUpdatingArticleGroupInDatabaseException : Exception
    {
        public NotContainingUpdatingArticleGroupInDatabaseException(CreatedOrUpdatedArticleGroupDTO updatedArticleGroup)
        {
            UpdatedArticleGroup = updatedArticleGroup;
        }
        public NotContainingUpdatingArticleGroupInDatabaseException(string? message, CreatedOrUpdatedArticleGroupDTO updatedArticleGroup) : base(message)
        {
            UpdatedArticleGroup = updatedArticleGroup;
        }
        public NotContainingUpdatingArticleGroupInDatabaseException(string? message, Exception? innerException, CreatedOrUpdatedArticleGroupDTO updatedArticleGroup) : base(message, innerException)
        {
            UpdatedArticleGroup = updatedArticleGroup;
        }

        public CreatedOrUpdatedArticleGroupDTO UpdatedArticleGroup { get; }
    }
}
