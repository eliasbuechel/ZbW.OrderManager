using DataLayer.ArticleGroups.DTOs;

namespace DataLayer.ArticleGroups.Exceptions
{
    public class NotContainingCreatingArticleGroupInDatabaseException : Exception
    {
        public CreatedOrUpdatedArticleGroupDTO CreatingArticleGroup { get; }

        public NotContainingCreatingArticleGroupInDatabaseException(CreatedOrUpdatedArticleGroupDTO lookingForCreatingArticleGroup)
        {
            CreatingArticleGroup = lookingForCreatingArticleGroup;
        }
        public NotContainingCreatingArticleGroupInDatabaseException(string? message, CreatedOrUpdatedArticleGroupDTO lookingForCreatingArticleGroup) : base(message)
        {
            CreatingArticleGroup = lookingForCreatingArticleGroup;
        }
        public NotContainingCreatingArticleGroupInDatabaseException(string? message, Exception? innerException, CreatedOrUpdatedArticleGroupDTO lookingForCreatingArticleGroup) : base(message, innerException)
        {
            CreatingArticleGroup = lookingForCreatingArticleGroup;
        }
    }
}
