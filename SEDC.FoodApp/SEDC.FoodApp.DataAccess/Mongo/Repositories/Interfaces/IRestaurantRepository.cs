using SEDC.FoodApp.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SEDC.FoodApp.DataAccess.Mongo.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        Task InsertRestaurantAsync(Restaurant restaurant);
        Task<Restaurant> GetRestaurantByIdAsync(string id);
        Task<List<Restaurant>> GetRestaurantsAsync(Expression<Func<Restaurant, bool>> filter);
        Task UpdateRestaurantAsync(Restaurant restaurant);
        Task DeleteRestaurantByIdAsync(string id);
    }
}
