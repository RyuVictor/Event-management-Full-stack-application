import { NgModule } from '@angular/core';
import { Routes, RouterModule } from "@angular/router";

import { EventsListComponent } from './components/events-list/events-list.component';
import { RegisterEventComponent } from './components/register-event/register-event.component';
import { EventDetailsComponent } from './components/event-details/event-details.component';

import { tokenGuard } from '../guards/token.guard';

const eventRoutes: Routes = [
  {
    path: '',
    component: EventsListComponent,
    canActivate: [tokenGuard]
  },
  {
    path: 'new',
    component: RegisterEventComponent,
    canActivate: [tokenGuard]
  },
  {
    path: ':eventId',
    component: EventDetailsComponent,
    canActivate: [tokenGuard]
  },
];

@NgModule({
  imports: [
    RouterModule.forChild(eventRoutes)
  ],
  exports:[
    RouterModule
  ]
})
export class EventsRoutingModule { }