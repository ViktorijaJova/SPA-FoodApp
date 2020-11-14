using SEDC.FoodApp.DomainModels.Models;
using SEDC.FoodApp.RequestModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.Services.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task CreateNewRestaurantAsync(RestaurantRequestModel model);
        Task<List<RestaurantRequestModel>> GetRestaurantsAsync(RestaurantRequestModel requestModel);
        Task DeleteRestaurantByIdAsync(string id);
        Task UpdateRestaurantAsync(UpdateRestaunratRequestModel requestModel);
        Task UpdateRestaurantMenuAsync(UpdateRestaunratRequestModel requestModel);
        Task<Restaurant> GetRestaurantByIdAsync(string id);
        Task DeleteRestaurantMenuItemAsync(Restaurant restaurant, string menuItemId);
    }
}
