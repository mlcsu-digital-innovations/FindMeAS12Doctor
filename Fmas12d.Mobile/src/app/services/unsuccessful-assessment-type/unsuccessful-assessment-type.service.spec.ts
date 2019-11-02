import { TestBed } from 'src/app/services/amhp-assessment/node_modules/@angular/core/testing';
import { UnsuccessfulExaminationTypeService } from './unsuccessful-assessment-type.service';

describe('UnsuccessfulExaminationTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UnsuccessfulExaminationTypeService = TestBed.get(UnsuccessfulExaminationTypeService);
    expect(service).toBeTruthy();
  });
});
