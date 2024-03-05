import { TestBed } from '@angular/core/testing';

import { BajajEventsService } from './bajaj-events.service';

describe('BajajEventsService', () => {
  let service: BajajEventsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BajajEventsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
