import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DisableControlDirective } from '../directives/disable-control/disable-control.directive';
import { NavbarComponent } from '../navbar/navbar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastsComponent } from '../toasts/toasts.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ReferralCreateComponent } from './referral-create.component';

describe('ReferralCreateComponent', () => {
  let component: ReferralCreateComponent;
  let fixture: ComponentFixture<ReferralCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        DisableControlDirective,
        NavbarComponent,
        ReferralCreateComponent,
        ToastsComponent
    ],
      imports: [
        HttpClientModule,
        NgbModule,
        ReactiveFormsModule
      ]
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
