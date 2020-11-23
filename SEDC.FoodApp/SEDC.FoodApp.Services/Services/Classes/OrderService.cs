using SEDC.FoodApp.DataAccess.Mongo.Repositories.Interfaces;
using SEDC.FoodApp.DomainModels.Models;
using SEDC.FoodApp.RequestModels.Models;
using SEDC.FoodApp.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.Services.Services.Classes
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateNewOrder(OrderRequestModel model)
        {
            var order = new Order()
            {
                UserId = model.UserId,
                MenuItems = new List<MenuItem>()
            };

            await _orderRepository.InsertOrder(order);
        }

        public Task<Order> GetOrderByUserId(string UserId)
        {
            return _orderRepository.GetOrderByUserId(UserId);
        }

        public async Task UpdateOrder(OrderRequestModel model)
        {
            var order = await GetOrderByUserId(model.UserId);
            order.MenuItems.Add(model.MenuItem);

            await _orderRepository.UpdateOrder(order);
        }
    }
}
