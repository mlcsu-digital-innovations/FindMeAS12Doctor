import { LogService } from './log.service';
import { TestBed } from '@angular/core/testing';

describe('LogService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LogService = TestBed.get(LogService);
    expect(service).toBeTruthy();
  });
});
