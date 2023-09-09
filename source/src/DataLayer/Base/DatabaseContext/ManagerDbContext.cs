using DataLayer.ArticleGroups.Models;
using DataLayer.Articles.Models;
using DataLayer.Customers.Models;
using DataLayer.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Base.DatabaseContext
{
    public class ManagerDbContext : DbContext
    {
        public ManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<ArticleGroup> ArticleGroups { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}