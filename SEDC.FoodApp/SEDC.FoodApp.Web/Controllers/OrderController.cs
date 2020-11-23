using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.FoodApp.RequestModels.Models;
using SEDC.FoodApp.Services.Services.Interfaces;

namespace SEDC.FoodApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestModel requestModel)
        {
            var order = await _orderService.GetOrderByUserId(requestModel.UserId);

            if (order == null)
            {
                await _orderService.CreateNewOrder(requestModel);
            }

            return Ok();
        }

        [HttpPost("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderRequestModel requestModel)
        {
            await _orderService.UpdateOrder(requestModel);
            return Ok();
        }

        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder([FromQuery] string UserId)
        {
            var order = await _orderService.GetOrderByUserId(UserId);
            return Ok(order);
        }
    }
}
