import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models';
import { AppConstants } from '../AppConstants';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    //to get current user
    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    //to loging user in
    login(Email: string, Password: string) {
        const url = AppConstants.baseURL+"WeatherAppUser/login"
        var user = {
            "Email": Email,
            "Password": Password,
            "FirstName": "",
            "LastName": ""
          }
          let options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
         
          return this.http.post<any>(url, user ,options)
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.Token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    this.currentUserSubject.next(user);
                }
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}