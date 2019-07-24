using WeatherAppApi.Models;
using WeatherAppApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WeatherAppApi.Controllers;
using Newtonsoft.Json;
using System.Web.Http;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using StackExchange.Redis;
namespace WeatherAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherUnlockController : ControllerBase
    {
        private readonly WeatherService _weatherService;
       // ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("localhost");
      // var rediscon=AppConstant.rediscon;

        public WeatherUnlockController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
        }

    [HttpPost,Route("Forcast")]
    //ActionResult<APixuWeatherReport>
     public Object GetWeatherReportWeatherUnlockForcast(ApiRequest apirequest) 
     {      
            if (apirequest.lat+","+apirequest.lon == null  )
             {
                 return NotFound();
             } 
              WeatherService obj=new WeatherService();
              IDatabase db=AppConstant.rediscon.GetDatabase(); 

              var projectData=db.StringGet("WeatherUnlockForcast"+apirequest.Place+apirequest.RequestDate.date);
             
             if (!string.IsNullOrEmpty(projectData))
             {   
                  Console.WriteLine("Get Data From Cache");  
                  return   JsonConvert.DeserializeObject(projectData);
             }
             else
             {   
                 Console.WriteLine("Set Data in Cache");
                 Console.WriteLine(apirequest.lat+","+apirequest.lon);
                 db.StringSet("WeatherUnlockForcast"+apirequest.Place+apirequest.RequestDate.date,obj.GetWeatherReportWeatherUnlockForcast(apirequest.lat+","+apirequest.lon));
                
                  projectData=db.StringGet("WeatherUnlockForcast"+apirequest.Place+apirequest.RequestDate.date);  
                  return  JsonConvert.DeserializeObject(projectData);
             }
     }      


     [HttpPost]
    //ActionResult<APixuWeatherReport>
     public Object GetWeatherReportWeatherUnlockCurrent(ApiRequest apirequest) 
     {      
            if (apirequest.lat+","+apirequest.lon == null  )
             {
                 return NotFound();
             } 
              WeatherService obj=new WeatherService();
              IDatabase db=AppConstant.rediscon.GetDatabase(); 

              var projectData=db.StringGet("WeatherUnlockCurrent"+apirequest.Place+apirequest.RequestDate.date);
             
             if (!string.IsNullOrEmpty(projectData))
             {   
                  Console.WriteLine("Get Data From Cache");  
                  return   JsonConvert.DeserializeObject<WeatherUnlock>(projectData);
             }
             else
             {   
                 Console.WriteLine("Set Data in Cache");
                 Console.WriteLine(apirequest.lat+","+apirequest.lon);
                 db.StringSet("WeatherUnlockCurrent"+apirequest.Place+apirequest.RequestDate.date,obj.GetWeatherReportWeatherUnlockCurrent(apirequest.lat+","+apirequest.lon));
                
                  projectData=db.StringGet("WeatherUnlockCurrent"+apirequest.Place+apirequest.RequestDate.date);  
                  return  JsonConvert.DeserializeObject<WeatherUnlock>(projectData);
             }
     }                         
    }
}