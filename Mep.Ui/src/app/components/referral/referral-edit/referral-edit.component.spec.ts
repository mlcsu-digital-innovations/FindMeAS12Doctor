import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReferralEditComponent } from './referral-edit.component';
import { SharedComponentsModule } from '../../shared-components.module';

describe('ReferralEditComponent', () => {
  let component: ReferralEditComponent;
  let fixture: ComponentFixture<ReferralEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReferralEditComponent ],
      imports: [ SharedComponentsModule ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferralEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
