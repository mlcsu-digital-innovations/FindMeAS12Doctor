import { ActivatedRoute } from '@angular/router';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AssessmentCreateComponent } from './assessment-create.component';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { SharedComponentsModule } from '../../shared-components.module';
import { RouterService } from 'src/app/services/router/router.service';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { OidcSecurityServiceStub } from 'src/app/mocks/oidc-security-service.mock';

describe('AssessmentCreateComponent', () => {
  let component: AssessmentCreateComponent;
  let fixture: ComponentFixture<AssessmentCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AssessmentCreateComponent
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
    fixture = TestBed.createComponent(AssessmentCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
