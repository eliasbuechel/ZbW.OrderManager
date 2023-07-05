using DataLayer.Customers.Models;

namespace DataLayer.Customers.Services.CustomerProviders
{
    public interface ICustomerProvider
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
    }
}
