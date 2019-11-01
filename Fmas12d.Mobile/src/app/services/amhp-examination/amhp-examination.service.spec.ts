import { AmhpExaminationService } from './amhp-examination.service';
import { TestBed } from '@angular/core/testing';

describe('AmhpExaminationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AmhpExaminationService = TestBed.get(AmhpExaminationService);
    expect(service).toBeTruthy();
  });
});
