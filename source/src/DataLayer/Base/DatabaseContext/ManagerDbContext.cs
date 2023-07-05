using DataLayer.Customers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Base.DatabaseContext
{
    public class ManagerDbContext : DbContext
    {
        public ManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CustomerDTO> Customers { get; set; }
        public DbSet<AddressDTO> Address { get; set; }
    }
}