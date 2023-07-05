using DataLayer.Customers.Models;

namespace DataLayer.Customers.Services.CustomerDeletors
{
    public interface ICustomerDeletor
    {
        Task DeleteCustomer(Customer customer);
    }
}
