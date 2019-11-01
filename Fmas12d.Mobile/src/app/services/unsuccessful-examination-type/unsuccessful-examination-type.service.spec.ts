import { TestBed } from '@angular/core/testing';
import { UnsuccessfulExaminationTypeService } from './unsuccessful-examination-type.service';

describe('UnsuccessfulExaminationTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UnsuccessfulExaminationTypeService = TestBed.get(UnsuccessfulExaminationTypeService);
    expect(service).toBeTruthy();
  });
});
