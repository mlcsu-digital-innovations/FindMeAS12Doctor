import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DoctorAcceptComponent } from './doctor-accept.component';
import { SharedComponentsModule } from '../../shared-components.module';
import { RouterService } from 'src/app/services/router/router.service';
import { of } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { OidcSecurityServiceStub } from 'src/app/mocks/oidc-security-service.mock';

describe('DoctorAcceptComponent', () => {
  let component: DoctorAcceptComponent;
  let fixture: ComponentFixture<DoctorAcceptComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations:
      [
        DoctorAcceptComponent
      ],
      imports: [
        SharedComponentsModule
      ],
      providers: [
        {
          provide: OidcSecurityService,
          useClass: OidcSecurityServiceStub
        },
        {
          provide: RouterService,
          useValue: {
            paramMap: of({referralId: 1})
          }
        }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorAcceptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy();
  });
});
