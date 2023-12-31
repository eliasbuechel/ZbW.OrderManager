﻿using DataLayer.Orders.DTOs;
using DataLayer.Orders.Services.OrderCreators;
using DataLayer.Orders.Services.OrderDeletors;
using DataLayer.Orders.Services.OrderProviders;
using System.Collections.Generic;

namespace BusinessLayer.Orders.Models
{
    public class OrderList
    {
        public OrderList(IOrderProvider orderProvider, IOrderCreator orderCreator, IOrderDeletor orderDeletor)
        {
            _orderProvider = orderProvider;
            _orderCreator = orderCreator;
            _orderDeletor = orderDeletor;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await _orderProvider.GetAllOrdersAsync();
        }
        public async Task<OrderDTO> CreateOrderAsync(CreatingOrderDTO order)
        {
            return await _orderCreator.CreateOrderAsync(order);
        }
        public async Task DeleteOrderAsync(OrderDTO order)
        {
            await _orderDeletor.DeleteOrderAsync(order);
        }

        private readonly IOrderProvider _orderProvider;
        private readonly IOrderCreator _orderCreator;
        private readonly IOrderDeletor _orderDeletor;
    }
}
