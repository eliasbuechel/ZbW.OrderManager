using DataLayer.Base.Models;
using DataLayer.Customers.Models;

namespace BusinessLayer.Base.Stores
{
    public class ManagerStore
    {
        public ManagerStore(Manager manager)
        {
            _customers = new List<Customer>();
            _manager = manager;
            _inizializeLazy = new Lazy<Task>(Inizialize);
        }

        public IEnumerable<Customer> Customers => _customers;
        public event Action<Customer> CustomerCreated;
        public event Action<Customer> CustomerDeleted;

        public async Task Load()
        {
            try
            {
                await _inizializeLazy.Value;
            }
            catch (Exception)
            {
                _inizializeLazy = new Lazy<Task>(Inizialize);
                throw;
            }
        }

        private async Task Inizialize()
        {
            IEnumerable<Customer> customers = await _manager.GetAllCustomers();

            _customers.Clear();
            _customers.AddRange(customers);
        }

        public async Task CreateCustomer(Customer customer)
        {
            await _manager.CreateCustomer(customer);
            _customers.Add(customer);
            OnCustomerCreated(customer);
        }

        public async Task<int> GetNextFreeCustomerIdAsync()
        {
            return await _manager.GetNextFreeCustomerIdAsync();
        }

        public async Task DeleteCustomer(Customer customer)
        {
            await _manager.DeleteCustomer(customer);
            _customers.Remove(customer);
            OnCustomerDeleted(customer);
        }

        public async Task EditCustomer(Customer initialCustomer, Customer editedCustomer)
        {
            await _manager.EditCustomer(initialCustomer, editedCustomer);
            int initialCustomerIndex = _customers.IndexOf(initialCustomer);
            _customers[initialCustomerIndex] = editedCustomer;
        }

        private void OnCustomerCreated(Customer customer)
        {
            CustomerCreated?.Invoke(customer);
        }

        private void OnCustomerDeleted(Customer customer)
        {
            CustomerDeleted?.Invoke(customer);
        }

        private readonly List<Customer> _customers;
        private readonly Manager _manager;
        private Lazy<Task> _inizializeLazy;
    }
}
