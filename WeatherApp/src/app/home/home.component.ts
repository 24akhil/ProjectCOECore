import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from '../_models';
import {AuthenticationService } from '../_services';
import { PlaceService } from '../_services/place.service';
import { Place } from '../_models/place.model';
  
@Component({ 
    styleUrls: ['./home.component.scss'],
    selector: 'app-home',
    templateUrl: './home.component.html'
 })
export class HomeComponent implements OnInit, OnDestroy {
  
    currentUser: User;
    currentUserSubscription: Subscription;
    placeSubscription : Subscription;
    users: User[] = [];
    lat: number = 22.7196;
    lng: number = 75.8577;
    place:Place = new Place();
    

    constructor(
        private authenticationService: AuthenticationService,
        private placeService : PlaceService) {

        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });
        this.placeSubscription = this.placeService.getPlace().subscribe((message : Place) => {
            if (message) {                       
            this.alterMapLocation(message.latitude,message.longitude);
            } 
          });
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.currentUserSubscription.unsubscribe();
        this.placeSubscription.unsubscribe();
    }

    //changing user selected city
    private alterMapLocation(lat:number,lng:number){
        console.log("Hello");
        this.lat =lat;
        this.lng =lng;
    }
   
}