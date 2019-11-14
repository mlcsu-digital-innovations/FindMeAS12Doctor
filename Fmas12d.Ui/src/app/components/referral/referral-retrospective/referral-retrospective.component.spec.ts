import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferralRetrospectiveComponent } from './referral-retrospective.component';

describe('ReferralRetrospectiveComponent', () => {
  let component: ReferralRetrospectiveComponent;
  let fixture: ComponentFixture<ReferralRetrospectiveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReferralRetrospectiveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferralRetrospectiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
