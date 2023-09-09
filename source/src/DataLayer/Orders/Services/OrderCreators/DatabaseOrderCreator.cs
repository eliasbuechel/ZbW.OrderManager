using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.Exceptions;
using DataLayer.Customers.Models;
using DataLayer.Orders.DTOs;
using DataLayer.Orders.Models;
using DataLayer.Orders.Services.OrderProviders;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Orders.Services.OrderCreators
{
    public class DatabaseOrderCreator : IOrderCreator
    {
        public DatabaseOrderCreator(ManagerDbContextFactory managerDbContextFactory, IOrderProvider orderProvider)
        {
            _managerDbContextFactory = managerDbContextFactory;
            _orderProvider = orderProvider;
        }

        public async Task<OrderDTO> CreateOrderAsync(CreatingOrderDTO creatingOrderDTO)
        {
            using ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            Customer? customer = await context.Customers
                .Where(c => c.Id == creatingOrderDTO.Customer.Id)
                .FirstOrDefaultAsync()
                ?? throw new NotInDatabaseException($"Not found customer width id '{creatingOrderDTO.Customer.Id}' in database!");

            ICollection<Position> positions = creatingOrderDTO.Positions
                .Select(p => new Position()
                {
                    Number = p.Number,
                    Ammount = p.Ammount,
                    Article = context.Articles
                            .Where(a => a.Id == p.Article.Id)
                            .FirstOrDefault()
                            ?? throw new NotInDatabaseException($"Nof fount article with id '{p.Article.Id}' in database!")
                }
                )
                .ToList();

            Order order = new Order()
            {
                //Id = creatingOrder.Id,
                TimeStamp = creatingOrderDTO.TimeStamp,
                Customer = customer,
                Positions = positions
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();
            return await _orderProvider.GetOrdersAsync(order.Id);
        }

        private readonly ManagerDbContextFactory _managerDbContextFactory;
        private readonly IOrderProvider _orderProvider;
    }
}
