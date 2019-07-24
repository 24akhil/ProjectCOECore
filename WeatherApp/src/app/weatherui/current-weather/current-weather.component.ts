import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { WeatherapicallService } from 'src/app/_services/weatherapicall.service';
import { PlaceService } from 'src/app/_services/place.service';
import { Place } from 'src/app/_models/place.model';
import { Subscription, Observable } from 'rxjs';
import { DarkSkyCurrent, AppConstants, OpenWeatherMapCurrent, WeatherUnlockCurrent } from 'src/app/AppConstants';


export class CurrentWeatherDataTemplate{
  label : string;
  value : any;
  otherValue:any;

  constructor(label : string, value : string,otherValue:string){
    this.label = label;
    this.value = value;
    this.otherValue =otherValue
  }
  
}

@Component({
  selector: 'app-current-weather',
  templateUrl: './current-weather.component.html',
  styleUrls: ['./current-weather.component.scss']
})
export class CurrentWeatherComponent implements OnInit,OnDestroy {
 
   
  @Input() weatherProvider : string;
  place :Place = new Place();
  placeSubscription : Subscription;
  Temperature : any;
  apparentTemperature : any;
  humidity:any;
  pressure : any;
  summary : any;
  windSpeed:any;
  dataTemplate : CurrentWeatherDataTemplate[] = [];
  // private weatherdata  = [];
  // private weatherdataObservable : Observable<any[]> ; 
  weatherdataSubscription : Subscription;

  
  constructor(private weatherdataapicallService : WeatherapicallService,
    private placeService : PlaceService) { 

this.placeSubscription = this.placeService.getPlace().subscribe((result : Place) => {
if (result) {
this.place =result;   
this.refreshWeatherData();
} 
});

this.place.latitude =22;
this.place.longitude =77;
this.place.placeName = "Indore";
}

  ngOnInit() {
    this.refreshWeatherData();
  }

  ngOnDestroy(): void {
    this.placeSubscription.unsubscribe;
  }
  
  refreshWeatherData(){

    switch (this.weatherProvider) {
      case "DarkSky":     
     this.weatherdataapicallService.getWeatherData(this.place, this.weatherProvider).subscribe((res)=>{        
         var resBit = Object.assign({}, res);
         console.log(res);
         this.initialiseDarkSkyData(res);
     });
          break;
      case "OpenWeatherMap": 
      this.weatherdataapicallService.getWeatherData(this.place, this.weatherProvider).subscribe((res)=>{       
          this.initialiseOpenWeatherMapData(res);
      });
          break;
      case "WeatherUnlock/Current": 
       this.weatherdataapicallService.getWeatherData(this.place, this.weatherProvider).subscribe((res)=>{            
         this.initialiseWeatherUnlockCurrentData(res);
     });
      case "WeatherBit/Current": 
     this.weatherdataapicallService.getWeatherData(this.place, this.weatherProvider).subscribe((res)=>{
       var resBitt = Object.assign({}, res); 
      res["q"] = res;
       this.initialiseWeatherBitCurrentData(res);
      });
      case "APixu": 
       this.weatherdataapicallService.getWeatherData(this.place, this.weatherProvider).subscribe((resa)=>{            
         this.initialiseAPixuData(resa);
     });
      default:
  }
  }

  initialiseDarkSkyData(res:any){
    this.dataTemplate.length=0;
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Temperature",res["Tempindiffform"]["tempc"],res["Tempindiffform"]["tempf"]));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Real feel",res["DarkSky"]["currently"][DarkSkyCurrent.apparentTemperature]+" F",""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Humidity",res["DarkSky"]["currently"][DarkSkyCurrent.humidity]+" Rh",""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Pressure",res["DarkSky"]["currently"][DarkSkyCurrent.pressure]+" mb",""));    
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Wind Speed",res["DarkSky"]["currently"][DarkSkyCurrent.windSpeed]+" kmph",""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Summary",res["DarkSky"]["currently"][DarkSkyCurrent.summary],""));       
  }

  initialiseOpenWeatherMapData(res:any){
    this.dataTemplate.length=0;    
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Temperature",res["Tempindiffform"]["tempc"],res["Tempindiffform"]["tempf"]));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Humidity",res["OpenWeather"]["main"][OpenWeatherMapCurrent.humidity]+" %",""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Pressure",res["OpenWeather"]["main"][OpenWeatherMapCurrent.pressure]+" mb",""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Wind Speed",res["OpenWeather"]["wind"]["speed"]+" mps",""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Summary",res["OpenWeather"]["weather"][0]["main"],""));  
  }

  initialiseWeatherUnlockCurrentData(res:any){   
    this.dataTemplate.length=0;
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Temperature",res[WeatherUnlockCurrent.temperature] +"C",""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Real feel",res[WeatherUnlockCurrent.apparentTemperature]+"C",""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Humidity",res[WeatherUnlockCurrent.humidity],""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Wind Speed",res[WeatherUnlockCurrent.windSpeed]+"kmh",""));   
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Summary",res[WeatherUnlockCurrent.summary],""));
  }

  initialiseWeatherBitCurrentData(res:any){
    this.dataTemplate.length=0;
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Temperature",res["data"][0]["temp"] +"C",""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Precipitation",res["data"][0]["precip"],""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Humidity",res["data"][0]["rh"] +"Relative Humidity",""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Pressure",res["data"][0]["pres"],""));   
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Summary",res["data"][0]["weather"]["description"],""));
    this.dataTemplate.push(new CurrentWeatherDataTemplate("Wind Speed",res["data"][0]["wind_spd"]+"kmh","")); 
  }

  initialiseAPixuData(res:any){
     this.dataTemplate.length=0;   
     console.log(res);
     this.dataTemplate.push(new CurrentWeatherDataTemplate("Temperature",res["Tempindiffform"]["tempc"],res["Tempindiffform"]["tempf"]));
     this.dataTemplate.push(new CurrentWeatherDataTemplate("Real feel",res["APixuWeatherReport"]["current"]["feelslike_c"],res["APixuWeatherReport"]["current"]["feelslike_f"]));
     this.dataTemplate.push(new CurrentWeatherDataTemplate("Humidity",res["APixuWeatherReport"]["current"]["humidity"] +" %",""));
     this.dataTemplate.push(new CurrentWeatherDataTemplate("Pressure",res["APixuWeatherReport"]["current"]["pressure_mb"]+" mb",""));       
     this.dataTemplate.push(new CurrentWeatherDataTemplate("Wind Speed",res["APixuWeatherReport"]["current"]["wind_kph"]+" kmph",""));
     this.dataTemplate.push(new CurrentWeatherDataTemplate("Summary",res["APixuWeatherReport"]["current"]["condition"]["text"],"")); 
  
  }
  voteFavourite(){
    console.log(this.weatherProvider,this.place.placeName);
    this.weatherdataapicallService.votefavourite(this.weatherProvider,this.place.placeName);
  }
}
