import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PlaceService } from '../_services/place.service';
import { Subscription } from 'rxjs';
import { Place } from '../_models/place.model';
import { AppConstants } from '../AppConstants';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class WeatherapicallService {

  place:Place = new Place();

  constructor(private http: HttpClient,
     public datepipe: DatePipe
    ) {}

   votefavourite(provider : string,placename:string){

    var param = {      
      "ApiName": provider,
      "Location": placename,      
    };
    
    let options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
    
    const url = AppConstants.baseURL+"WeatherApiRating/Rating";      
    console.log("Voting"+param.ApiName+""+param.Location+"" +url);
    return this.http.post<any>(url,param,options)
    .subscribe(
      res=>{console.log(JSON.stringify(res));}
    ); 
   
   }

  //to get weather data for a place from different providers.
  getWeatherData(place : Place, provider :string) {     
  let date = new Date();   
  let latest_date =this.datepipe.transform(date, 'yyyy-MM-dd');
  const baseURL = AppConstants.baseURL+provider; 
  var param = {
    "Place": place.placeName,
    "lat": place.latitude.toString(),
    "lon":place.longitude.toString(),
    "RequestDate":{
      "date":latest_date
    }
  };

  //providing options with header
  let options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
    return this.http.post<any>(baseURL,param,options);      
    }
    
}
