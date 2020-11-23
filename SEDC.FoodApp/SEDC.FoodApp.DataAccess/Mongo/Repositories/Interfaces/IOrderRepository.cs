using SEDC.FoodApp.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.DataAccess.Mongo.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task InsertOrder(Order order);
        Task UpdateOrder(Order order);
        Task<Order> GetOrderByUserId(string id);
    }
}
