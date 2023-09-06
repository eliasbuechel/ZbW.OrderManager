using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Exceptions;
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

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();
            IEnumerable<Customer> customerDTOs = await context.Customers.Include(c => c.Address).ToListAsync();

            return customerDTOs.Select(c => ToCustomer(c));
        }
        public async Task<IEnumerable<SerializableCustomerDTO>> GetAllSerializableCustomersAsync()
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            return await context.Customers
                .Include(c => c.Address)
                .Select(c => new SerializableCustomerDTO(
                    c.Id,
                    c.FirstName,
                    c.LastName,
                    new SerializableAddressDTO(
                        c.Address.StreetName,
                        c.Address.HouseNumber,
                        c.Address.City,
                        c.Address.PostalCode
                        ),
                    c.EmailAddress,
                    c.WebsiteURL,
                    c.Password
                    )
                )
                .ToListAsync();
        }
        public async Task<CustomerDTO> GetCustomerAsync(int id)
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();
            return await context.Customers
                .Where(c => c.Id == id)
                .Include(c => c.Address)
                .Select(c => ToCustomer(c))
                .FirstOrDefaultAsync()
                ?? throw new NotInDatabaseException($"Not containing customer with the id '{id}' in database");
        }

        private static CustomerDTO ToCustomer(Customer c)
        {
            CustomerDTO customer = new CustomerDTO(c.Id, c.FirstName, c.LastName, c.Address.StreetName, c.Address.HouseNumber, c.Address.City, c.Address.PostalCode, c.EmailAddress, c.WebsiteURL, c.Password);
            return customer;
        }
    }
}
