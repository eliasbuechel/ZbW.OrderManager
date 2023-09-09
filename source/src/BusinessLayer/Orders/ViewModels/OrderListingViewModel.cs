using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Orders.Commands;
using DataLayer.Orders.DTOs;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLayer.Orders.ViewModels
{
    public class OrderListingViewModel : BaseLoadableViewModel
    {
        public static OrderListingViewModel LoadViewModel(ManagerStore managerStore, NavigationStore navigationStore)
        {
            OrderListingViewModel viewModel = new OrderListingViewModel(managerStore, navigationStore);
            viewModel.LoadOrdersCommand?.Execute(null);
            return viewModel;
        }
        private OrderListingViewModel(ManagerStore managerStore, NavigationStore navigationStore)
        {
            _managerStore = managerStore;
            _navigationStore = navigationStore;

            LoadOrdersCommand = new LoadOrdersCommand(managerStore, this);
            CreateOrderCommand = new NavigateCommand(new ToSubNavigationService<CreateOrderViewModel>(navigationStore, CreateCreateOrderViewModel));

            managerStore.OrderCreated += OnOrderCreated;
            managerStore.OrderDeleted += OnOrderDeleted;
        }

        public ICommand CreateOrderCommand { get; }
        public ICommand LoadOrdersCommand { get; }
        public IEnumerable<OrderViewModel> Orders => _orders;

        public void OnOrdersLoaded(IEnumerable<OrderDTO> orders)
        {
            _orders.Clear();
            foreach (var o in orders)
            {
                OrderViewModel viewModel = new OrderViewModel(_managerStore, o);
                _orders.Add(viewModel);
            }
        }
        public override void Dispose(bool disposing)
        {
            _managerStore.OrderCreated -= OnOrderCreated;
            _managerStore.OrderDeleted -= OnOrderDeleted;
        }

        private void OnOrderCreated(OrderDTO order)
        {
            OrderViewModel orderViewModel = new OrderViewModel(_managerStore, order);
            _orders.Add(orderViewModel);
        }
        private void OnOrderDeleted(OrderDTO order)
        {
            foreach (var o in _orders)
            {
                if (o.GetOrder().Id == order.Id)
                {
                    _orders.Remove(o);
                    break;
                }
            }
        }
        private CreateOrderViewModel CreateCreateOrderViewModel()
        {
            return CreateOrderViewModel.LoadViewModel(
                _managerStore,
                _navigationStore,
                new FromSubNavigationService<OrderListingViewModel>(_navigationStore, this)
                );
        }

        private readonly ManagerStore _managerStore;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<OrderViewModel> _orders = new ObservableCollection<OrderViewModel>();
    }
}
