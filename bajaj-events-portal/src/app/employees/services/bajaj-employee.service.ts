import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Employee } from '../models/employee';

@Injectable()
export class BajajEmployeeService {
  constructor(private _httpClient: HttpClient) {}
  private _serviceBaseUrl: string = 'https://localhost:7081/api';
  getAllEmployees(): Observable<Employee[]> {
    return this._httpClient.get<Employee[]>(`${this._serviceBaseUrl}/employees`);
  }
  getEmployeeDetails(employeeId: number): Observable<Employee> {
    return this._httpClient.get<Employee>(
      `${this._serviceBaseUrl}/employees/${employeeId}`
    );
  }
}
