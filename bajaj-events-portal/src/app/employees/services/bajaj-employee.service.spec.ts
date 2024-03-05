import { TestBed } from '@angular/core/testing';

import { BajajEmployeeService } from './bajaj-employee.service';

describe('BajajEmployeeService', () => {
  let service: BajajEmployeeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BajajEmployeeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
