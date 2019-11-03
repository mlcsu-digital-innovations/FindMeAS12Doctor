import { AmhpAssessmentService } from './amhp-assessment.service';
import { TestBed } from '@angular/core/testing';

describe('AmhpAssessmentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AmhpAssessmentService = TestBed.get(AmhpAssessmentService);
    expect(service).toBeTruthy();
  });
});
