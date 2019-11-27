import { ActivatedRoute } from '@angular/router';
import { AssessmentEditComponent } from './assessment-edit.component';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { OidcSecurityServiceStub } from 'src/app/mocks/oidc-security-service.mock';
import { RouterService } from 'src/app/services/router/router.service';
import { SharedComponentsModule } from '../../shared-components.module';

describe('AssessmentEditComponent', () => {
  let component: AssessmentEditComponent;
  let fixture: ComponentFixture<AssessmentEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AssessmentEditComponent
      ],
      imports: [
        HttpClientModule,
        SharedComponentsModule
      ],
      providers: [
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
    fixture = TestBed.createComponent(AssessmentEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
