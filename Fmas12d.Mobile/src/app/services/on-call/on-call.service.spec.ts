import { OnCallService } from './on-call.service';
import { TestBed } from '@angular/core/testing';

describe('OnCallService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OnCallService = TestBed.get(OnCallService);
    expect(service).toBeTruthy();
  });
});
