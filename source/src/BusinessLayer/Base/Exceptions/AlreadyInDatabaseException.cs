namespace BusinessLayer.Base.Exceptions
{
    public class AlreadyInDatabaseException : Exception
    {
        public AlreadyInDatabaseException(string? message) : base(message)
        {
        }
    }
}
