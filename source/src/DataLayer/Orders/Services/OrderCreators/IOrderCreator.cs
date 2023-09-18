using DataLayer.Orders.DTOs;

namespace DataLayer.Orders.Services.OrderCreators
{
    public interface IOrderCreator
    {
        Task<OrderDTO> CreateOrderAsync(CreatingOrderDTO creatingOrderDTO);
    }
}
