using MongoDB.Driver;
using SEDC.FoodApp.DataAccess.Mongo.Repositories.Interfaces;
using SEDC.FoodApp.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.DataAccess.Mongo.Repositories.Classes
{
    public class OrderRepository : MongoDbRepository<Order>, IOrderRepository
    {
        public OrderRepository(string connectionString, string databaseName)
            : base(connectionString, databaseName) { }

        protected override string GetCollectionName()
        {
            return typeof(Order).Name + "s";
        }

        public async Task InsertOrder(Order order)
        {
            await InsertAsync(order);
        }

        public async Task<Order> GetOrderByUserId(string id)
        {
            return await MongoCollection.Find(Builders<Order>.Filter.Eq("UserId", id)).FirstOrDefaultAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            FilterDefinition<Order> obj = MapFilter(order.Id);

            var updateDefinition = Builders<Order>.Update
                                    .Set(r => r.Id, order.Id)
                                    .Set(r => r.UserId, order.UserId)
                                    .Set(r => r.MenuItems, order.MenuItems);

            await UpdateOneAsync(obj, updateDefinition);
        }

        //map
        private FilterDefinition<Order> MapFilter(string id)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Empty;

            filter = filter & Builders<Order>.Filter.Where(item => item.Id == id);

            return filter;
        }
    }
}
