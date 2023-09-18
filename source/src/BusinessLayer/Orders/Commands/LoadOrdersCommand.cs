using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Stores;
using BusinessLayer.Orders.ViewModels;

namespace BusinessLayer.Orders.Commands
{
    public class LoadOrdersCommand : BaseAsyncCommand
    {
        public LoadOrdersCommand(ManagerStore managerStore, OrderListingViewModel orderListingViewModel)
        {
            _managerStore = managerStore;
            _orderListingViewModel = orderListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _orderListingViewModel.IsLoading = true;
            try
            {
                await _managerStore.LoadOrders();
                _orderListingViewModel.OnOrdersLoaded(_managerStore.Orders);
            }
            catch (Exception e)
            {
                _orderListingViewModel.ErrorMessage = e.Message;
            }
            finally
            {
                _orderListingViewModel.IsLoading = false;
            }
        }

        private readonly ManagerStore _managerStore;
        private readonly OrderListingViewModel _orderListingViewModel;
    }
}
