using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Exceptions;
using DataLayer.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Customers.Services.CustomerDeletors
{
    public class DatabaseCustomerDeletor : ICustomerDeletor
    {
        public async Task DeleteCustomer(Customer customer)
        {
            ManagerDbContext context = _dbContextFactory.CreateDbContext();

            CustomerDTO? toDeleteCustomer = await context.Customers.Include(c => c.Address).SingleOrDefaultAsync(c =>
                c.FirstName == customer.FirstName &&
                c.LastName == customer.LastName &&
                c.Address.StreetName == customer.StreetName &&
                c.Address.City == customer.City &&
                c.Address.PostalCode == customer.PostalCode &&
                c.Address.City == customer.City &&
                c.EmailAddress == customer.EmailAddress &&
                c.WebsiteURL == customer.WebsiteURL &&
                c.Password == customer.Password
            );

            if (toDeleteCustomer == null)
                throw new NotContainingCustomerInDatabaseException(customer);

            context.Customers.Remove(toDeleteCustomer);
            await context.SaveChangesAsync();
        }

        public DatabaseCustomerDeletor(ManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        private readonly ManagerDbContextFactory _dbContextFactory;
    }
}
