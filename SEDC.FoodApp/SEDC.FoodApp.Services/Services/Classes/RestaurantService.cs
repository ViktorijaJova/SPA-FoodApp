using SEDC.FoodApp.DataAccess.Mongo.Repositories.Interfaces;
using SEDC.FoodApp.DomainModels.Enums;
using SEDC.FoodApp.DomainModels.Models;
using SEDC.FoodApp.RequestModels.Models;
using SEDC.FoodApp.Services.Helpers;
using SEDC.FoodApp.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.FoodApp.Services.Services.Classes
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task CreateNewRestaurantAsync(RestaurantRequestModel model)
        {
            var dtoRestaurant = new Restaurant()
            {
                Name = model.Name,
                Address = model.Address,
                Municipality = (Municipality)model.Municipality,
                Menu = new List<MenuItem>()
            };

            await _restaurantRepository.InsertRestaurantAsync(dtoRestaurant);
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(string id) 
        {
            return await _restaurantRepository.GetRestaurantByIdAsync(id);
        }

        public async Task<List<RestaurantRequestModel>> GetRestaurantsAsync(RestaurantRequestModel requestModel)
        {
            Expression<Func<Restaurant, bool>> filter = f => true;

            if (!string.IsNullOrEmpty(requestModel.Name)) 
            {
                filter = filter.AndAlso(x => x.Name.ToLower().Contains(requestModel.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(requestModel.Address))
            {
                filter = filter.AndAlso(x => x.Address.ToLower().Contains(requestModel.Address.ToLower()));
            }

            if (requestModel.Municipality.HasValue)
            {
                filter = filter.AndAlso(x => x.Municipality == requestModel.Municipality);
            }

            var restaurantList = await _restaurantRepository.GetRestaurantsAsync(filter);

            var mapToRestaurantRequestModel = new List<RestaurantRequestModel>();

            foreach (var restaurant in restaurantList)
            {
                var tempModel = new RestaurantRequestModel()
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Address = restaurant.Address,
                    Municipality = restaurant.Municipality,
                    Menu = restaurant.Menu
                };

                mapToRestaurantRequestModel.Add(tempModel);
            }

            return mapToRestaurantRequestModel;
        }

        public async Task DeleteRestaurantByIdAsync(string id) 
        {
            await _restaurantRepository.DeleteRestaurantByIdAsync(id);
        }

        public async Task UpdateRestaurantAsync(UpdateRestaunratRequestModel requestModel) 
        {
            var restaurant = await GetRestaurantByIdAsync(requestModel.Id);
            restaurant.Name = requestModel.Name;
            restaurant.Address = requestModel.Address;
            restaurant.Municipality = requestModel.Municipality;

            await _restaurantRepository.UpdateRestaurantAsync(restaurant);
        }

        public async Task UpdateRestaurantMenuAsync(UpdateRestaunratRequestModel requestModel) 
        {
            var restaurant = await GetRestaurantByIdAsync(requestModel.Id);
            var menuItem = requestModel.MenuItem;

            if (string.IsNullOrEmpty(menuItem.Id))
            {
                var dtoMenuItem = new MenuItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = menuItem.Name,
                    Calories = menuItem.Calories,
                    IsVege = menuItem.IsVege,
                    Price = menuItem.Price,
                    MealType = menuItem.MealType
                };

                restaurant.Menu.Add(dtoMenuItem);
            }
            else 
            {
                for (int i = 0; i < restaurant.Menu.Count; i++)
                {
                    if (restaurant.Menu[i].Id == menuItem.Id) 
                    {
                        restaurant.Menu[i].Id = menuItem.Id;
                        restaurant.Menu[i].Name = menuItem.Name;
                        restaurant.Menu[i].Calories = menuItem.Calories;
                        restaurant.Menu[i].Price = menuItem.Price;
                        restaurant.Menu[i].IsVege = menuItem.IsVege;
                        restaurant.Menu[i].MealType = menuItem.MealType;
                        break;
                    }
                }
            }

            await _restaurantRepository.UpdateRestaurantAsync(restaurant);
        }

        public async Task DeleteRestaurantMenuItemAsync(Restaurant restaurant, string menuItemId) 
        {
            var menuItem = restaurant.Menu.FirstOrDefault(x => x.Id == menuItemId);
            restaurant.Menu.Remove(menuItem);
            await _restaurantRepository.UpdateRestaurantAsync(restaurant);
        }
    }
}
