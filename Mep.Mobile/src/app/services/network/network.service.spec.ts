import { NetworkService } from './network.service';
import { TestBed } from '@angular/core/testing';

describe('NetworkService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NetworkService = TestBed.get(NetworkService);
    expect(service).toBeTruthy();
  });
});
