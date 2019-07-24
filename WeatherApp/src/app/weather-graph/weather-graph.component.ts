import { Component, OnInit } from '@angular/core';
import {Chart} from 'chart.js';
import { Subscription } from 'rxjs';
import { PlaceService } from '../_services/place.service';
import { Place } from '../_models/place.model';
import { AppConstants } from '../AppConstants';

@Component({
  selector: 'app-weather-graph',
  templateUrl: './weather-graph.component.html',
  styleUrls: ['./weather-graph.component.scss']
})
export class WeatherGraphComponent implements OnInit {

  title = 'Ng7ChartJs By DotNet Techy';
  LineChart=[];
  BarChart=[];
  PieChart=[];
  placeSubscription : Subscription;

constructor(private placeService : PlaceService){
  this.placeSubscription = this.placeService.getPlace().subscribe((result : Place) => {
    if (result) {  
    this.refreshGraphData();
    } 
    });        
}
  ngOnInit()
  {
    this.refreshGraphData();
// Line chart:


// Bar chart:
// this.BarChart = new Chart('barChart', {
//   type: 'bar',
// data: {
//  labels: ["DarkSky", "Open Weather", "Apixu"],  
//  datasets: [{
//      label: 'Temperature',
//      data: [20,24,26],
//      backgroundColor: [
//          'rgba(255, 99, 132, 0.2)',
//          'rgba(54, 162, 235, 0.2)',
//          'rgba(255, 206, 86, 0.2)',
//      ],
//      borderColor: [
//          'rgba(255,99,132,1)',
//          'rgba(54, 162, 235, 1)',
//          'rgba(255, 206, 86, 1)',
//      ],
//      borderWidth: 1
//  }]
// }, 
// options: {
//  title:{
//      text:"",
//      display:true
//  },
//  scales: {
//      yAxes: [{
//          ticks: {
//              beginAtZero:true
//          }
//      }]
//  }
// }
// });

// pie chart:

  }
  refreshGraphData(){
    console.log("qweerwqr"+AppConstants.tempAPixuGraph);
   this.BarChart = new Chart('barChart', {
    type: 'bar',
  data: {
   labels: ["DarkSky", "Open Weather", "Apixu"],  
   datasets: [{
       label: 'Temperature',
       data:[AppConstants.tempDarkSkyGraph,AppConstants.tempOpenWeatherMapGraph,AppConstants.tempAPixuGraph],
       backgroundColor: [
           'rgba(255, 99, 132, 0.2)',
           'rgba(54, 162, 235, 0.2)',
           'rgba(255, 206, 86, 0.2)',
       ],
       borderColor: [
           'rgba(255,99,132,1)',
           'rgba(54, 162, 235, 1)',
           'rgba(255, 206, 86, 1)',
       ],
       borderWidth: 1
   }]
  }, 
  options: {
   title:{
       text:"",
       display:true
   },
   scales: {
       yAxes: [{
           ticks: {
               beginAtZero:true
           }
       }]
   }
  }
  });
  
   
  }
}
