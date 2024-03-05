import { Component, Input, OnChanges, SimpleChanges, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { Employee } from '../../models/employee';

import { BajajEmployeeService } from '../../services/bajaj-employee.service';

@Component({
  selector: 'bajaj-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrl: './employee-details.component.css'
})
export class EmployeeDetailsComponent implements OnChanges,OnDestroy {
  constructor(private _bajajEmployeeService : BajajEmployeeService){}
  
  title:string = "Details of Employee - "
  @Input() employeeId : number;
  employee : Employee;

  private _employeeServiceSubscription : Subscription;
  ngOnChanges(changes: SimpleChanges): void {
    console.log("Changed");
    
    this._employeeServiceSubscription = this._bajajEmployeeService.getEmployeeDetails(this.employeeId).subscribe({
      next:data=>this.employee = data,
      error:err=>console.log(err)  
    })
  }
  ngOnDestroy(): void {
    if(this._employeeServiceSubscription) this._employeeServiceSubscription.unsubscribe();
  }
}
