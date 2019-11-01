import { OfflineManagerService } from './offline-manager.service';
import { TestBed } from '@angular/core/testing';

describe('OfflineManagerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OfflineManagerService = TestBed.get(OfflineManagerService);
    expect(service).toBeTruthy();
  });
});
