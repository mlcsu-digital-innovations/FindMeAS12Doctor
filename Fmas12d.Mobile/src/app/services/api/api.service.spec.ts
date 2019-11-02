import { ApiService } from './api.service';
import { TestBed } from 'src/app/services/amhp-assessment/node_modules/@angular/core/testing';

describe('ApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApiService = TestBed.get(ApiService);
    expect(service).toBeTruthy();
  });
});
