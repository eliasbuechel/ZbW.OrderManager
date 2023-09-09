using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;
using DataLayer.Customers.DTOs;

namespace BusinessLayer.Customers.Commands
{
    public class DeleteCustomeCommand : BaseAsyncCommand
    {
        public DeleteCustomeCommand(ManagerStore managerStore, CustomerDTO customer)
        {
            _managerStore = managerStore;
            _customer = customer;
        }

        public async override Task ExecuteAsync(object? parameter)
        {
            await _managerStore.DeleteCustomerAsync(_customer);
        }

        private readonly ManagerStore _managerStore;
        private readonly CustomerDTO _customer;
    }
}
