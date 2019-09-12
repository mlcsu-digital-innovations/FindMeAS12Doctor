import { TestBed } from '@angular/core/testing';

import { PatientSearchService } from './patient-search.service';
import { HttpClientModule } from '@angular/common/http';

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


