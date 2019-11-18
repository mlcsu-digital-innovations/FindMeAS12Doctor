import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Component } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReferralListComponent } from './referral-list.component';
import { ReferralListService } from 'src/app/services/referral-list/referral-list-service';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedComponentsModule } from '../../shared-components.module';
import { ToastService } from 'src/app/services/toast/toast.service';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { OidcSecurityServiceStub } from 'src/app/mocks/oidc-security-service.mock';
import { of } from 'rxjs';

@Component({
  template: ''
})
class DummyComponent {
}

describe('ReferralListComponent', () => {
  let component: ReferralListComponent;
  let fixture: ComponentFixture<ReferralListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        DummyComponent,
        ReferralListComponent
      ],
      imports: [
        HttpClientModule,
        RouterTestingModule.withRoutes([
          {path: 'patient/edit/:patientId', component: DummyComponent}
        ]),
        SharedComponentsModule
      ],
      providers: [
        DecimalPipe,
        ReferralListService,
        ToastService,
        {
          provide: OidcSecurityService,
          useClass: OidcSecurityServiceStub
        },
        {
          provide: ReferralListService,
          useValue: {
            referralList$: of({})
          }
        }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferralListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
