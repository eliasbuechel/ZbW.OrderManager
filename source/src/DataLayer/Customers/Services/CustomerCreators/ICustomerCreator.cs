using DataLayer.Customers.Models;

namespace DataLayer.Customers.Services.CustomerCreators
{
    public interface ICustomerCreator
    {
        Task CreateCustomer(Customer customer);
        Task<int> GetNextFreeCustomerIdAsync();
    }
}
