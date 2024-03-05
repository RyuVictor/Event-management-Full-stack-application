import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventsHomeComponent } from './components/events-home/events-home.component';



@NgModule({
  declarations: [
    EventsHomeComponent
  ],
  imports: [
    CommonModule
  ],
  exports:[
    EventsHomeComponent
  ]
})
export class HomeModule { }
