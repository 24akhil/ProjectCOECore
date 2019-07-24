using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherAppApi.Models
{
    
    public class  WeatherUnlock
{
   public string lat { get; set; }
   public string lon { get; set; }
   public string alt_m { get; set; }
   public string alt_ft { get; set; }
   public string wx_desc { get; set; }
   public string wx_code { get; set; }
   public string wx_icon { get; set; }
   public string temp_c { get; set; }
   public string temp_f { get; set; }
   public string feelslike_c { get; set; }
   public string feelslike_f { get; set; }
   public string humid_pct { get; set; }
   public string windspd_mph { get; set; }
   public string windspd_kmh { get; set; }
   public string windspd_kts { get; set; }
   public string windspd_ms { get; set; }
   public string winddir_deg { get; set; }
   public string winddir_compass { get; set; }
   public string cloudtotal_pct { get; set; }
   public string vis_km { get; set; }
   public string vis_mi { get; set; }
   public string vis_desc { get; set; }
   public string slp_mb { get; set; }
   public string slp_in { get; set; }
   public string dewpoint_c { get; set; }
   public string dewpoint_f { get; set; }
}

    

    

}
