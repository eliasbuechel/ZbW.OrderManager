using DataLayer.Customers.DTOs;

namespace DataLayer.Customers.Services.CustomerEditors
{
    public interface ICustomerEditor
    {
        public Task EditCustomerAsync(CustomerDTO initialCustomerDTO, CustomerDTO editedCustomerDTO);
    }
}
