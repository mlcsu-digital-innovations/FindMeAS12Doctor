import { OnCallDoctorListService } from './on-call-doctor-list.service';
import { TestBed } from '@angular/core/testing';

describe('OnCallDoctorListService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OnCallDoctorListService = TestBed.get(OnCallDoctorListService);
    expect(service).toBeTruthy();
  });
});
