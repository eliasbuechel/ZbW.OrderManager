using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Customers.Services.CustomerCreators
{
    public class DatabaseCustomerCreator : ICustomerCreator
    {
        public DatabaseCustomerCreator(OrderDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateCustomer(Customer customer)
        {
            using OrderDbContext context = _dbContextFactory.CreateDbContext();
            CustomerDTO customerDTO = ToCustomerDTO(customer);

            context.Customers.Add(customerDTO);
            await context.SaveChangesAsync();
        }

        public async Task<int> GetNextFreeCustomerIdAsync()
        {
            using OrderDbContext context = _dbContextFactory.CreateDbContext();

            int maxId = 0;
            if (await context.Customers.CountAsync() != 0)
                maxId = await context.Customers.MaxAsync(x => x.Id);

            return maxId + 1;
        }

        private static CustomerDTO ToCustomerDTO(Customer customer)
        {
            return new CustomerDTO()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = new AddressDTO()
                {
                    StreetName = customer.StreetName,
                    HouseNumber = customer.HouseNumber,
                    City = customer.City,
                    PostalCode = customer.PostalCode
                },
                EmailAddress = customer.EmailAddress,
                WebsiteURL = customer.WebsiteURL,
                Password = customer.Password
            };
        }

        private readonly OrderDbContextFactory _dbContextFactory;
    }
}
