import { HttpClientModule } from '@angular/common/http';
import { ReferralListService } from './referral-list-service';
import { TestBed } from '@angular/core/testing';
import { ReferralModule } from 'src/app/components/referral/referral.module';

describe('ReferralListService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    declarations: [
    ],
    imports: [
      HttpClientModule,
      ReferralModule
    ],
    providers: [
      ReferralListService
    ]
  }));

  it('should be created', () => {
    const service: ReferralListService = TestBed.get(ReferralListService);
    expect(service).toBeTruthy();
  });
});
