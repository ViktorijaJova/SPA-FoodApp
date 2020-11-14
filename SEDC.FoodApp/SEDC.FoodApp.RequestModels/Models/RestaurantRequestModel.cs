using SEDC.FoodApp.DomainModels.Enums;
using SEDC.FoodApp.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.FoodApp.RequestModels.Models
{
    public class RestaurantRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Municipality? Municipality { get; set; }
        public List<MenuItem> Menu { get; set; }
    }
}
