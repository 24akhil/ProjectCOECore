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
    public class SunRiseSunSetController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        //For Radis
       //  ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("localhost");
          

        public SunRiseSunSetController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
        }

    [HttpPost]
     public Object GetSunRiseSunSetData(ApiRequest apirequest) 
     {     
          //ActionResult<SunRiseSunSet>
         if(apirequest.RequestDate.date==null)
         {
             apirequest.RequestDate.date=DateTime.Now.ToString("yyyy-MM-dd");
         }
             WeatherService obj=new WeatherService();
             IDatabase db=AppConstant.rediscon.GetDatabase(); 
           
             
               var projectData=db.StringGet("SunRiseSunSet"+apirequest.Place+apirequest.RequestDate.date);
             
             if (!string.IsNullOrEmpty(projectData))
             {   
                  Console.WriteLine("Get Data From Cache");  
                  return   JsonConvert.DeserializeObject(projectData);
             }
             else
             {   
                 Console.WriteLine("Set Data in Cache");
                 db.StringSet("SunRiseSunSet"+apirequest.Place+apirequest.RequestDate.date,obj.GetSunRiseSunSet(apirequest.RequestDate.date,apirequest.lat,apirequest.lon));
                
                  projectData=db.StringGet("SunRiseSunSet"+apirequest.Place+apirequest.RequestDate.date);  
                  return  JsonConvert.DeserializeObject(projectData);
             }   
     }       
                         
    }
}