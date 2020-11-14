using SEDC.FoodApp.DomainModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.FoodApp.DomainModels.Models
{
    public class MenuItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Calories { get; set; }
        public bool IsVege { get; set; }
        public MealType MealType { get; set; }
    }
}
