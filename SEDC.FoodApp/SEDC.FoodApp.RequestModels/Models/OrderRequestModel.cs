using SEDC.FoodApp.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.FoodApp.RequestModels.Models
{
    public class OrderRequestModel
    {
        public string UserId { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
