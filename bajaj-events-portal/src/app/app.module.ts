import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { HomeModule } from './home/home.module';
import {EventsModule} from "./events/events.module";
import { EmployeesModule } from './employees/employees.module';
import { NavigationModule } from './navigation/navigation.module';
import { AppRoutingModule } from './app-routing.module';
import { SecurityModule } from './security/security.module';

import { AppComponent } from './app.component';
import { HttpTokenInterceptor } from './interceptors/http-token.interceptor';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    EventsModule,
    EmployeesModule,
    HomeModule,
    NavigationModule,
    SecurityModule

    
  ],
  providers:[
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpTokenInterceptor,
      multi:true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }