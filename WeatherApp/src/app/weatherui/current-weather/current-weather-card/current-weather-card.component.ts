import { Component, OnInit, Input } from '@angular/core';
import { CurrentWeatherDataTemplate } from '../current-weather.component';

@Component({
  selector: 'app-current-weather-card',
  templateUrl: './current-weather-card.component.html',
  styleUrls: ['./current-weather-card.component.scss']
})
export class CurrentWeatherCardComponent implements OnInit {

  @Input() data : CurrentWeatherDataTemplate;
  isCelcius:boolean;
  color_c:string;
  color_f:string;
  showTemp:string;
  showTempFeel:string;
  value:string;
  constructor() { }

  ngOnInit() {
    this.isCelcius=true;
    this.value = this.data.value;
  }
  toggleTempScale(selector:string){
    console.log(selector);
    let convert=parseFloat(this.data.value);
    if(selector==='C'){
      this.isCelcius =true;
      this.value = this.data.value;      
    }else{
      this.isCelcius =false;
      this.value = this.data.otherValue;
    }
  }
}
