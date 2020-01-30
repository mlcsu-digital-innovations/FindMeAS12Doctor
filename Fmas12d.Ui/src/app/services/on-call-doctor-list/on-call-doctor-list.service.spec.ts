import { OnCallDoctorListService } from './on-call-doctor-list.service';
import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { OnCallDoctorModule } from 'src/app/components/on-call-doctor/on-call-doctor.module';

describe('OnCallDoctorListService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [
    ],
    imports: [
      HttpClientModule,
      OnCallDoctorModule
    ],
    providers: [
      OnCallDoctorListService
    ]
  }));

  it('should be created', () => {
    const service: OnCallDoctorListService = TestBed.get(OnCallDoctorListService);
    expect(service).toBeTruthy();
  });
});
