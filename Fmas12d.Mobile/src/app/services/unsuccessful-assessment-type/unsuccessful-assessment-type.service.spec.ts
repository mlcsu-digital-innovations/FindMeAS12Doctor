import { TestBed } from 'src/app/services/amhp-assessment/node_modules/@angular/core/testing';
import { UnsuccessfulAssessmentTypeService } from './unsuccessful-assessment-type.service';

describe('UnsuccessfulAssessmentTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UnsuccessfulAssessmentTypeService = TestBed.get(UnsuccessfulAssessmentTypeService);
    expect(service).toBeTruthy();
  });
});
