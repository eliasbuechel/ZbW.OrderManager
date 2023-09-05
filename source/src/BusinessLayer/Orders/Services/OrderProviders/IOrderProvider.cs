using DataLayer.Orders.DTOs;

namespace BusinessLayer.Orders.Services.OrderProviders
{
    public interface IOrderProvider
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO> GetOrdersAsync(int id);
    }
}
