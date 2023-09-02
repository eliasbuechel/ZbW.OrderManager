using DataLayer.Customers.DTOs;

namespace DataLayer.Customers.Services.CustomerCreators
{
    public interface ICustomerCreator
    {
        Task CreateCustomerAsync(CustomerDTO customer);
        Task<int> GetNextFreeCustomerIdAsync();
    }
}
