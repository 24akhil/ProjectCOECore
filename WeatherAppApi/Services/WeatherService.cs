using WeatherAppApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.IO;
using System.Text.Encodings.Web;
//using System.Net.Http;
//using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Configuration;
//using System.IO;
//using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;


namespace WeatherAppApi.Services
{
    public class WeatherService
    {
        private readonly IMongoCollection<WeatherAppUser> _Weather;
        private readonly IMongoCollection<WeatherApiRating> _Rating;

        public WeatherService(IWeatherstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Weather = database.GetCollection<WeatherAppUser>(settings.WeatherCollectionName);
            _Rating = database.GetCollection<WeatherApiRating>("WebApiRating");
        }
        public WeatherService()
        {

        }

       public List<WeatherAppUser> Get() =>
            _Weather.Find(weatherappuser => true).ToList();
            
        public WeatherAppUser Get(string Email) =>
             _Weather.Find<WeatherAppUser>(weatherappuser => weatherappuser.Email == Email).FirstOrDefault();
        public WeatherAppUser GetByName(string Email) {
         return   _Weather.Find<WeatherAppUser>(weatherappuser => weatherappuser.Email == Email).FirstOrDefault();  
        }
        public WeatherAppUser GetByName1(string Password) =>
            _Weather.Find<WeatherAppUser>(weatherappuser => weatherappuser.Password == Password).FirstOrDefault();        

        //SignUP 
        public LoginResponse CreateUser(LoginResponse login )
        {
            WeatherAppUser obj=new WeatherAppUser();
            obj.Email= login.Email;
            obj.Password=login.Password;
            obj.FirstName=login.FirstName;
            obj.LastName=login.LastName;
            _Weather.InsertOne(obj);
            return login;
        }

        //Add Rating 
        public WeatherApiRating AddApiRating(WeatherApiRating rating)
        {
            _Rating.InsertOne(rating);
            return rating;
        }
        public WeatherApiRating GetByApiName(string key) =>
            _Rating.Find<WeatherApiRating>(weatherapirating => weatherapirating.key == key).FirstOrDefault();  

        public WeatherApiRating GetByID(string id) =>
            _Rating.Find<WeatherApiRating>(weatherapirating => weatherapirating.id == id).FirstOrDefault();  

         public void RemoveRating(string key) => 
            _Rating.DeleteOne(weatherapirating => weatherapirating.key == key); 

      
        //Login
        public WeatherAppUser CreateLogin(WeatherAppUser weatherappuser)
        {
            _Weather.InsertOne(weatherappuser);
            return weatherappuser;
        }


        public void Update(string Email, WeatherAppUser weatherappuserIn) =>
            _Weather.ReplaceOne(weatherappuser => weatherappuser.Email == Email, weatherappuserIn);

      //  public void Remove(WeatherAppUser weatherappuserIn) =>
          //  _Weather.DeleteOne(weatherappuser => weatherappuser.Id == weatherappuserIn.Id);

        public void Remove(string Email) => 
            _Weather.DeleteOne(weatherappuser => weatherappuser.Email == Email);

              public string GetSunRiseSunSet(string date,string latitude,string longitude)
              {      
                      Console.WriteLine(date);
                      Console.WriteLine(latitude);
                       Console.WriteLine(longitude);
                       Console.WriteLine("https://api.sunrise-sunset.org/json?lat="+latitude+"&lng="+longitude+"&date="+date);
                      HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("https://api.sunrise-sunset.org/json?lat="+latitude+"&lng="+longitude+"+&date="+date);
                      webrequest.Method = "GET";
                      webrequest.ContentType = "application/json";
                      HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                      Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                      StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                      var result = responseStream.ReadToEnd();
                      return result;
             }

             public string GetAPixuWeatherReport(string place)
              {
                      HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("http://api.apixu.com/v1/current.json?key=45ec29d7cf064796931100608190207&q="+place);
                      webrequest.Method = "GET";
                      webrequest.ContentType = "application/json";
                      HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                      Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                      StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                      var result = responseStream.ReadToEnd();
                      return result;
             }

             public string GetDarkSkyWeatherReport(string latlon)
              {       

                      HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("https://api.darksky.net/forecast/8b5aa6a008633ba254abbc7f3f0a12dc/"+latlon);
                      webrequest.Method = "GET";
                      webrequest.ContentType = "application/json";
                      HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                      Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                      StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                      var result = responseStream.ReadToEnd();
                      return result;
             }


             public string GetCurrentReportWeatherBit(string Place)
              {       

                      HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("https://api.weatherbit.io/v2.0/current?city="+Place+"&key="+"c228919584a8453b90188c57e37b45ce");
                      webrequest.Method = "GET";
                      webrequest.ContentType = "application/json";
                      HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                      Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                      StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                      var result = responseStream.ReadToEnd();
                      return result;
             }


            public string GetDailyReportWeatherBit(string Place)
              {       
                      Console.WriteLine("https://api.weatherbit.io/v2.0/forecast/daily?city="+Place+"&key=c228919584a8453b90188c57e37b45ce");
                      HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("https://api.weatherbit.io/v2.0/forecast/daily?city="+Place+"&key=c228919584a8453b90188c57e37b45ce");
                      webrequest.Method = "GET";
                      webrequest.ContentType = "application/json";
                      HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                      Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                      StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                      var result = responseStream.ReadToEnd();
                      return result;
             }

