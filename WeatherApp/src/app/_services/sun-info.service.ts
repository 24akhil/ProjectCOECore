import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {  Subject, Subscription } from 'rxjs';
import { DatePipe } from '@angular/common'
import { Place } from '../_models/place.model';
import { PlaceService } from './place.service';

export class DayDetail{
sunrise: Date;
sunset : Date;
daylength : number;
solarnoon : Date;
}
@Injectable({
  providedIn: 'root'
})
export class SunInfoService implements OnInit {
  ngOnInit(): void {
    this.sunRiseSetDetails();
  }
  
  date : Date;
  dayDetail : DayDetail;
  subjectdayDetail = new Subject<DayDetail>();
  place :Place = new Place();
  placeSubscription : Subscription;

  constructor(private http: HttpClient,
              public datepipe: DatePipe,              
              private placeService : PlaceService) { 
  this.placeSubscription = this.placeService.getPlace().subscribe((result : Place) => {
    if (result) {
    this.place =result;      
    } 
    });
    
    this.place.latitude =22;
    this.place.longitude =77;
    this.place.placeName = "Indore";
              }


  sunRiseSetDetails() {
    this.date = new Date();
    let latest_date =this.datepipe.transform(this.date, 'dd-MM-yyyy');

    var param = {
      "Place": this.place.placeName,
      "lat": this.place.latitude.toString(),
      "lon":this.place.longitude.toString(),
      "RequestDate":{
        "date":latest_date
      }};
    
    const url = "http://localhost:5000/api/SunRiseSunSet?date=07-09-19";

 
    return this.http.post<any>(url, { }).subscribe((res:any)=>{      
      this.parseData(res);   
    });      
    }

    //parsing date for desired format
    parseData(res: any){
      this.dayDetail = new DayDetail();
      this.dayDetail.daylength = res["results"]["day_length"];
      this.dayDetail.sunrise = res["results"]["sunrise"];
      this.dayDetail.sunset =res["results"]["sunset"];
      this.dayDetail.solarnoon = res["results"]["solar_noon"];      
      this.subjectdayDetail.next(this.dayDetail);
    }
    
}
