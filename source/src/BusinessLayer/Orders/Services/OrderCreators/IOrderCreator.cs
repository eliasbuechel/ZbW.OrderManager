using DataLayer.Orders.DTOs;

namespace BusinessLayer.Orders.Services.OrderCreators
{
    public interface IOrderCreator
    {
        Task<OrderDTO> CreateOrderAsync(CreatingOrderDTO order);
    }
}
