using DataLayer.Customers.DTOs;

namespace DataLayer.Customers.Services.CustomerProviders
{
    public interface ICustomerProvider
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<IEnumerable<SerializableCustomerDTO>> GetAllSerializableCustomersAsync();
        Task<CustomerDTO> GetCustomerAsync(int id);
    }
}
