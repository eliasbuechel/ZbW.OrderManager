using DataLayer.Customers.DTOs;

namespace DataLayer.Customers.Services.CustomerEditors
{
    public interface ICustomerEditor
    {
        public Task EditCustomer(CustomerDTO initialCustomer, CustomerDTO editedCustomer);
    }
}
