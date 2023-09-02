using DataLayer.Customers.DTOs;

namespace DataLayer.Customers.Services.CustomerDeletors
{
    public interface ICustomerDeletor
    {
        Task DeleteCustomerAsync(CustomerDTO customer);
    }
}
