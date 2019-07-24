import { KeyValue } from '@angular/common';

export class AppConstants {
    //public static get baseURL(): string { return  "/api/"; }
//  public static get baseURL(): string { return "http://192.168.99.100:5000/api/"; }
   public static get baseURL(): string { return "http://23.99.142.41:5000/api/"; }
//   public static get baseURL(): string { return "http://localhost:5000/api/"; }
    
   // public static weatherProviderList: KeyValue {return }
public static weatherProviderList = ["DarkSky",
                                    //  "WeatherUnlock/Current",
                                        "OpenWeatherMap",
                                    // "WeatherBit/Current",
                                        "APixu"
                                ];        
                                
                                
    public static tempGraph:any = [];
    public static tempDarkSkyGraph:number;
    public static tempOpenWeatherMapGraph:number;
    public static tempAPixuGraph:number;
                                 
}
export enum DarkSkyCurrent {
    temperature ="temperature",
    apparentTemperature = "apparentTemperature",
    humidity =   "humidity",
    pressure =  "pressure",
    summary = "summary",
    windSpeed = "windSpeed"
};

export enum WeatherUnlockCurrent {
    temperature ="temp_c",
    apparentTemperature = "feelslike_c",
    humidity =   "humid_pct",
    pressure =  "pressure",
    summary = "wx_desc",
    windSpeed = "windspd_kmh"
};

export enum OpenWeatherMapCurrent {
    temperature ="temp",
    //apparentTemperature = "apparentTemperature",
    humidity =   "humidity",
    pressure =  "pressure",
    summary = "weather[description]",
    windSpeed = "wind[speed]"
};

export enum APixu {
    temperature ="temp_c",
    apparentTemperature = "feelslike_c",
    humidity =   "humid_pct",
    pressure =  "pressure",
    summary = "wx_desc",
    windSpeed = "windspd_kmh"
};

  

