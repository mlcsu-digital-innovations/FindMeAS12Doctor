import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { ReferralCreateComponent } from './referral-create.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { DisableControlDirective } from '../directives/disable-control.directive';

describe('ReferralCreateComponent', () => {
  let component: ReferralCreateComponent;
  let fixture: ComponentFixture<ReferralCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReferralCreateComponent, NavbarComponent, DisableControlDirective ],
      imports: [ReactiveFormsModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferralCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
