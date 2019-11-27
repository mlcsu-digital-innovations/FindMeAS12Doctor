import { ActivatedRoute } from '@angular/router';
import { AssessmentViewComponent } from './assessment-view.component';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { OidcSecurityServiceStub } from 'src/app/mocks/oidc-security-service.mock';
import { RouterService } from 'src/app/services/router/router.service';
import { SharedComponentsModule } from '../../shared-components.module';

describe('AssessmentViewComponent', () => {
  let component: AssessmentViewComponent;
  let fixture: ComponentFixture<AssessmentViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AssessmentViewComponent
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
    fixture = TestBed.createComponent(AssessmentViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
