using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Models;
using DataLayer.Customers.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Customers.Services.CustomerCreators
{
    public class DatabaseCustomerCreator : ICustomerCreator
    {
        public DatabaseCustomerCreator(ManagerDbContextFactory dbContextFactory, ICustomerValidator customerValidator)
        {
            _dbContextFactory = dbContextFactory;
            _customerValidator = customerValidator;
        }

        public async Task CreateCustomerAsync(CustomerDTO customer)
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            ValidateCustomer(customer);
            Customer customerDTO = ToCustomerDTO(customer);

            context.Customers.Add(customerDTO);
            await context.SaveChangesAsync();
        }
        public async Task<int> GetNextFreeCustomerIdAsync()
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            int maxId = 0;
            if (await context.Customers.AnyAsync())
                maxId = await context.Customers.MaxAsync(x => x.Id);

            return maxId + 1;
        }

        private void ValidateCustomer(CustomerDTO customer)
        {
            if (!_customerValidator.Validate(customer))
                throw new ValidationException();
        }
        private static Customer ToCustomerDTO(CustomerDTO customer)
        {
            return new Customer()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = new Address()
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

        private readonly ManagerDbContextFactory _dbContextFactory;
        private readonly ICustomerValidator _customerValidator;
    }
}
