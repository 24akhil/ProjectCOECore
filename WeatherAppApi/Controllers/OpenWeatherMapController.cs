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
namespace WeatherAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenWeatherMapController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        //For Radis
         // ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("redis_image:6379,abortConnect=False");
        //   ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("localhost");
        
        public OpenWeatherMapController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
        }

    [HttpPost]
     public Object GetOpenWeatherMap(ApiRequest apirequest) 
     {    
         
          
              WeatherService obj=new WeatherService();
              OpenWeatherFinal obj1=new OpenWeatherFinal();
              IDatabase db=AppConstant.rediscon.GetDatabase(); 
           
             
                var projectData=db.StringGet("OpenWeather"+apirequest.Place+apirequest.RequestDate.date);
             
              if (!string.IsNullOrEmpty(projectData))
              {   
                   Console.WriteLine("Get Data From Cache");  
                  // return   JsonConvert.DeserializeObject<OpenWeather>(projectData);
              }
              else
              {   
                  Console.WriteLine("Set Data in Cache");
                  db.StringSet("OpenWeather"+apirequest.Place+apirequest.RequestDate.date,obj.GetOpenWeatherMap(apirequest.Place));
                
                   projectData=db.StringGet("OpenWeather"+apirequest.Place+apirequest.RequestDate.date);  
                  // return  JsonConvert.DeserializeObject<OpenWeather>(projectData);
              }  

                 obj1.OpenWeather=JsonConvert.DeserializeObject<OpenWeather>(projectData);
                  obj1.Tempindiffform= (Tempindiffform)AppConstant.tempconversion(JsonConvert.DeserializeObject<OpenWeather>(projectData).main.temp,'K');
                   return Ok(obj1);
     }       
                         
    }
}