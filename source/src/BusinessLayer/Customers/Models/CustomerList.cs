using DataLayer.Customers.DTOs;
using DataLayer.Customers.Services.CustomerCreators;
using DataLayer.Customers.Services.CustomerDeletors;
using DataLayer.Customers.Services.CustomerEditors;
using DataLayer.Customers.Services.CustomerProviders;
using DataLayer.Customers.Validation;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Customers.Models
{
    public class CustomerList
    {
        public CustomerList(ICustomerProvider customerProvider, ICustomerCreator customerCreator, ICustomerDeletor customerDeletor, ICustomerEditor customerEditor, ICustomerValidator customerValidator)
        {
            _customerProvider = customerProvider;
            _customerCreator = customerCreator;
            _customerDeletor = customerDeletor;
            _customerEditor = customerEditor;
            _customerValidator = customerValidator;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            return await _customerProvider.GetAllCustomers();
        }
        public async Task CreateCustomer(CustomerDTO customer)
        {
            ValidateCustomer(customer);
            await _customerCreator.CreateCustomer(customer);
        }
        public async Task<int> GetNextFreeCustomerIdAsync()
        {
            return await _customerCreator.GetNextFreeCustomerIdAsync();
        }
        public async Task DeleteCustomer(CustomerDTO customer)
        {
            await _customerDeletor.DeleteCustomer(customer);
        }
        public async Task EditCustomer(CustomerDTO initialCustomer, CustomerDTO editedCustomer)
        {
            ValidateCustomer(initialCustomer);
            await _customerEditor.EditCustomer(initialCustomer, editedCustomer);
        }

        private void ValidateCustomer(CustomerDTO customer)
        {
            if (!_customerValidator.Validate(customer))
                throw new ValidationException();
        }

        private readonly ICustomerProvider _customerProvider;
        private readonly ICustomerCreator _customerCreator;
        private readonly ICustomerDeletor _customerDeletor;
        private readonly ICustomerEditor _customerEditor;
        private readonly ICustomerValidator _customerValidator;
    }
}
