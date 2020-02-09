import { SpecialitiesService } from './specialities.service';
import { TestBed } from '@angular/core/testing';

describe('SpecialitiesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SpecialitiesService = TestBed.get(SpecialitiesService);
    expect(service).toBeTruthy();
  });
});
