using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherAppApi.Models
{
    public class  APixuWeatherReportFinal
    {
         public APixuWeatherReport APixuWeatherReport {get;set;}
         public Tempindiffform Tempindiffform {get;set;}
    }
    
    public class APixuWeatherReport
    {
        
        public location location  { get; set; }

         public current current { get; set; }
    }
    public class location
    {
        public string name { get; set; }

        public string region { get; set; }
        
        public string country { get; set; }

        public string lat { get; set; }

        public string lon { get; set; }

        public string tz_id { get; set; }

        public string localtime_epoch { get; set; }

        public string localtime { get; set; }

    }

    public class current
    {
        public string last_updated_epoch { get; set; }

        public string last_updated { get; set; }
        
        public string temp_c { get; set; }

        public string temp_f { get; set; }

        public condition condition  { get; set; }
        public string wind_mph { get; set; }

        public string wind_kph { get; set; }

        public string wind_degree { get; set; }

        public string wind_dir { get; set; }

        public string pressure_mb { get; set; }

        public string pressure_in { get; set; }

        public string humidity { get; set; }

        public string cloud { get; set; }

        public string feelslike_c { get; set; }

        public string feelslike_f { get; set; }

        public string vis_km { get; set; }

        public string vis_miles { get; set; }

        public string uv { get; set; }

        public string gust_mph { get; set; }

        public string gust_kph { get; set; }

    }
    public class condition
    {
        public string text { get; set; }

        public string icon { get; set; }

        public string code { get; set; }
    }

}



