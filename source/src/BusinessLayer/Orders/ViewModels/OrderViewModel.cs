using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Orders.Commands;
using DataLayer.Customers.DTOs;
using DataLayer.Orders.DTOs;
using System.Windows.Input;

namespace BusinessLayer.Orders.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public OrderViewModel(ManagerStore managerStore, OrderDTO order)
        {
            _order = order;
            DeleteOrderCommand = new DeleteOrderCommand(managerStore, order);
        }

        public ICommand EditOrderCommand { get; }
        public ICommand DeleteOrderCommand { get; }
        public int Id => _order.Id;
        public string TimeStamp => _order.TimeStamp.ToString("dd-MM-yyyy");
        public CustomerDTO Customer => _order.Customer;

        public OrderDTO GetOrder()
        {
            return _order;
        }


        private readonly OrderDTO _order;
    }
}
