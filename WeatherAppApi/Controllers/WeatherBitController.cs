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
    public class WeatherBitController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        //For Radis
       //  ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("localhost");
         // var rediscon=AppConstant.rediscon;

        public WeatherBitController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
        }

    [HttpPost,Route("Current")]
     public Object GetCurrentReportWeatherBit(ApiRequest apirequest) 
     {        
             WeatherService obj=new WeatherService();
             IDatabase db=AppConstant.rediscon.GetDatabase(); 
           
             
               var projectData=db.StringGet("WeatherBitCurrent"+apirequest.Place+apirequest.RequestDate.date);
             
             if (!string.IsNullOrEmpty(projectData))
             {   
                  Console.WriteLine("Get Data From Cache");  
                  return   JsonConvert.DeserializeObject(projectData);
             }
             else
             {   
                 Console.WriteLine("Set Data in Cache");
                 db.StringSet("WeatherBitCurrent"+apirequest.Place+apirequest.RequestDate.date,obj.GetCurrentReportWeatherBit(apirequest.Place));
                
                  projectData=db.StringGet("WeatherBitCurrent"+apirequest.Place+apirequest.RequestDate.date);  
                  return  JsonConvert.DeserializeObject(projectData);
             }   
     }       

        [HttpPost,Route("Daily")]
     public Object GetDailyReportWeatherBit(ApiRequest apirequest) 
     {     
          
         
             WeatherService obj=new WeatherService();
             IDatabase db=AppConstant.rediscon.GetDatabase(); 
           
             
               var projectData=db.StringGet("WeatherBitDaily"+apirequest.Place+apirequest.RequestDate.date);
             
             if (!string.IsNullOrEmpty(projectData))
             {   
                  Console.WriteLine("Get Data From Cache");  
                  return   JsonConvert.DeserializeObject(projectData);
             }
             else
             {   
                 Console.WriteLine("Set Data in Cache");
                 db.StringSet("WeatherBitDaily"+apirequest.Place+apirequest.RequestDate.date,obj.GetDailyReportWeatherBit(apirequest.Place));
                
                  projectData=db.StringGet("WeatherBitDaily"+apirequest.Place+apirequest.RequestDate.date);  
                  return  JsonConvert.DeserializeObject(projectData);
             }   
     } 



     [HttpGet,Route("Hourly")]
     public Object GetHourlyReportWeatherBit(string lat,string lon,string start_date,string end_date) 
     {     
           Console.WriteLine(lat);
           Console.WriteLine(lon);
           Console.WriteLine(start_date);
           Console.WriteLine(end_date);
         
             WeatherService obj=new WeatherService();
             IDatabase db=AppConstant.rediscon.GetDatabase(); 
           
             
               var projectData=db.StringGet("WeatherBitHourly"+lat+lon+start_date+end_date);
             
             if (!string.IsNullOrEmpty(projectData))
             {   
                  Console.WriteLine("Get Data From Cache");  
                  return   JsonConvert.DeserializeObject(projectData);
             }
             else
             {   
                 Console.WriteLine("Set Data in Cache");
                 db.StringSet("WeatherBitHourly"+lat+lon+start_date+end_date,obj.GetHourlyReportWeatherBit(lat,lon,start_date,end_date));
                 Console.WriteLine("Set Data in Cache");
                  projectData=db.StringGet("WeatherBitHourly"+lat+lon+start_date+end_date); 
                   Console.WriteLine("Set Data in Cache"); 
                  return  JsonConvert.DeserializeObject(projectData);
             }   
     }

    }
}