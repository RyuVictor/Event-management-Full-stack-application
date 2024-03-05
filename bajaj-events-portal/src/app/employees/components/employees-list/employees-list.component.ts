import { Component, OnInit, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { Employee } from '../../models/employee';

import { BajajEmployeeService } from '../../services/bajaj-employee.service';

@Component({
  selector: 'bajaj-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrl: './employees-list.component.css',
})
export class EmployeesListComponent implements OnInit,OnDestroy {
  constructor(private _bajajEmployeeService: BajajEmployeeService) {}
  title: string = 'Welcome to Bajaj Finserv Employees List!';
  subtitle: string = 'Published by Bajaj Finserv HR team';
  selectedEmployeeId: number;
  totalItemsPerPage: number = 2;
  currentPage = 1;
  searchEmployeeCharacters: string = '';
  employees: Employee[];
  _employeeServiceSubscription : Subscription;
  sortedColumn: keyof Employee | undefined;
  isAscending: boolean = true
  onEmployeeSelection(employeeId: number): void {
    console.log("Button Pressed");
    
     this.selectedEmployeeId = employeeId;
     console.log(`Id selected : ${this.selectedEmployeeId}`);
     
  }
  filterEventsByName(): Employee[] {
    return this.employees.filter((event) =>
      event.employeeName
        .toLocaleLowerCase()
        .includes(this.searchEmployeeCharacters.toLocaleLowerCase())
    );
  }
  changePageNumber(): void {
    this.currentPage = 1;
  }
  ngOnInit(): void {
    this._employeeServiceSubscription  = this._bajajEmployeeService.getAllEmployees().subscribe({
      next : data => this.employees = data,
      error : err => console.log(err)      
    })
  }
  ngOnDestroy(): void {
    if(this._employeeServiceSubscription) this._employeeServiceSubscription.unsubscribe();
  }
  sortTable(column: keyof Employee) {
    if (this.sortedColumn === column) {
      this.isAscending = !this.isAscending;
    } else {
      this.sortedColumn = column;
      this.isAscending = true;
    }
    this.employees.sort((a, b) => {
      const aValue = this.sortedColumn ? a[this.sortedColumn] : undefined;
      const bValue = this.sortedColumn ? b[this.sortedColumn] : undefined;

      if (aValue !== undefined && bValue !== undefined) {
        if (typeof aValue === 'string' && typeof bValue === 'string') {
          return this.isAscending ? aValue.localeCompare(bValue) : bValue.localeCompare(aValue);
        }
        const aValueNumeric = Number(aValue);
        const bValueNumeric = Number(bValue);

        if (!isNaN(aValueNumeric) && !isNaN(bValueNumeric)) {
          return this.isAscending ? aValueNumeric - bValueNumeric : bValueNumeric - aValueNumeric;
        }
      }

      return 0;
    });
  }
}
