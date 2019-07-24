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
    public class APixuController : ControllerBase
    {
        private readonly WeatherService _weatherService;
       // ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("localhost");
      

        public APixuController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
        }

    [HttpPost]
    //ActionResult<APixuWeatherReport>
     public Object GetWeatherReport(ApiRequest apirequest) 
     {      
         if (apirequest.Place == null  )
          {
                  return NotFound();
          } 
              WeatherService obj=new WeatherService();   
              APixuWeatherReportFinal obj1=new APixuWeatherReportFinal(); 
            IDatabase db=AppConstant.rediscon.GetDatabase(); 

      var projectData=db.StringGet("Apixu"+apirequest.Place+apirequest.RequestDate.date);
             
             if (!string.IsNullOrEmpty(projectData))
             {   
                  Console.WriteLine("Get Data From Cache");  
                 // return   JsonConvert.DeserializeObject<APixuWeatherReport>(projectData);
             }
             else
            {   
                 Console.WriteLine("Set Data in Cache");
                db.StringSet("Apixu"+apirequest.Place+apirequest.RequestDate.date,obj.GetAPixuWeatherReport(apirequest.Place));
                
                  projectData=db.StringGet("Apixu"+apirequest.Place+apirequest.RequestDate.date);  
                 // return  JsonConvert.DeserializeObject<APixuWeatherReport>(projectData);
             }

                   obj1.APixuWeatherReport=JsonConvert.DeserializeObject<APixuWeatherReport>(projectData);
                  // Console.WriteLine();
                      //Console.WriteLine(JsonConvert.DeserializeObject<APixuWeatherReport>(projectData).current.temp_c,'C');
                    obj1.Tempindiffform= (Tempindiffform)AppConstant.tempconversion(JsonConvert.DeserializeObject<APixuWeatherReport>(projectData).current.temp_c,'C');
                   return obj1;
      }                        
    }
}
