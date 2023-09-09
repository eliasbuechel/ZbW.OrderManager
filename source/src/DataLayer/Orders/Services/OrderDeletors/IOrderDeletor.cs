using DataLayer.Orders.DTOs;

namespace DataLayer.Orders.Services.OrderDeletors
{
    public interface IOrderDeletor
    {
        Task DeleteOrderAsync(OrderDTO orderDTO);
    }
}
