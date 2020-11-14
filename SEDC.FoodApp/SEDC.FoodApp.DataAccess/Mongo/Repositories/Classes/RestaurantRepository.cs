using MongoDB.Driver;
using SEDC.FoodApp.DataAccess.Mongo.Repositories.Interfaces;
using SEDC.FoodApp.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.DataAccess.Mongo.Repositories.Classes
{
    public class RestaurantRepository : MongoDbRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(string connectionString, string databaseName) 
            : base(connectionString, databaseName) {}

        protected override string GetCollectionName()
        {
            return typeof(Restaurant).Name + "s";
        }

        public async Task InsertRestaurantAsync(Restaurant restaurant)
        {
            await InsertAsync(restaurant);
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(string id)
        {
            return await MongoCollection.Find(Builders<Restaurant>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        public async Task<List<Restaurant>> GetRestaurantsAsync(Expression<Func<Restaurant, bool>> filter)
        {
            return await MongoCollection.Find(filter).ToListAsync();
        }

        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            FilterDefinition<Restaurant> obj = MapFilter(restaurant.Id);

            var updateDefinition = Builders<Restaurant>.Update
                                    .Set(r => r.Id, restaurant.Id)
                                    .Set(r => r.Name, restaurant.Name)
                                    .Set(r => r.Address, restaurant.Address)
                                    .Set(r => r.Municipality, restaurant.Municipality)
                                    .Set(r => r.Menu, restaurant.Menu);

            await UpdateOneAsync(obj, updateDefinition);
        }

        public async Task DeleteRestaurantByIdAsync(string id)
        {
            FilterDefinition<Restaurant> obj = MapFilter(id);
            await DeleteManyAsync(obj);
        }

        //map
        private FilterDefinition<Restaurant> MapFilter(string id) 
        {
            FilterDefinition<Restaurant> filter = Builders<Restaurant>.Filter.Empty;
            return filter & Builders<Restaurant>.Filter.Where(item => item.Id == id);
        }
    }
}
