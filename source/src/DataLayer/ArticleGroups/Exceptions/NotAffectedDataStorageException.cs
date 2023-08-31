namespace DataLayer.ArticleGroups.Exceptions
{
    public class NotAffectedDataStorageException : Exception
    {
        public NotAffectedDataStorageException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
