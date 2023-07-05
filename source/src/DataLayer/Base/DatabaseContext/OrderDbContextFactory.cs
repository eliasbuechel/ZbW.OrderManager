using Microsoft.EntityFrameworkCore;

namespace DataLayer.Base.DatabaseContext
{
    public class OrderDbContextFactory
    {
        private readonly string _connectionString;

        public OrderDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public OrderDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(_connectionString).Options;

            return new OrderDbContext(options);
        }
    }
}
