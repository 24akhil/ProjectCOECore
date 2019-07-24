import { BrowserModule } from '@angular/platform-browser';
import { NgModule,NO_ERRORS_SCHEMA } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor, ErrorInterceptor, fakeBackendProvider } from './_helpers';
import { AlertComponent } from './_components';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { AgmCoreModule } from '@agm/core';
import { GooglePlaceModule } from "ngx-google-places-autocomplete";
import { FooterComponent } from './footer/footer.component';
import { SunInfoComponent } from './sun-info/sun-info.component';
import { DatePipe } from '@angular/common';
import { WeatheruiComponent } from './weatherui/weatherui.component';
import { CurrentWeatherComponent } from './weatherui/current-weather/current-weather.component';
import { CurrentWeatherCardComponent } from './weatherui/current-weather/current-weather-card/current-weather-card.component';
import { FlexLayoutModule } from "@angular/flex-layout";
import { WeatherGraphComponent } from './weather-graph/weather-graph.component';

@NgModule({
  declarations: [
    AppComponent,
    AlertComponent,
    HeaderComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    FooterComponent,
    SunInfoComponent,
    WeatheruiComponent,
    CurrentWeatherComponent,   
    CurrentWeatherCardComponent, WeatherGraphComponent     
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,    
    MDBBootstrapModule.forRoot(),   
    FormsModule,
    ReactiveFormsModule,   
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,    
    GooglePlaceModule,
    FlexLayoutModule, 
    AgmCoreModule.forRoot({
      //apiKey :'AIzaSyAEc9LOeHV0gKMVK3XVmwVSUlv3cGurjME'
     // apiKey:'AIzaSyAvcDy5ZYc2ujCS6TTtI3RYX5QmuoV8Ffw'
       apiKey:'AIzaSyCLP3xkkv62Pk_D1Z8VJe55uaCCaCb2wsY'
      //apiKey :'AIzaSyBu-916DdpKAjTmJNIgngS6HL_kDIKU0aU'
      //apiKey:'AIzaSyCOcV4ao0b9KtPjlWvX4PyA2D05BrvTMeU'      
    })
    // routing
  ],
  providers: [DatePipe,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },],
  bootstrap: [AppComponent]
})
export class AppModule {
   
 }
