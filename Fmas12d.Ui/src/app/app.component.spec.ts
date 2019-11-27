import { AppComponent } from './app.component';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { OidcSecurityServiceStub } from './mocks/oidc-security-service.mock';
import { RouterTestingModule } from '@angular/router/testing';
import { TestBed, async } from '@angular/core/testing';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        AppComponent
      ],
      providers: [
        {
          provide: OidcSecurityService,
          useClass: OidcSecurityServiceStub
        }
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Fmas12d'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('Fmas12d');
  });
});
