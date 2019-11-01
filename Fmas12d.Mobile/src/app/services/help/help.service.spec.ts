import { HelpService } from './help.service';
import { TestBed } from '@angular/core/testing';

describe('HelpService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HelpService = TestBed.get(HelpService);
    expect(service).toBeTruthy();
  });
});
