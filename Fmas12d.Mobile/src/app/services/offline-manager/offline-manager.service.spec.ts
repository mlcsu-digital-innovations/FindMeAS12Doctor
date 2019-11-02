import { OfflineManagerService } from './offline-manager.service';
import { TestBed } from 'src/app/services/amhp-assessment/node_modules/@angular/core/testing';

describe('OfflineManagerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OfflineManagerService = TestBed.get(OfflineManagerService);
    expect(service).toBeTruthy();
  });
});
