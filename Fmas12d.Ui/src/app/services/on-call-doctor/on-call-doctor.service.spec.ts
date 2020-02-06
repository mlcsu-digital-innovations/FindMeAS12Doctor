import { OnCallDoctorService } from './on-call-doctor.service';
import { TestBed } from '@angular/core/testing';

describe('OnCallDoctorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OnCallDoctorService = TestBed.get(OnCallDoctorService);
    expect(service).toBeTruthy();
  });
});
