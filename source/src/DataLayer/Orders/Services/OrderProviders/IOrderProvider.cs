using DataLayer.Orders.DTOs;

namespace DataLayer.Orders.Services.OrderProviders
{
    public interface IOrderProvider
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO> GetOrdersAsync(int id);
    }
}
