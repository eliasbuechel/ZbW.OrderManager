using DataLayer.Customers.DTOs;

namespace DataLayer.Customers.Services.CustomerDeletors
{
    public interface ICustomerDeletor
    {
        Task DeleteCustomer(CustomerDTO customer);
    }
}
