using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Customers.Services.CustomerProviders
{
    public class DatabaseCustomerProvider : ICustomerProvider
    {
        private readonly ManagerDbContextFactory _dbContextFactory;

        public DatabaseCustomerProvider(ManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();
            IEnumerable<CustomerDTO> customerDTOs = await context.Customers.Include(c => c.Address).ToListAsync();

            return customerDTOs.Select(c => ToCustomer(c));
        }

        private static Customer ToCustomer(CustomerDTO c)
        {
            Customer customer = new Customer(c.Id, c.FirstName, c.LastName, c.Address.StreetName, c.Address.HouseNumber, c.Address.City, c.Address.PostalCode, c.EmailAddress, c.WebsiteURL, c.Password);
            return customer;
        }
    }
}
