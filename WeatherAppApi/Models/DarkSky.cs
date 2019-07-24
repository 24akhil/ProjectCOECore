using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherAppApi.Models
{
    public class DarkSkyFinalData
    {
        public DarkSky DarkSky {get;set;}

        public Tempindiffform Tempindiffform {get;set;}

    }
    public class DarkSky
    {
        
        public string latitude   { get; set; }

        public string longitude  { get; set; }

        public string timezone  { get; set; }

        public currently currently  { get; set; }

        public minutely minutely  { get; set; }

        public hourly hourly  { get; set; }

        public daily daily  { get; set; }

         
    }
    public class currently
    {
        public string time { get; set; }

        public string summary { get; set; }
        
        public string icon { get; set; }

        public string nearestStormDistance { get; set; }

        public string nearestStormBearing { get; set; }

        public string precipIntensity { get; set; }

        public string precipProbability { get; set; }

        public string temperature { get; set; }

        public string apparentTemperature { get; set; }

        public string humidity { get; set; }

         public string pressure { get; set; }

        public string windSpeed { get; set; }

        public string windGust { get; set; }

        public string windBearing { get; set; }

        public string cloudCover { get; set; }

        public string uvIndex { get; set; }

        public string visibility { get; set; }

        public string ozone { get; set; }

    }

    public class minutely
    {
        public string summary { get; set; }

        public string icon { get; set; }

        public List<minutedata> data  { get; set; }

    }

    public class minutedata
    {
       public string time { get; set; }

        public string precipIntensity { get; set; }

        public string precipProbability { get; set; }

    }

    public class hourly
    {
        public string summary { get; set; }

        public string icon { get; set; }

         public List<hourlydata> data  { get; set; }
    }
    public class hourlydata
    {
        public string time { get; set; }

        public string summary { get; set; }
        public string icon { get; set; }
        public string precipIntensity { get; set; }
        public string temperature { get; set; }
        public string apparentTemperature { get; set; }
        public string humidity { get; set; }
        public string pressure { get; set; }
        public string windSpeed { get; set; }
        public string windGust { get; set; }
        public string windBearing { get; set; }
        public string cloudCover { get; set; }
        public string uvIndex { get; set; }
        public string visibility { get; set; }

        public string ozone { get; set; }

    }
    public class daily
    {
        public string summary { get; set; }

        public string icon { get; set; }

         public List<dailydata> data  { get; set; }
    }
    
    public class dailydata
    {
    public string time { get; set; }
    public string summary { get; set; }
    public string icon { get; set; }
    public string sunriseTime { get; set; }
    public string moonPhase { get; set; }
    public string precipIntensity { get; set; }
    public string precipIntensityMax { get; set; }
    public string precipIntensityMaxTime { get; set; }
    public string precipProbability { get; set; }
    public string precipType { get; set; }
    public string temperatureHigh { get; set; }
    public string temperatureHighTime { get; set; }
    public string temperatureLow { get; set; }
    public string temperatureLowTime { get; set; }
    public string apparentTemperatureHigh { get; set; }
    public string apparentTemperatureHighTime { get; set; }
    public string apparentTemperatureLow { get; set; }
    public string apparentTemperatureLowTime { get; set; }
    public string dewPoint { get; set; }
    public string humidity { get; set; }
    public string pressure { get; set; }
    public string windSpeed { get; set; }
    public string windGust { get; set; }
    public string windGustTime { get; set; }
    public string windBearing { get; set; }

    public string cloudCover { get; set; }

    public string uvIndex { get; set; }

    public string uvIndexTime { get; set; }

    public string visibility { get; set; }

    public string ozone { get; set; }

   // public string ozone { get; set; }

    public string temperatureMin { get; set; }

    public string temperatureMax { get; set; }


    public string temperatureMaxTime { get; set; }

    public string apparentTemperatureMin { get; set; }

     public string apparentTemperatureMinTime { get; set; }

    public string apparentTemperatureMax { get; set; }

    public string apparentTemperatureMaxTime { get; set; }

    }
    
    
}

