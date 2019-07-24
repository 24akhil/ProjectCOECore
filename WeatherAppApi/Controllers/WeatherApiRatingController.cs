using WeatherAppApi.Models;
using WeatherAppApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WeatherAppApi.Controllers;
using System.Web.Http;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
namespace WeatherAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherApiRatingController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherApiRatingController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
        }
         
    
         [HttpPost, Route("Rating")]  
        public  ActionResult<WeatherApiRating> AddRating(ApiRating apirequest)
        {
            Console.WriteLine(apirequest.ApiName);
            Console.WriteLine(apirequest.Location);
            WeatherApiRating rating=new WeatherApiRating();
            rating.ApiName=apirequest.ApiName;
            rating.Location=apirequest.Location;
            
            rating.key=apirequest.Location+apirequest.ApiName;

           var weatherappuser2 = _weatherService.GetByApiName(apirequest.Location+apirequest.ApiName);
           if (weatherappuser2==null)
           {
                  rating.Rating=1;
           }
           else
           { 
                _weatherService.RemoveRating(apirequest.Location+apirequest.ApiName);      
                Console.WriteLine(weatherappuser2.Rating);
                rating.Rating= weatherappuser2.Rating+1;
           }
         _weatherService.AddApiRating(rating);
            return rating;
        }

        
        
    }
}