             public string GetHourlyReportWeatherBit(string lat,string lon,string start_date,string end_date)
              {       

                      HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("https://api.weatherbit.io/v2.0/history/hourly?lat="+lat+"&lon="+lon+"&start_date="+start_date+"&end_date="+end_date+"&tz=local&key=c228919584a8453b90188c57e37b45ce");
                      webrequest.Method = "GET";
                      webrequest.ContentType = "application/json";
                      HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                      Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                      StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                      var result = responseStream.ReadToEnd();
                      return result;
             }

             public string GetOpenWeatherMap(string Place)
             {
                  HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("https://community-open-weather-map.p.rapidapi.com/weather?q="+Place);
                    webrequest.Method = "GET";
                    webrequest.ContentType = "application/json";
                    webrequest.Headers.Add("X-RapidAPI-Host"== null ? "" :"X-RapidAPI-Host", "community-open-weather-map.p.rapidapi.com" == null ? "" : "community-open-weather-map.p.rapidapi.com");
                     webrequest.Headers.Add("X-RapidAPI-Key"== null ? "" :"X-RapidAPI-Key", "b11e6aefdfmsh62ba5ed9a36fc4dp11abebjsn251728af79e3" == null ? "" : "b11e6aefdfmsh62ba5ed9a36fc4dp11abebjsn251728af79e3");
                    HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                    Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                   string result = responseStream.ReadToEnd();
                    Console.WriteLine(result);
                    return result;
             }
            public string GetAccuWeatherReportLocID(string latlon)
            {
                    HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?q="+latlon+"&apikey=EK32OwukAujAQ1QyJcC5lP5eBR7xeaGB");
                    webrequest.Method = "GET";
                    webrequest.ContentType = "application/json";
                 //   webrequest.Headers.Add("X-RapidAPI-Host"== null ? "" :"X-RapidAPI-Host", "community-open-weather-map.p.rapidapi.com" == null ? "" : "community-open-weather-map.p.rapidapi.com");
                   //  webrequest.Headers.Add("X-RapidAPI-Key"== null ? "" :"X-RapidAPI-Key", "b11e6aefdfmsh62ba5ed9a36fc4dp11abebjsn251728af79e3" == null ? "" : "b11e6aefdfmsh62ba5ed9a36fc4dp11abebjsn251728af79e3");
                    HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                    Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                   string result = responseStream.ReadToEnd();
                  //  Console.WriteLine(result);
                    return result;
            }

            
           public string GetAccuWeatherReport(string Key)
            {
                    HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("http://dataservice.accuweather.com/currentconditions/v1/"+Key+"?apikey=EK32OwukAujAQ1QyJcC5lP5eBR7xeaGB");
                    webrequest.Method = "GET";
                    webrequest.ContentType = "application/json";
                   // webrequest.Headers.Add("X-RapidAPI-Host"== null ? "" :"X-RapidAPI-Host", "community-open-weather-map.p.rapidapi.com" == null ? "" : "community-open-weather-map.p.rapidapi.com");
                   //  webrequest.Headers.Add("X-RapidAPI-Key"== null ? "" :"X-RapidAPI-Key", "b11e6aefdfmsh62ba5ed9a36fc4dp11abebjsn251728af79e3" == null ? "" : "b11e6aefdfmsh62ba5ed9a36fc4dp11abebjsn251728af79e3");
                    HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                    Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                   string result = responseStream.ReadToEnd();
                    Console.WriteLine(result);
                    return result;
            }

            
            
            public string  GetWeatherReportWeatherUnlockForcast(string latlon)
            {        
                    Console.WriteLine(latlon);
                    HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("http://api.weatherunlocked.com/api/forecast/"+latlon+"?app_id=1dd2c4e4&app_key=f411b5b9fdf45711d7936fb8879937f8");
                    webrequest.Method = "GET";
                    webrequest.ContentType = "application/json";
                   // webrequest.Headers.Add("X-RapidAPI-Host"== null ? "" :"X-RapidAPI-Host", "community-open-weather-map.p.rapidapi.com" == null ? "" : "community-open-weather-map.p.rapidapi.com");
                   //  webrequest.Headers.Add("X-RapidAPI-Key"== null ? "" :"X-RapidAPI-Key", "b11e6aefdfmsh62ba5ed9a36fc4dp11abebjsn251728af79e3" == null ? "" : "b11e6aefdfmsh62ba5ed9a36fc4dp11abebjsn251728af79e3");
                    HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                    Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = responseStream.ReadToEnd();
                    Console.WriteLine(result);
                    return result;
            }



            public string  GetWeatherReportWeatherUnlockCurrent(string latlon)
            {        
                    Console.WriteLine(latlon);
                    HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("http://api.weatherunlocked.com/api/current/"+latlon+"?app_id=1dd2c4e4&app_key=f411b5b9fdf45711d7936fb8879937f8");
                    webrequest.Method = "GET";
                    webrequest.ContentType = "application/json";
                   // webrequest.Headers.Add("X-RapidAPI-Host"== null ? "" :"X-RapidAPI-Host", "community-open-weather-map.p.rapidapi.com" == null ? "" : "community-open-weather-map.p.rapidapi.com");
                   //  webrequest.Headers.Add("X-RapidAPI-Key"== null ? "" :"X-RapidAPI-Key", "b11e6aefdfmsh62ba5ed9a36fc4dp11abebjsn251728af79e3" == null ? "" : "b11e6aefdfmsh62ba5ed9a36fc4dp11abebjsn251728af79e3");
                    HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                    Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                    string result = responseStream.ReadToEnd();
                    Console.WriteLine(result);
                    return result;
            }

          
    }          
}