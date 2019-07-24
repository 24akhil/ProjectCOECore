using WeatherAppApi.Models;
using WeatherAppApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WeatherAppApi.Controllers;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using  System.Threading.Tasks;
using StackExchange.Redis;

namespace WeatherAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherReportAllApiController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        //For Caching
       
        public WeatherReportAllApiController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
            
        }

    [HttpGet,Route("WeatherReport/Current")]
     public ActionResult<WeatherReportAllApiCurrent> GetWeatherReportCurrent(string place) 
     {      
            if (place == null  )
             {
                 return NotFound();
             } 
              string date=DateTime.Now.ToString("yyyy-MM-dd");
             WeatherService obj=new WeatherService();
              var ApixuWeatherData = JsonConvert.DeserializeObject<APixuWeatherReport>(obj.GetAPixuWeatherReport(place));
              var sunRiseSunSetData = JsonConvert.DeserializeObject<SunRiseSunSet>(obj.GetSunRiseSunSet(date,ApixuWeatherData.location.lat,ApixuWeatherData.location.lon));
              var DarkSkytData = JsonConvert.DeserializeObject<DarkSky>(obj.GetDarkSkyWeatherReport(ApixuWeatherData.location.lat+","+ApixuWeatherData.location.lon));
              Console.WriteLine(ApixuWeatherData.location.lat);
              
              WeatherReportAllApiCurrent  obj1=new WeatherReportAllApiCurrent();
             // obj1.ApixuCurrentReport=ApixuWeatherData;
             // obj1.SunRIseSunSetCurrentReport=sunRiseSunSetData;
             // obj1.DarkSkyCurrentReport=DarkSkytData;
              
              return  obj1;
             
     }    
     
       [HttpGet]
        public string GetData()
        {
               ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("localhost");
                IDatabase db=AppConstant.rediscon.GetDatabase();     
                db.StringSet("Test","Te"); 
                string val=db.StringGet("Test"); 
                return val;     
        }
    //  [HttpGet,Route("WeatherReport/Weekely")]
    //  public ActionResult<WeatherReportAllApiCurrent> GetWeatherReportWeekely(string place) 
    //  {      
    //         if (place == null  )
    //          {
    //              return NotFound();
    //          } 
    //           WeatherService obj=new WeatherService();
    //           //string place="Indore";
    //           var projectData = JsonConvert.DeserializeObject<WeatherReportAllApi>(obj.GetAPixuWeatherReport(place));
    //           return  projectData;
    //  }    
     
    //  [HttpGet,Route("WeatherReport/Daily")]
    //  public ActionResult<WeatherReportAllApiCurrent> GetWeatherReportDaily(string place) 
    //  {      
    //         if (place == null  )
    //          {
    //              return NotFound();
    //          } 
    //           WeatherService obj=new WeatherService();
    //           //string place="Indore";
    //           var projectData = JsonConvert.DeserializeObject<WeatherReportAllApi>(obj.GetAPixuWeatherReport(place));
    //           return  projectData;
    //  }    
     
     
 

//  if (!string.IsNullOrEmpty(existingTime))
//  {
//  return "Fetched from cache : " + existingTime;
//  }
//  else
//  {
//  existingTime = DateTime.UtcNow.ToString();
//  _distributedCache.SetString(cacheKey, existingTime);
//  return "Added to cache : " + existingTime;
//  }
                   
    }
}