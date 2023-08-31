using DataLayer.Customers.DTOs;

namespace DataLayer.Customers.Services.CustomerCreators
{
    public interface ICustomerCreator
    {
        Task CreateCustomer(CustomerDTO customer);
        Task<int> GetNextFreeCustomerIdAsync();
    }
}
