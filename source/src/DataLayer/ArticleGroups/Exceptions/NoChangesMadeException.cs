namespace DataLayer.ArticleGroups.Exceptions
{
    public class NoChangesMadeException<T> : Exception
    {
        public NoChangesMadeException(T item)
        {
            Item = item;
        }
        public NoChangesMadeException(string? message, T item) : base(message)
        {
            Item = item;
        }
        public NoChangesMadeException(string? message, Exception? innerException, T item) : base(message, innerException)
        {
            Item = item;
        }

        public T Item { get; }
    }
}
