using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherAppApi.Models
{
    public  class Tempindiffform
    {
        public  float tempc {get;set;}
        public float tempf {get;set;}
        public float tempk {get;set;}
    }
}