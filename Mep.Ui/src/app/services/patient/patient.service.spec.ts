import { HttpClientModule } from '@angular/common/http';
import { PatientService } from './patient.service';
import { TestBed } from '@angular/core/testing';

describe('PatientService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: PatientService = TestBed.get(PatientService);
    expect(service).toBeTruthy();
  });
});
