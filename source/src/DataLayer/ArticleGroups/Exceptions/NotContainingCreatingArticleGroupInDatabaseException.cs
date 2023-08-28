using DataLayer.ArticleGroups.Models;

namespace DataLayer.ArticleGroups.Exceptions
{
    public class NotContainingCreatingArticleGroupInDatabaseException : Exception
    {
        public CreatingArticleGroup CreatingArticleGroup { get; }

        public NotContainingCreatingArticleGroupInDatabaseException(CreatingArticleGroup lookingForCreatingArticleGroup)
        {
            CreatingArticleGroup = lookingForCreatingArticleGroup;
        }
        public NotContainingCreatingArticleGroupInDatabaseException(string? message, CreatingArticleGroup lookingForCreatingArticleGroup) : base(message)
        {
            CreatingArticleGroup = lookingForCreatingArticleGroup;
        }
        public NotContainingCreatingArticleGroupInDatabaseException(string? message, Exception? innerException, CreatingArticleGroup lookingForCreatingArticleGroup) : base(message, innerException)
        {
            CreatingArticleGroup = lookingForCreatingArticleGroup;
        }
    }
}
