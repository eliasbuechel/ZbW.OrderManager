using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Exceptions;
using DataLayer.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Customers.Services.CustomerEditors
{
    public class DatabaseCustomerEditor : ICustomerEditor
    {
        public DatabaseCustomerEditor(ManagerDbContextFactory orderDbContextFactory)
        {
            _orderDbContextFactory = orderDbContextFactory;
        }

        public async Task EditCustomer(CustomerDTO initialCustomer, CustomerDTO editedCustomer)
        {
            ManagerDbContext dbContext = _orderDbContextFactory.CreateDbContext();
            Customer? customerDTO = await dbContext.Customers.Include(c => c.Address).Where(c =>
                c.FirstName == initialCustomer.FirstName &&
                c.LastName == initialCustomer.LastName &&
                c.Address.StreetName == initialCustomer.StreetName &&
                c.Address.City == initialCustomer.City &&
                c.Address.PostalCode == initialCustomer.PostalCode &&
                c.Address.City == initialCustomer.City &&
                c.EmailAddress == initialCustomer.EmailAddress &&
                c.WebsiteURL == initialCustomer.WebsiteURL &&
                c.Password == initialCustomer.Password
                ).FirstOrDefaultAsync();

            if (customerDTO == null)
                throw new NotContainingCustomerInDatabaseException(initialCustomer);

            EditCustomer(editedCustomer, customerDTO);
            await dbContext.SaveChangesAsync();
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
    }
}
