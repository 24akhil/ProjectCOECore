import { Component, OnInit } from '@angular/core';
// import { PlaceService } from '../_services/place.service';
//import { Subscription, Observable } from 'rxjs';
//import { Place } from '../_models/place.model';
// import { WeatherapicallService } from '../_services/weatherapicall.service';
import { AppConstants,
  //  DarkSkyCurrent
   } from '../AppConstants';

@Component({
  selector: 'app-weatherui',
  templateUrl: './weatherui.component.html',
  styleUrls: ['./weatherui.component.scss']
})
export class WeatheruiComponent implements OnInit {

  showCurrent : boolean = false;
  showDaily : boolean = false;
  showHourly : boolean = false;
  
  private weatherProviders : any;
  // constructor(private weatherdataapicallService : WeatherapicallService,
  //             private placeService : PlaceService) {    
  // }

  ngOnInit() {
    this.weatherProviders = AppConstants.weatherProviderList;
    this.weatherProviders.forEach(element => {
          console.log(element);            
         });  
    this.showCurrent =true;
  }



  toggleWeatherDisplayType(event,item){
    if(item==="current"){
      this.offWeatherDisplay();
      this.showCurrent = true;
    }
    if(item==="daily"){
      this.offWeatherDisplay();
      this.showDaily = true;
    }
    if(item==="hourly"){
      this.offWeatherDisplay();
      this.showHourly = true;
    }
  }

  offWeatherDisplay(){
    this.showCurrent = false;
      this.showDaily =false;
      this.showHourly = false;
  }
}
