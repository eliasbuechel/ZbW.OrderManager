using BusinessLayer.Base.Command;
using BusinessLayer.Base.Stores;
using DataLayer.Customers.Models;

namespace BusinessLayer.Customers.Commands
{
    class DeleteCustomeCommand : BaseAsyncCommand
    {
        public DeleteCustomeCommand(ManagerStore managerStore, Customer customer)
        {
            _managerStore = managerStore;
            _customer = customer;
        }

        public async override Task ExecuteAsync(object? parameter)
        {
            await _managerStore.DeleteCustomer(_customer);
        }

        private readonly ManagerStore _managerStore;
        private readonly Customer _customer;
    }
}
