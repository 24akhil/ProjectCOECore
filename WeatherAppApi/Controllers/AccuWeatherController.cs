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
    public class AccuWeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;
       // ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("localhost");
     // var rediscon=AppConstant.rediscon;
        public AccuWeatherController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
        }

    [HttpPost,Route("Current")]
    //ActionResult<APixuWeatherReport>
     public Object GetAccuWeatherReportCurrent(ApiRequest apirequest) 
     {      
            if ((apirequest.lat  == null ) || (apirequest.lon==null) )
             {
                 return NotFound();
             } 
              WeatherService obj=new WeatherService();
              IDatabase db=AppConstant.rediscon.GetDatabase(); 

              var projectData=db.StringGet("ACCUCurrent"+apirequest.Place+apirequest.RequestDate.date);
             
             if (!string.IsNullOrEmpty(projectData))
             {   
                  Console.WriteLine("Get Data From Cache");  
                  return   JsonConvert.DeserializeObject(projectData);
             }
             else
             {   
                 Console.WriteLine("Set Data in Cache");
                 db.StringSet("ACCUCurrent"+apirequest.Place+apirequest.RequestDate.date,obj.GetAccuWeatherReportLocID(apirequest.lat+","+apirequest.lon));
                
                  projectData=db.StringGet("ACCUCurrent"+apirequest.Place+apirequest.RequestDate.date);  
                  
                 var projectData1= JsonConvert.DeserializeObject<Accu>(projectData);
                 Console.WriteLine(projectData1.Key);

                 db.StringSet("ACCUCurrent"+apirequest.Place+apirequest.RequestDate.date,obj.GetAccuWeatherReport(projectData1.Key));
                
                  projectData=db.StringGet("ACCUCurrent"+apirequest.Place+apirequest.RequestDate.date);  
                  
                  return  JsonConvert.DeserializeObject(projectData);
             }
     }  
                   
    }
}