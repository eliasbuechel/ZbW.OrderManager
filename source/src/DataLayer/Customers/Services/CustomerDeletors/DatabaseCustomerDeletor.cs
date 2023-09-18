using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Exceptions;
using DataLayer.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Customers.Services.CustomerDeletors
{
    public class DatabaseCustomerDeletor : ICustomerDeletor
    {
        public DatabaseCustomerDeletor(ManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task DeleteCustomerAsync(CustomerDTO customerDTO)
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            Customer customer = await context.Set<Customer>()
                .SingleOrDefaultAsync(c => c.Id == customerDTO.Id)
                ?? throw new NotContainingCustomerInDatabaseException(customerDTO);

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
        }

        private readonly ManagerDbContextFactory _dbContextFactory;
    }
}
