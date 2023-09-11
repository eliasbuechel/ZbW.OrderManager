using Microsoft.EntityFrameworkCore;

namespace DataLayer.Base.DatabaseContext
{
    public class ManagerDbContextFactory
    {
        public ManagerDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ManagerDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(_connectionString).Options;

            return new ManagerDbContext(options);
        }

        private readonly string _connectionString;
    }
}
