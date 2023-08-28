using DataLayer.ArticleGroups.DTOs;
using DataLayer.Articles.DTOs;
using DataLayer.Customers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Base.DatabaseContext
{
    public class ManagerDbContext : DbContext
    {
        public ManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.LogTo(Console.WriteLine);

        public DbSet<CustomerDTO> Customers { get; set; }
        public DbSet<AddressDTO> Address { get; set; }
        public DbSet<ArticleGroupDTO> ArticleGroups { get; set; }
        public DbSet<ArticleDTO> Articles { get; set; }
    }
}