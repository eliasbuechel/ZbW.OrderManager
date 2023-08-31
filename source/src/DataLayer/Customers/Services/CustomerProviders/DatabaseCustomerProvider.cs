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

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();
            IEnumerable<Customer> customerDTOs = await context.Customers.Include(c => c.Address).ToListAsync();

            return customerDTOs.Select(c => ToCustomer(c));
        }

        private static CustomerDTO ToCustomer(Customer c)
        {
            CustomerDTO customer = new CustomerDTO(c.Id, c.FirstName, c.LastName, c.Address.StreetName, c.Address.HouseNumber, c.Address.City, c.Address.PostalCode, c.EmailAddress, c.WebsiteURL, c.Password);
            return customer;
        }
    }
}
