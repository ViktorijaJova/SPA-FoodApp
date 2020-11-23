using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections.Generic;

namespace SEDC.FoodApp.DomainModels.Models
{
    public class Order
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<MenuItem> MenuItems { get; set; }
    }
}
