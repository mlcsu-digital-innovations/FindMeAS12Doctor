import { LogService } from './log.service';
import { TestBed } from 'src/app/services/amhp-assessment/node_modules/@angular/core/testing';

describe('LogService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LogService = TestBed.get(LogService);
    expect(service).toBeTruthy();
  });
});
