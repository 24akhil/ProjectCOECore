using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherAppApi.Models
{
    
    public class  OpenWeatherFinal
    {
        public OpenWeather OpenWeather {get;set;}
        public  Tempindiffform Tempindiffform {get;set;}
    }
    public class OpenWeather
    {
        public coord coord {get; set;}
        public   List<weather> weather {get;set;}

    //public string base{ get; set; }
    public main main {get;set;}
    public string visibility { get; set; }
    public wind wind {get;set;}
     public clouds clouds {get;set;}
    public string dt { get; set; }
     public sys sys {get;set;}
    public string timezone { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string cod { get; set; }


    }

    public class coord
    {
        public string lon { get; set; }
        public string lat { get; set; }
       
    }
    public class weather
    {
         public string id { get; set; }
         public string main { get; set; }
         public string description { get; set; }
         public string icon { get; set; }

    }
    public class main
    {
        public string temp { get; set; }
        public string pressure { get; set; }
        public string humidity { get; set; }
        public string temp_min { get; set; }
        public string temp_max { get; set; }

        
    }
    public class wind
    {
         public string speed { get; set; }
          public string deg { get; set; }
        
    }
    public class clouds
    {
         public string all { get; set; }
          
    }
    public class sys
    {
        public string type { get; set; }
         public string id { get; set; }
          public string message { get; set; }
           public string country { get; set; }
            public string sunrise { get; set; }
             public string sunset { get; set; }
    }

    

    
}


