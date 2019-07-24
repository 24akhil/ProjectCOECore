using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherAppApi.Models
{
    
    public class  AverageReport
    {
       public int Temp_C {get;set;}
       public int Temp_F {get;set;}

       public int Humidity {get;set;}

       public int Pressure {get;set;}
       public int WindSpeed {get;set;}
        public int AppearentTemp {get;set;}


    }
    

}

