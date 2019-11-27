import { ActivatedRoute } from '@angular/router';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DoctorSelectComponent } from './doctor-select.component';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { OidcSecurityServiceStub } from 'src/app/mocks/oidc-security-service.mock';
import { SharedComponentsModule } from '../../shared-components.module';
import { RouterService } from 'src/app/services/router/router.service';

describe('DoctorSelectComponent', () => {
  let component: DoctorSelectComponent;
  let fixture: ComponentFixture<DoctorSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        DoctorSelectComponent
      ],
      imports: [
        HttpClientModule,
        SharedComponentsModule
      ],
      providers: [
        {
          provide: OidcSecurityService,
          useClass: OidcSecurityServiceStub
        },
        {
          provide: ActivatedRoute,
          useValue: {
            paramMap: of({referralId: 1})
          }
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
    fixture = TestBed.createComponent(DoctorSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
