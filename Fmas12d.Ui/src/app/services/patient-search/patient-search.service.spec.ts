import { HttpClientModule } from '@angular/common/http';
import { PatientSearchService } from './patient-search.service';
import { TestBed } from '@angular/core/testing';

describe('PatientSearchService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: PatientSearchService = TestBed.get(PatientSearchService);
    expect(service).toBeTruthy();
  });
});


