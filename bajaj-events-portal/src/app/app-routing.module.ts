import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";

import { EventsHomeComponent } from './home/components/events-home/events-home.component';
import { LoginComponent } from './security/components/login/login.component';

import { EmployeesListComponent } from './employees/components/employees-list/employees-list.component';

const routes:Routes = [
  {
    path:'',
    component: EventsHomeComponent
  },
  {
    path:'home',
    component: EventsHomeComponent
  },
  {
    path:'employees',
    component: EmployeesListComponent
  },
  {
    path:'events',
    loadChildren: () => import("./events/events.module").then(m=>m.EventsModule)
  },
  {
    path:'login',
    component: LoginComponent
  }

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class AppRoutingModule {
  
 }


// import { NgModule } from '@angular/core';
// import { Routes, RouterModule, } from '@angular/router';

// import { EventsHomeComponent } from './home/components/events-home/events-home.component';
// import { EmployeesListComponent } from './employees/components/employees-list/employees-list.component';
// // import { EventsListComponent } from './events/components/events-list/events-list.component';
// import { LoginComponent } from './security/components/login/login.component';
// // import { EventDetailsComponent } from './events/components/event-details/event-details.component';
// // import { RegisterEventComponent } from './events/components/register-event/register-event.component';

// import { tokenGuard } from './guards/token.guard';

// const routes : Routes = [
//   {
//     path : '',
//     component : EventsHomeComponent 
//   },
//   {
//     path : 'home',
//     component : EventsHomeComponent
//   },
//   {
//     path : 'employees',
//     component : EmployeesListComponent,
//     canActivate : [tokenGuard]
//   },
//   {
//     path : 'events',
//     loadChildren:()=>import('./events/events.module').then(m=>m.EventsModule)
//   },
//   // {
//   //   path : 'events',
//   //   component : EventsListComponent,
//   //   canActivate : [tokenGuard]
//   // },
//   // {
//   //   path : 'events/new',
//   //   component : RegisterEventComponent,
//   //   canActivate : [tokenGuard]
//   // },
//   // {
//   //   path : 'events/:eventId',
//   //   component : EventDetailsComponent,
//   //   canActivate : [tokenGuard]
//   // },
//   {
//     path : 'login',
//     component : LoginComponent
//   }
// ];

// @NgModule({
//   imports: [
//     RouterModule.forRoot(routes)
//   ],
//   exports:[
//     RouterModule
//   ]
// })
// export class AppRoutingModule {}
