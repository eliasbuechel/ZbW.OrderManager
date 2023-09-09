using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Exceptions;
using DataLayer.Customers.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLayer.Customers.Services.CustomerProviders
{
    public class DatabaseCustomerProvider : ICustomerProvider
    {
        public DatabaseCustomerProvider(ManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            IEnumerable<CustomerDTO> customers = await context.Customers
                .Include(c => c.Address)
                .Select(SelectCustomerDTO())
                .ToListAsync();

            return customers;
        }
        public async Task<CustomerDTO> GetCustomerAsync(int id)
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            CustomerDTO customer = await context.Customers
                .Where(c => c.Id == id)
                .Include(c => c.Address)
                .Select(SelectCustomerDTO())
                .FirstOrDefaultAsync()
                ?? throw new NotInDatabaseException($"Not containing customer with the id '{id}' in database");

            return customer;
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

        private static Expression<Func<Customer, CustomerDTO>> SelectCustomerDTO()
        {
            return c => new CustomerDTO(
                                c.Id,
                                c.FirstName,
                                c.LastName,
                                c.Address.StreetName,
                                c.Address.HouseNumber,
                                c.Address.City,
                                c.Address.PostalCode,
                                c.EmailAddress,
                                c.WebsiteURL,
                                c.Password
                                );
        }

        private readonly ManagerDbContextFactory _dbContextFactory;
    }
}
