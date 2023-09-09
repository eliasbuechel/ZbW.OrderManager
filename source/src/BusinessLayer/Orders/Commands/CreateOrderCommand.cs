using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Orders.ViewModels;
using DataLayer.Orders.DTOs;
using System.ComponentModel;

namespace BusinessLayer.Orders.Commands
{
    public class CreateOrderCommand : BaseAsyncCommand, IDisposable
    {
        public CreateOrderCommand(ManagerStore managerStore, CreateOrderViewModel createOrderViewModel, IEnumerable<CreatingPositionDTO> positions, FromSubNavigationService<OrderListingViewModel> orderListingViewModelNavigateBackService)
        {
            _managerStore = managerStore;
            _createOrderViewModel = createOrderViewModel;
            _positions = positions;
            _orderListingViewModelNavigateBackService = orderListingViewModelNavigateBackService;

            createOrderViewModel.ErrorsChanged += OnCreateOrderViewModelErrorsChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && !_createOrderViewModel.HasErrors;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            if (_createOrderViewModel.Customer == null)
                return;

            CreatingOrderDTO order = new CreatingOrderDTO(
                _createOrderViewModel.Customer,
                DateTime.Now,
                _positions
                );

            await _managerStore.CreateOrderAsync(order);

            _orderListingViewModelNavigateBackService.Navigate();
        }
        public void Dispose()
        {
            _createOrderViewModel.ErrorsChanged += OnCreateOrderViewModelErrorsChanged;
        }

        private void OnCreateOrderViewModelErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        private readonly ManagerStore _managerStore;
        private readonly CreateOrderViewModel _createOrderViewModel;
        private readonly IEnumerable<CreatingPositionDTO> _positions;
        private readonly FromSubNavigationService<OrderListingViewModel> _orderListingViewModelNavigateBackService;
    }
}
