using DataLayer.Customers.Services.CustomerCreators;
using DataLayer.Customers.Services.CustomerDeletors;
using DataLayer.Customers.Services.CustomerEditors;
using DataLayer.Customers.Services.CustomerProviders;

namespace DataLayer.Customers.Models
{
    public class CustomerList
    {
        public CustomerList(ICustomerProvider customerProvider, ICustomerCreator customerCreator, ICustomerDeletor customerDeletor, ICustomerEditor customerEditor)
        {
            _customerProvider = customerProvider;
            _customerCreator = customerCreator;
            _customerDeletor = customerDeletor;
            _customerEditor = customerEditor;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerProvider.GetAllCustomers();
        }

        public async Task CreateCustomer(Customer customer)
        {
            await _customerCreator.CreateCustomer(customer);
        }

        public async Task<int> GetNextFreeCustomerIdAsync()
        {
            return await _customerCreator.GetNextFreeCustomerIdAsync();
        }

        public async Task DeleteCustomer(Customer customer)
        {
            await _customerDeletor.DeleteCustomer(customer);
        }

        public async Task EditCustomer(Customer initialCustomer, Customer editedCustomer)
        {
            await _customerEditor.EditCustomer(initialCustomer, editedCustomer);
        }

        private readonly ICustomerProvider _customerProvider;
        private readonly ICustomerCreator _customerCreator;
        private readonly ICustomerDeletor _customerDeletor;
        private readonly ICustomerEditor _customerEditor;
    }
}
