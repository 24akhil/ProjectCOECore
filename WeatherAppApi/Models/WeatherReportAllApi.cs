using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;


namespace WeatherAppApi.Models
{

    public class WeatherReportAllApiCurrent
    {
        public ApixuCurrentReport ApixuCurrentReport{ get;set;}
        public SunRIseSunSetCurrentReport SunRIseSunSetCurrentReport{get;set;}
        public DarkSkyCurrentReport DarkSkyCurrentReport{get;set;}
    }
    public class ApixuCurrentReport
    {
        public WeatherData WeatherData {get;set;}
    }
    public class SunRIseSunSetCurrentReport
    {
        public WeatherData WeatherData {get;set;}
    }

    public class DarkSkyCurrentReport
    {
        public WeatherData WeatherData {get;set;}
    }

    public class WeatherData
    {
         public string latitude{get;set;}
         public string longitude{get;set;}
         public string temp_C{get;set;}
         public string temp_F{get;set;}
         public string Presure{get;set;}
         public string Humidity{get;set;}
         public string WindSpeed{get;set;}
         public string Precipited{get;set;}
         public string Cloud{get;set;}
    }

}