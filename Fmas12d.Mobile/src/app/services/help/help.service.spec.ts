import { HelpService } from './help.service';
import { TestBed } from 'src/app/services/amhp-assessment/node_modules/@angular/core/testing';

describe('HelpService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HelpService = TestBed.get(HelpService);
    expect(service).toBeTruthy();
  });
});
