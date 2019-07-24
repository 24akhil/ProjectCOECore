using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace WeatherAppApi.Models
{
    public class WeatherApiRating
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public string key {get; set;}
        public string ApiName { get; set; }
        public string Location { get; set; }
        public int Rating { get; set; }
    }
}