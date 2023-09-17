namespace DataLayer.Customers.Exceptions
{
    public class AlreadyInDatabaseException : Exception
    {
        public AlreadyInDatabaseException(string? message) : base(message)
        {
        }
    }
}
