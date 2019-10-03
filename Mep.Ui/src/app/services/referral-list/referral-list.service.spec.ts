import { ReferralListService } from './referral-list-service';
import { TestBed } from '@angular/core/testing';

describe('ReferralService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ReferralListService = TestBed.get(ReferralListService);
    expect(service).toBeTruthy();
  });
});