import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import {SharedModule} from '../shared/shared.module';
import {NgxPaginationModule} from 'ngx-pagination';

import { EmployeesListComponent } from './components/employees-list/employees-list.component';
import { EmployeeDetailsComponent } from './components/employee-details/employee-details.component';

import {BajajEmployeeService} from './services/bajaj-employee.service';


@NgModule({
  declarations: [
    EmployeesListComponent,
    EmployeeDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    NgxPaginationModule,
    HttpClientModule
  ],
  exports:[
    EmployeesListComponent
  ],
  providers:[
    BajajEmployeeService
  ]
})
export class EmployeesModule { }
