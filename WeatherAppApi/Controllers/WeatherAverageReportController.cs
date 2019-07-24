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
using Newtonsoft.Json.Linq;
using System.Threading;

namespace WeatherAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAverageReportController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        //For Radis
        // ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("localhoste");
         
  
        public WeatherAverageReportController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
        }

    [HttpPost]
     public Object GetAverageWeatherReport(ApiRequest apirequest) 
     {   
         Console.WriteLine("1"); 
        Thread.Sleep(4000);
        Console.WriteLine("2");
         Console.WriteLine("GetAverageData");
          IDatabase db=AppConstant.rediscon.GetDatabase();
          string darksky=db.StringGet("DarkSky"+apirequest.Place+apirequest.RequestDate.date);
          Console.WriteLine("darksky");
          JObject json1 = JObject.Parse(darksky);
          Console.WriteLine(Convert.ToInt32(json1["currently"]["windSpeed"]));
          Console.WriteLine(Convert.ToInt32(json1["currently"]["temperature"]));
          Console.WriteLine(Convert.ToInt32(json1["currently"]["humidity"]));
          Console.WriteLine(Convert.ToInt32(json1["currently"]["pressure"]));
          Console.WriteLine(Convert.ToInt32(json1["currently"]["apparentTemperature"]));

         
        string openweather=db.StringGet("OpenWeather"+apirequest.Place+apirequest.RequestDate.date);
        Console.WriteLine("openweather");
        JObject json2 = JObject.Parse(openweather);
        Console.WriteLine(json2["main"]["temp"]);
        Console.WriteLine(json2["main"]["pressure"]);
        Console.WriteLine(json2["main"]["humidity"]);
        Console.WriteLine(json2["wind"]["speed"]);
        Console.WriteLine(json2["main"]["temp"]);
        


        string apixu=db. StringGet("Apixu"+apirequest.Place+apirequest.RequestDate.date); 
        Console.WriteLine("apixu");
        JObject json3 = JObject.Parse(apixu);
        Console.WriteLine(json3["current"]["temp_f"]);
        Console.WriteLine(json3["current"]["humidity"]);
        Console.WriteLine(json3["current"]["pressure_mb"]);
        Console.WriteLine(json3["current"]["wind_kph"]);
        Console.WriteLine(json3["current"]["feelslike_c"]);


        int tempc=((5*(Convert.ToInt32(json1["currently"]["temperature"])-32))/9+ (Convert.ToInt32(json2["main"]["temp"]) -273) + (5*(Convert.ToInt32(json3["current"]["temp_f"])-32))/9)/3;    
        int tempf=((9*(tempc))/5)+32;
        Console.WriteLine(tempc);
        Console.WriteLine(tempf);

        int temphumid=((Convert.ToInt32(json1["currently"]["humidity"])+ Convert.ToInt32(json2["main"]["humidity"]))+    Convert.ToInt32(json3["current"]["humidity"]))/3;

        int temppressure=((Convert.ToInt32(json1["currently"]["pressure"])+ Convert.ToInt32(json2["main"]["pressure"]))+    Convert.ToInt32(json3["current"]["pressure_mb"]))/3;

        int tempwind=((Convert.ToInt32(json1["currently"]["windSpeed"])+ Convert.ToInt32(json2["wind"]["speed"]))+    Convert.ToInt32(json3["current"]["wind_kph"]))/3;

        int tempapprent=((Convert.ToInt32(json1["currently"]["apparentTemperature"])+ Convert.ToInt32(json2["main"]["temp"]))+    Convert.ToInt32(json3["current"]["feelslike_c"]))/3;

       
         AverageReport obj=new AverageReport();
         obj.Temp_C=tempc;
         obj.Temp_F=tempf;
         obj.Pressure=temppressure;
         obj.Humidity=temphumid;
         obj.AppearentTemp=tempapprent;
         obj.WindSpeed=tempwind;

         return  obj;



        // weatherunlock=JsonConvert.DeserializeObject<WeatherUnlock>(db.StringGet("WeatherUnlock"+apirequest.Place+apirequest.RequestDate.date)); 
         // var openweather=Convert.Json(db.StringGet("OpenWeather"+apirequest.Place+apirequest.RequestDate.date));
         // var weatherbitcurrent=JsonConvert.DeserializeObject<WeathrBit>(db.StringGet("WeatherBitCurrent"+apirequest.Place+apirequest.RequestDate.date));
        //  var apixu=JsonConvert.DeserializeObject<APixuWeatherReport>(db. StringGet("Apixu"+apirequest.Place+apirequest.RequestDate.date)); 
  
        //  Console.WriteLine(openweather.main.temp); 
         // var avgtemp=Convert.ToInt32(openweather.main.temp);
         // var avcpreciptate=;
         // var avgwindspeed=;
         
     }

                         
    }
}