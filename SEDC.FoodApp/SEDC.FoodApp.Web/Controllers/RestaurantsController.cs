using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.FoodApp.DomainModels.Enums;
using SEDC.FoodApp.RequestModels.Models;
using SEDC.FoodApp.Services.Services.Classes;
using SEDC.FoodApp.Services.Services.Interfaces;

namespace SEDC.FoodApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        //api/Restaurants/AddRestaurant
        [HttpPost("AddRestaurant")]
        public async Task<IActionResult> AddRestaurantAsync([FromBody] RestaurantRequestModel model) 
        {
            await _restaurantService.CreateNewRestaurantAsync(model);
            return Ok();
        }

        //api/Restaurants/GetRestaurants queries are optional ex: ?id=12345
        [HttpGet("GetRestaurants")]
        public async Task<IActionResult> GetRestaurantsAsync([FromQuery] string name,
                                                             [FromQuery] string address,
                                                             [FromQuery] Municipality? municipality) 
        {
            var requestModel = new RestaurantRequestModel()
            {
                Name = name,
                Address = address,
                Municipality = municipality
            };

            var response = await _restaurantService.GetRestaurantsAsync(requestModel);
            return Ok(response);
        }

        //api/Restaurants/UpdateRestaurant
        [HttpPut("UpdateRestaurant")]
        public async Task<IActionResult> UpdateRestaurantAsync([FromBody] UpdateRestaunratRequestModel requestModel)
        {
            await _restaurantService.UpdateRestaurantAsync(requestModel);
            return Ok();
        }

        //api/Restaurants/DeleteRestaurant
        [HttpDelete("DeleteRestaurant")]
        public async Task<IActionResult> DeleteRestaurantAsync([FromQuery] string id) 
        {
            await _restaurantService.DeleteRestaurantByIdAsync(id);
            return Ok();
        }

        [HttpPut("UpdateRestaurantMenu")]
        public async Task<IActionResult> UpdateRestaurantMenuAsync([FromBody] UpdateRestaunratRequestModel requestModel) 
        {
            await _restaurantService.UpdateRestaurantMenuAsync(requestModel);
            return Ok();
        }

        //api/Restaurants/GetRestaurantMenuItems queries are optional ex: ?name=testrestaurant
        [HttpGet("GetRestaurantMenuItems")]
        public async Task<IActionResult> GetRestaurantMenuItemsAsync([FromQuery] string restaurantId,
                                                                     [FromQuery] string name)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
            var menuItems = restaurant.Menu;

            if (!string.IsNullOrEmpty(name)) 
            {
                menuItems = restaurant.Menu.FindAll(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return Ok(menuItems);
        }

        //api/Restaurants/DeleteMenuItem
        [HttpDelete("DeleteMenuItem")]
        public async Task<IActionResult> DeleteMenuItemAsync([FromQuery] string restaurantId,
                                                             [FromQuery] string menuItemId ) 
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
            await _restaurantService.DeleteRestaurantMenuItemAsync(restaurant, menuItemId);
            return Ok();
        }

    }
}
