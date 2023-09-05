using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;
using DataLayer.Orders.DTOs;

namespace BusinessLayer.Orders.Commands
{
    public class DeleteOrderCommand : BaseAsyncCommand
    {
        public DeleteOrderCommand(ManagerStore managerStore, OrderDTO order)
        {
            _managerStore = managerStore;
            _order = order;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _managerStore.DeleteOrderAsync(_order);
            }
            catch (Exception e)
            {
                // error message
            }
        }

        private readonly ManagerStore _managerStore;
        private readonly OrderDTO _order;
    }
}
