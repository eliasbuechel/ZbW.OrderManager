using DataLayer.Customers.Models;

namespace DataLayer.Base.Models
{
    public class Manager
    {
        public Manager(CustomerList customerList)
        {
            _customerList = customerList;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerList.GetAllCustomers();
        }

        public async Task CreateCustomer(Customer customer)
        {
            await _customerList.CreateCustomer(customer);
        }

        public async Task<int> GetNextFreeCustomerIdAsync()
        {
            return await _customerList.GetNextFreeCustomerIdAsync();
        }

        public async Task DeleteCustomer(Customer customer)
        {
            await _customerList.DeleteCustomer(customer);
        }

        public async Task EditCustomer(Customer initialCustomer, Customer editedCustomer)
        {
            await _customerList.EditCustomer(initialCustomer, editedCustomer);
        }

        private readonly CustomerList _customerList;
    }
}
