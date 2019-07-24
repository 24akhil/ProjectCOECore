import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Place } from '../_models/place.model';
import { PlaceService } from '../_services/place.service';
import { WeatherapicallService } from '../_services/weatherapicall.service';

@Component({
  selector: 'app-sun-info',
  templateUrl: './sun-info.component.html',
  styleUrls: ['./sun-info.component.scss']
})
export class SunInfoComponent implements OnInit {

  subscription : Subscription;
  place :Place = new Place();
  placeSubscription : Subscription;
  Temperature : any;
  apparentTemperature : any;
  humidity:any;
  pressure : any;
  summary : any;
  windSpeed:any;

  constructor( private weatherdataapicallService : WeatherapicallService,
    private placeService : PlaceService) { 
      this.placeSubscription = this.placeService.getPlace().subscribe((result : Place) => {
        if (result) {
        this.place =result;   
        setTimeout(() => this.refreshSummaryData(),1000);
        } 
        });
        
        this.place.latitude =22;
        this.place.longitude =77;
        this.place.placeName = "Indore";
    }
  
  ngOnInit() {
  setTimeout(() => this.refreshSummaryData(),1000);  
  }

  //to populate summmary.
  refreshSummaryData(){    
     this.weatherdataapicallService.getWeatherData(this.place, "WeatherAverageReport").subscribe((res)=>{                  
     this.Temperature = res.Temp_C;
     this.humidity =res.Humidity;
     this.pressure = res.Pressure;
     this.windSpeed = res.WindSpeed ;
  });
  }

}
