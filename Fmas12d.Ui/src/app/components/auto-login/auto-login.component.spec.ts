import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AutoLoginComponent } from './auto-login.component';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { OidcSecurityServiceStub } from 'src/app/mocks/oidc-security-service.mock';

describe('AutoLoginComponent', () => {
  let component: AutoLoginComponent;
  let fixture: ComponentFixture<AutoLoginComponent>;

  const securityStub = new OidcSecurityServiceStub();

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutoLoginComponent ],
      providers: [
        {
          provide: OidcSecurityService,
          useValue: securityStub
        }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutoLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
