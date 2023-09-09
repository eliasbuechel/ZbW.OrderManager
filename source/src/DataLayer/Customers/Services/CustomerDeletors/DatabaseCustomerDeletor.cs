using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Exceptions;
using DataLayer.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Customers.Services.CustomerDeletors
{
    public class DatabaseCustomerDeletor : ICustomerDeletor
    {
        public async Task DeleteCustomerAsync(CustomerDTO customerDTO)
        {
            ManagerDbContext context = _dbContextFactory.CreateDbContext();

            Customer? customer = await context.Customers
                .SingleOrDefaultAsync(c => c.Id == customerDTO.Id)
                ?? throw new NotContainingCustomerInDatabaseException(customerDTO);

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
        }

        public DatabaseCustomerDeletor(ManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        private readonly ManagerDbContextFactory _dbContextFactory;
    }
}
