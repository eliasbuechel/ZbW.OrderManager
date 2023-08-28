using DataLayer.ArticleGroups.Models;

namespace DataLayer.ArticleGroups.Exceptions
{
    public class DeletingNonLeaveArticleGroupException : Exception
    {
        public DeletingNonLeaveArticleGroupException(ArticleGroup articleGroup)
        {
            ArticleGroup = articleGroup;
        }
        public DeletingNonLeaveArticleGroupException(string? message, ArticleGroup articleGroup) : base(message)
        {
            ArticleGroup = articleGroup;
        }
        public DeletingNonLeaveArticleGroupException(string? message, Exception? innerException, ArticleGroup articleGroup) : base(message, innerException)
        {
            ArticleGroup = articleGroup;
        }

        public ArticleGroup ArticleGroup { get; }
    }
}
