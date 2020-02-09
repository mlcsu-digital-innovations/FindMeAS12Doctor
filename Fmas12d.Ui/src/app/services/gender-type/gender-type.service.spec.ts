import { GenderTypeService } from './gender-type.service';
import { TestBed } from '@angular/core/testing';

describe('GenderTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GenderTypeService = TestBed.get(GenderTypeService);
    expect(service).toBeTruthy();
  });
});
