namespace DataLayer.Customers.Exceptions
{
    public class NotInDatabaseException : Exception
    {
        public NotInDatabaseException(string? message) : base(message)
        {
        }
    }
}
