
import { AppComponent } from './app.component';
import { AppRoutes } from './app.routes';
import { AssessmentModule } from './components/assessment/assessment.module';
import { AuthInterceptor } from './services/auth-interceptor';
import { AuthModule, OidcSecurityService, ConfigResult, OidcConfigService, OpenIdConfiguration }
  from 'angular-auth-oidc-client';
import { AuthorizationGuard } from './authorization.guard';
import { BrowserModule } from '@angular/platform-browser';
import { DigitOnlyModule } from '@uiowa/digit-only';
import { DoctorModule } from './components/doctor/doctor.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { PatientModule } from './components/patient/patient.module';
import { ReferralModule } from './components/referral/referral.module';
import { RouterModule } from '@angular/router';
import { RouterService } from './services/router/router.service';
import { environment } from 'src/environments/environment.prod';


export function loadConfig(oidcConfigService: OidcConfigService) {
  console.log('APP_INITIALIZER STARTING');
  // https://login.microsoftonline.com/damienbod.onmicrosoft.com/.well-known/openid-configuration
  // jwt keys: https://login.microsoftonline.com/common/discovery/keys
  // Azure AD does not support CORS, so you need to download the OIDC configuration, 
  // and use these from the application.
  // The jwt keys needs to be configured in the well-known-openid-configuration.json
  return () => oidcConfigService.load(`${environment.apiEndpoint}/config/configuration`);
}

@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent
  ],
  imports: [
    AuthModule.forRoot(),
    BrowserModule,
    DigitOnlyModule,
    DoctorModule,
    AssessmentModule,
    HttpClientModule,
    PatientModule,
    ReferralModule,
    RouterModule.forRoot(AppRoutes)
  ],
  providers: [
    AuthorizationGuard,
    OidcSecurityService,
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: loadConfig,
      deps: [OidcConfigService],
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

  constructor(
    private oidcSecurityService: OidcSecurityService,
    private oidcConfigService: OidcConfigService,
    private routerExtService: RouterService
  ) {
    this.oidcConfigService.onConfigurationLoaded.subscribe((configResult: ConfigResult) => {

      const config: OpenIdConfiguration = {
        auto_userinfo: false,
        client_id: "c898ea46-4e6e-4e55-b53b-8ae61c825507",
        forbidden_route: "/forbidden",
        history_cleanup_off: true,
        iss_validation_off: true,
        log_console_debug_active: !environment.production,
        log_console_warning_active: !environment.production,
        max_id_token_iat_offset_allowed_in_seconds: 1000,
        post_login_route: '/welcome',
        post_logout_redirect_uri: "https://www.digitalinnovationwm.nhs.uk/",
        redirect_url: environment.oidc_redirect_url,
        response_type: "id_token token",
        scope: "openid profile email https://graph.microsoft.com/User.Read",
        silent_renew: false,
        silent_renew_url: `${environment.oidc_redirect_url}silent-renew.html`,
        start_checksession: true,
        stsServer: 'https://login.microsoftonline.com/f47807cf-afbc-4184-a579-8678bea3019a/',
        trigger_authorization_result_event: true,
        unauthorized_route: '/unauthorized',
      };

      this.oidcSecurityService.setupModule(config, configResult.authWellknownEndpoints);

      this.oidcSecurityService.setCustomRequestParameters(
        {
          'resource': 'c898ea46-4e6e-4e55-b53b-8ae61c825507',
          'response_mode': 'fragment'
        }
      );

    });
  }
}
