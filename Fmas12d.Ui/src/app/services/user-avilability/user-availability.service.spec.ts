import { TestBed } from '@angular/core/testing';
import { UserAvailabilityService } from './user-availability.service';

describe('UserAvailabilityService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserAvailabilityService = TestBed.get(UserAvailabilityService);
    expect(service).toBeTruthy();
  });
});
