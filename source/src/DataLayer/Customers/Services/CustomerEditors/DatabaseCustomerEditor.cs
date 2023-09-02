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

        public async Task EditCustomer(CustomerDTO initialCustomer, CustomerDTO editedCustomer)
        {
            ManagerDbContext dbContext = _orderDbContextFactory.CreateDbContext();

            ValidateCustomer(editedCustomer);

            Customer? customerDTO = await dbContext.Customers
                .Include(c => c.Address)
                .Where(c =>
                    c.FirstName == initialCustomer.FirstName &&
                    c.LastName == initialCustomer.LastName &&
                    c.Address.StreetName == initialCustomer.StreetName &&
                    c.Address.City == initialCustomer.City &&
                    c.Address.PostalCode == initialCustomer.PostalCode &&
                    c.Address.City == initialCustomer.City &&
                    c.EmailAddress == initialCustomer.EmailAddress &&
                    c.WebsiteURL == initialCustomer.WebsiteURL &&
                    c.Password == initialCustomer.Password)
                .FirstOrDefaultAsync()
                ?? throw new NotContainingCustomerInDatabaseException(initialCustomer);

            EditCustomer(editedCustomer, customerDTO);
            await dbContext.SaveChangesAsync();
        }

        private void ValidateCustomer(CustomerDTO editedCustomer)
        {
            if (!_customerValidator.Validate(editedCustomer))
                throw new ValidationException();
        }
        private static void EditCustomer(CustomerDTO editedCustomer, Customer customerDTO)
        {
            customerDTO.FirstName = editedCustomer.FirstName;
            customerDTO.LastName = editedCustomer.LastName;
            customerDTO.Address.StreetName = editedCustomer.StreetName;
            customerDTO.Address.City = editedCustomer.City;
            customerDTO.Address.PostalCode = editedCustomer.PostalCode;
            customerDTO.Address.City = editedCustomer.City;
            customerDTO.EmailAddress = editedCustomer.EmailAddress;
            customerDTO.WebsiteURL = editedCustomer.WebsiteURL;
            customerDTO.Password = editedCustomer.Password;
        }

        private readonly ManagerDbContextFactory _orderDbContextFactory;
        private readonly ICustomerValidator _customerValidator;
    }
}
