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

        public async Task CreateCustomerAsync(CustomerDTO customerDTO)
        {
            using ManagerDbContext context = _dbContextFactory.CreateDbContext();

            ValidateCustomer(customerDTO);
            Customer customer = ToCustomerDTO(customerDTO);

            context.Customers.Add(customer);
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

        private void ValidateCustomer(IValidatableCustomer customer)
        {
            if (!_customerValidator.Validate(customer))
                throw new ValidationException();
        }
        private static Customer ToCustomerDTO(CustomerDTO customerDTO)
        {
            return new Customer()
            {
                Id = customerDTO.Id,
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Address = new Address()
                {
                    StreetName = customerDTO.StreetName,
                    HouseNumber = customerDTO.HouseNumber,
                    City = customerDTO.City,
                    PostalCode = customerDTO.PostalCode
                },
                EmailAddress = customerDTO.EmailAddress,
                WebsiteURL = customerDTO.WebsiteURL,
                Password = customerDTO.HashedPassword
            };
        }

        private readonly ManagerDbContextFactory _dbContextFactory;
        private readonly ICustomerValidator _customerValidator;
    }
}
