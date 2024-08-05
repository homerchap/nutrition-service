using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService
{
    public class Users
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }
    }
}
