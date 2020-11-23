using SEDC.FoodApp.DomainModels.Models;
using SEDC.FoodApp.RequestModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.Services.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateNewOrder(OrderRequestModel model);
        Task UpdateOrder(OrderRequestModel model);
        Task<Order> GetOrderByUserId(string UserId);
    }
}
