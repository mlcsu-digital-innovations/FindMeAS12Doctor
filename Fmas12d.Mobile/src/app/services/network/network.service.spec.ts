import { NetworkService } from './network.service';
import { TestBed } from 'src/app/services/amhp-assessment/node_modules/@angular/core/testing';

describe('NetworkService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NetworkService = TestBed.get(NetworkService);
    expect(service).toBeTruthy();
  });
});
