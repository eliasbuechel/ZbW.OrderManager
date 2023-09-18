using DataLayer.Customers.DTOs;

namespace DataLayer.Customers.Services.CustomerCreators
{
    public interface ICustomerCreator
    {
        Task CreateCustomerAsync(CustomerDTO customerDTO);
        Task<int> GetNextFreeCustomerIdAsync();
    }
}
