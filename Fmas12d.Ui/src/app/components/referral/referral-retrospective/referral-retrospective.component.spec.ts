import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DecimalPipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { OidcSecurityServiceStub } from 'src/app/mocks/oidc-security-service.mock';
import { ReferralRetrospectiveComponent } from './referral-retrospective.component';
import { RouterService } from 'src/app/services/router/router.service';
import { SharedComponentsModule } from '../../shared-components.module';
import { ToastService } from 'src/app/services/toast/toast.service';



describe('ReferralRetrospectiveComponent', () => {
  let component: ReferralRetrospectiveComponent;
  let fixture: ComponentFixture<ReferralRetrospectiveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        ReferralRetrospectiveComponent
      ],
      imports: [
        HttpClientModule,
        SharedComponentsModule
      ],
      providers: [
        DecimalPipe,
        ToastService,
        {
          provide: RouterService,
          useValue: {
            paramMap: of({referralId: 1})
          }
        },
        {
          provide: OidcSecurityService,
          useClass: OidcSecurityServiceStub
        },
      ]
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
