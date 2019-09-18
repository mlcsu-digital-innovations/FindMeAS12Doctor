import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { PatientService } from './patient.service';

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
