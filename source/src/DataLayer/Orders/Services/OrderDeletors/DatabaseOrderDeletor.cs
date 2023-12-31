﻿using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.Exceptions;
using DataLayer.Orders.DTOs;
using DataLayer.Orders.Models;

namespace DataLayer.Orders.Services.OrderDeletors
{
    public class DatabaseOrderDeletor : IOrderDeletor
    {
        public DatabaseOrderDeletor(ManagerDbContextFactory managerDbContextFactory)
        {
            _managerDbContextFactory = managerDbContextFactory;
        }

        public async Task DeleteOrderAsync(OrderDTO orderDTO)
        {
            using ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            Order order = context.Orders
                .Where(o => o.Id == orderDTO.Id)
                .FirstOrDefault()
                ?? throw new NotInDatabaseException("");

            context.Remove(order);
            await context.SaveChangesAsync();
        }


        private readonly ManagerDbContextFactory _managerDbContextFactory;
    }
}
