using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer.Base.DatabaseContext
{
    public class ManagerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ManagerDbContext>
    {
        ManagerDbContext IDesignTimeDbContextFactory<ManagerDbContext>.CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer("Server=.\\SQLSERVER_EB;Database=OrderManagerTestV2;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False;").Options;

            return new ManagerDbContext(options);
        }
    }
}
