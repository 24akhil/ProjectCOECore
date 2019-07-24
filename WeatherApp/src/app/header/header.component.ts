import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../_models';
import { Router } from '@angular/router';
import { AuthenticationService } from '../_services';
import { GooglePlaceDirective } from 'ngx-google-places-autocomplete/ngx-google-places-autocomplete.directive';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { Place } from '../_models/place.model';
import { PlaceService } from '../_services/place.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  
  currentUser: User;
  place : Place = new Place();
  options :any;

  constructor(private router: Router,
              private authenticationService: AuthenticationService,
              private placeService : PlaceService){
      this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
                }

  ngOnInit() {    
  }

  @ViewChild("placesRef") placesRef : GooglePlaceDirective;
    
  // responding for address change from google api
  public handleAddressChange(address: Address) {       
        this.place.latitude = address.geometry.location.lat();
        this.place.longitude = address.geometry.location.lng();
        this.place.placeName = address.name;       
        this.placeService.sendPlace(this.place);
    }

  //logging  user out
  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }    
}
