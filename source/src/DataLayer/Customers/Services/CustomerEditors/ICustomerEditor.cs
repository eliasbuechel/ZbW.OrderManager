using DataLayer.Customers.Models;

namespace DataLayer.Customers.Services.CustomerEditors
{
    public interface ICustomerEditor
    {
        public Task EditCustomer(Customer initialCustomer, Customer editedCustomer);
    }
}
