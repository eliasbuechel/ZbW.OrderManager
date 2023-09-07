using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Orders.ViewModels;
using DataLayer.Orders.DTOs;
using System.ComponentModel;

namespace BusinessLayer.Orders.Commands
{
    public class CreateOrderCommand : BaseAsyncCommand
    {
        public CreateOrderCommand(ManagerStore managerStore, CreateOrderViewModel createOrderViewModel, IEnumerable<CreatingPositionDTO> positions, SubNavigationService<OrderListingViewModel, CreateOrderViewModel> orderListingViewModelSubNavigationService)
        {
            _managerStore = managerStore;
            _createOrderViewModel = createOrderViewModel;
            _positions = positions;
            _orderListingViewModelSubNavigationService = orderListingViewModelSubNavigationService;

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

            _orderListingViewModelSubNavigationService.Navigate();
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
        private readonly SubNavigationService<OrderListingViewModel, CreateOrderViewModel> _orderListingViewModelSubNavigationService;
    }
}
