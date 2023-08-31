namespace DataLayer.ArticleGroups.Exceptions
{
    public class InvalidDataException<T> : Exception
    {
        public InvalidDataException(T item)
        {
            Item = item;
        }
        public InvalidDataException(string? message, T item) : base(message)
        {
            Item = item;
        }
        public InvalidDataException(string? message, Exception? innerException, T item) : base(message, innerException)
        {
            Item = item;
        }

        public T Item { get; }
    }
}
