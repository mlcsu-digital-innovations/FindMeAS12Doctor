
import { AssessmentListComponent } from './assessment-list.component';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { OidcSecurityServiceStub } from 'src/app/mocks/oidc-security-service.mock';
import { SharedComponentsModule } from '../../shared-components.module';

describe('AssessmentListComponent', () => {
  let component: AssessmentListComponent;
  let fixture: ComponentFixture<AssessmentListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssessmentListComponent ],
      imports: [ SharedComponentsModule ],
      providers: [
        {
          provide: OidcSecurityService,
          useClass: OidcSecurityServiceStub
        }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssessmentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
