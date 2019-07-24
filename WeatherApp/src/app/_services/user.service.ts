import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models';
import { AppConstants } from '../AppConstants';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

   
    //to register the user,
    register(user: User) {
        const url = AppConstants.baseURL+"WeatherAppUser/SignUp";
        var param = {            
                "Email": user.UserName,
                "Password": user.Password,
                "FirstName": user.FirstName,
                "LastName": user.LastName              
        }
        return this.http.post(url,param);
    }
}