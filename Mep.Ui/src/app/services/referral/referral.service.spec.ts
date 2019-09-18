import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { ReferralService } from './referral.service';

describe('ReferralService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [],
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: ReferralService = TestBed.get(ReferralService);
    expect(service).toBeTruthy();
  });
});
