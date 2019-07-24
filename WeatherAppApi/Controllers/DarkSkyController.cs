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
using StackExchange.Redis;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
// using System.Web.Http.Cors;

namespace WeatherAppApi.Controllers
{
    [Route("api/[controller]")]
    // [EnableCors("AllowMyOrigin")]
    [ApiController]
    public class DarkSkyController : ControllerBase
    {
        private readonly WeatherService _weatherService;
       
      //  ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("redis_image:6379,abortConnect=False");
        // ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("localhost");
        public DarkSkyController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
        }

    [HttpPost]
     //ActionResult<DarkSky>
     public Object GetDarkSkyWeatherReport(ApiRequest apirequest) 
     {      
              if (apirequest.lat+","+apirequest.lon == null  )
             {
                 return NotFound();
             } 
              WeatherService obj=new WeatherService();
              DarkSkyFinalData obj1=new DarkSkyFinalData();

               IDatabase db=AppConstant.rediscon.GetDatabase(); 

            var projectData=db.StringGet("DarkSky"+apirequest.Place+apirequest.RequestDate.date);
             
             if (!string.IsNullOrEmpty(projectData))
             {   
                  Console.WriteLine("Get Data From Cache");  
                 // return   JsonConvert.DeserializeObject<DarkSky>(projectData);
             }
             else
             {   
                 Console.WriteLine("Set Data in Cache");
                 db.StringSet("DarkSky"+apirequest.Place+apirequest.RequestDate.date,obj.GetDarkSkyWeatherReport(apirequest.lat+","+apirequest.lon));
                
                  projectData=db.StringGet("DarkSky"+apirequest.Place+apirequest.RequestDate.date); 
                  
                  
               //   return  JsonConvert.DeserializeObject<DarkSky>(projectData).currently.temperature;
             } 

                 
                  obj1.DarkSky=JsonConvert.DeserializeObject<DarkSky>(projectData);
                  obj1.Tempindiffform= (Tempindiffform)AppConstant.tempconversion(JsonConvert.DeserializeObject<DarkSky>(projectData).currently.temperature,'F');
                  return Ok(obj1);
     }       
                         
    }
}