using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Exceptions;
using DataLayer.Customers.Models;
using DataLayer.Customers.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Customers.Services.CustomerEditors
{
    public class DatabaseCustomerEditor : ICustomerEditor
    {
        public DatabaseCustomerEditor(ManagerDbContextFactory orderDbContextFactory, ICustomerValidator customerValidator)
        {
            _orderDbContextFactory = orderDbContextFactory;
            _customerValidator = customerValidator;
        }

        public async Task EditCustomerAsync(CustomerDTO initialCustomerDTO, CustomerDTO editedCustomerDTO)
        {
            ManagerDbContext dbContext = _orderDbContextFactory.CreateDbContext();

            ValidateCustomer(editedCustomerDTO);

            Customer? customerDTO = await dbContext.Customers
                .Include(c => c.Address)
                .Where(c =>
                    c.FirstName == initialCustomerDTO.FirstName &&
                    c.LastName == initialCustomerDTO.LastName &&
                    c.Address.StreetName == initialCustomerDTO.StreetName &&
                    c.Address.City == initialCustomerDTO.City &&
                    c.Address.PostalCode == initialCustomerDTO.PostalCode &&
                    c.Address.City == initialCustomerDTO.City &&
                    c.EmailAddress == initialCustomerDTO.EmailAddress &&
                    c.WebsiteURL == initialCustomerDTO.WebsiteURL &&
                    c.Password == initialCustomerDTO.Password)
                .FirstOrDefaultAsync()
                ?? throw new NotContainingCustomerInDatabaseException(initialCustomerDTO);

            EditCustomer(editedCustomerDTO, customerDTO);
            await dbContext.SaveChangesAsync();
        }

        private void ValidateCustomer(IValidatableCustomer customer)
        {
            if (!_customerValidator.Validate(customer))
                throw new ValidationException();
        }
        private static void EditCustomer(CustomerDTO editedCustomerDTO, Customer customer)
        {
            customer.FirstName = editedCustomerDTO.FirstName;
            customer.LastName = editedCustomerDTO.LastName;
            customer.Address.StreetName = editedCustomerDTO.StreetName;
            customer.Address.City = editedCustomerDTO.City;
            customer.Address.PostalCode = editedCustomerDTO.PostalCode;
            customer.Address.City = editedCustomerDTO.City;
            customer.EmailAddress = editedCustomerDTO.EmailAddress;
            customer.WebsiteURL = editedCustomerDTO.WebsiteURL;
            customer.Password = editedCustomerDTO.Password;
        }

        private readonly ManagerDbContextFactory _orderDbContextFactory;
        private readonly ICustomerValidator _customerValidator;
    }
}
