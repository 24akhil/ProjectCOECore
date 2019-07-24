import { Injectable } from '@angular/core';
import { Place } from '../_models/place.model';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlaceService {

  subjectPlace = new Subject<Place>();
  constructor() { }

  //change the place
  sendPlace(message: Place) {
    this.subjectPlace.next( message );
    console.log("service place send"+message.latitude);
  }
  
  //announce change of place to sunscribers.
  getPlace(): Observable<Place> {
    console.log("service place get");
    return this.subjectPlace.asObservable();  
  }

}
