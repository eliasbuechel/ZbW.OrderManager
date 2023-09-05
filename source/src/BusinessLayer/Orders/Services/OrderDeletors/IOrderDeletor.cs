using DataLayer.Orders.DTOs;

namespace BusinessLayer.Orders.Services.OrderDeletors
{
    public interface IOrderDeletor
    {
        Task DeleteOrderAsync(OrderDTO order);
    }
}
