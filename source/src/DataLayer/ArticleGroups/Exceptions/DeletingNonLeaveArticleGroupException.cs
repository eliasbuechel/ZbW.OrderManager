using DataLayer.ArticleGroups.DTOs;

namespace DataLayer.ArticleGroups.Exceptions
{
    public class DeletingNonLeaveArticleGroupException : Exception
    {
        public DeletingNonLeaveArticleGroupException(ArticleGroupDTO articleGroup)
        {
            ArticleGroup = articleGroup;
        }
        public DeletingNonLeaveArticleGroupException(string? message, ArticleGroupDTO articleGroup) : base(message)
        {
            ArticleGroup = articleGroup;
        }
        public DeletingNonLeaveArticleGroupException(string? message, Exception? innerException, ArticleGroupDTO articleGroup) : base(message, innerException)
        {
            ArticleGroup = articleGroup;
        }

        public ArticleGroupDTO ArticleGroup { get; }
    }
}
