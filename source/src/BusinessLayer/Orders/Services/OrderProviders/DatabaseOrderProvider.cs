using DataLayer.Articles.Services.ArticleProviders;
using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Exceptions;
using DataLayer.Customers.Services.CustomerProviders;
using DataLayer.Orders.DTOs;
using DataLayer.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Orders.Services.OrderProviders
{
    public class DatabaseOrderProvider : IOrderProvider
    {
        public DatabaseOrderProvider(ManagerDbContextFactory managerDbContextFactory, ICustomerProvider customerProvider, IArticleProvider articleProvider)
        {
            _managerDbContextFactory = managerDbContextFactory;
            _customerProvider = customerProvider;
            _articleProvider = articleProvider;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            using ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            IEnumerable<Order> orders = await context.Orders
                .Include(o => o.Customer)
                .ToListAsync();

            ICollection<OrderDTO> orderDTOs = new List<OrderDTO>();
            foreach (Order order in orders)
            {
                OrderDTO orderDTO = await ConvertToOrdeerDTO(context, order);
                orderDTOs.Add(orderDTO);
            }

            return orderDTOs;
        }
        public async Task<OrderDTO> GetOrdersAsync(int id)
        {
            using ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            Order? order = await context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync()
                ?? throw new NotInDatabaseException($"Not found order width id '{id}' in database");

            return await ConvertToOrdeerDTO(context, order);
        }

        private async Task<OrderDTO> ConvertToOrdeerDTO(ManagerDbContext context, Order order)
        {
            CustomerDTO customerDTO = await _customerProvider.GetCustomerAsync(order.Customer.Id);
            IEnumerable<PositionDTO> positionDTOs = await GetPositions(context, order.Id);

            OrderDTO orderDTO = new OrderDTO(order.Id, order.TimeStamp, customerDTO, positionDTOs);
            return orderDTO;
        }
        private async Task<IEnumerable<PositionDTO>> GetPositions(ManagerDbContext context, int orderId)
        {
            return await context.Positions
                                .Include(p => p.Order)
                                .Include(p => p.Article)
                                .Where(p => p.Order.Id == orderId)
                                .Select(p => new PositionDTO(
                                    p.Id,
                                    p.Number,
                                    _articleProvider.GetArticle(p.Article.Id),
                                    p.Ammount
                                    )
                                )
                                .ToListAsync();
        }

        private readonly ManagerDbContextFactory _managerDbContextFactory;
        private readonly ICustomerProvider _customerProvider;
        private readonly IArticleProvider _articleProvider;
    }
}
