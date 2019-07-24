using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherAppApi.Models
{
    
    public class ApiRating
    {
        public string ApiName { get; set; }
        public string Location { get; set; }
    }

}