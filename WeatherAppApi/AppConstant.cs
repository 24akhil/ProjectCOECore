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
namespace WeatherAppApi
{
   
    public class AppConstant 
    {
      public static ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("redis_image:6379,abortConnect=False"); 
    //  public static ConnectionMultiplexer rediscon=ConnectionMultiplexer.Connect("localhost");   

         public static Object tempconversion(string temp,char type)
        {
             Tempindiffform obj=new Tempindiffform();
             float data=(float) Convert.ToDouble(temp);
            if (type=='K')
            {
                Console.WriteLine("K"+data);
               obj.tempc=data-273;
               obj.tempf=((9*(obj.tempc))/5)+32;
               obj.tempk=data;
            }
            if(type=='C')
            {
                Console.WriteLine("C"+data);
               obj.tempc=data;
               obj.tempf=((9*(data))/5)+32;
               obj.tempk=obj.tempc+273;
            }
            if(type=='F')
            {
                 Console.WriteLine("F"+data);
                 obj.tempc=(5*(data-32))/9;
               obj.tempf=data;
               obj.tempk=obj.tempc+273;
            }
              return obj;
       }              
    }
}