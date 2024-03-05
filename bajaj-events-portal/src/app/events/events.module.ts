import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import {SharedModule} from '../shared/shared.module';
import {NgxPaginationModule} from 'ngx-pagination';
import { RouterModule } from '@angular/router';

import { EventsListComponent } from './components/events-list/events-list.component';
import { EventDetailsComponent } from './components/event-details/event-details.component';

import { BajajEventsService } from './services/bajaj-events.service';
import { RegisterEventComponent } from './components/register-event/register-event.component';
import { EventsRoutingModule } from './events-routing.module';



@NgModule({
  declarations: [
    EventsListComponent,
    EventDetailsComponent,
    RegisterEventComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    NgxPaginationModule,
    HttpClientModule,
    RouterModule,
    ReactiveFormsModule,
    EventsRoutingModule
  ],
  exports:[
    EventsListComponent,
    RegisterEventComponent
  ],
  providers:[
    BajajEventsService
  ]
})
export class EventsModule { }
