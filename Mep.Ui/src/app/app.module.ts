import { AppComponent } from './app.component';
import { AppRoutes } from './app.routes';
import { BrowserModule } from '@angular/platform-browser';
import { DigitOnlyModule } from '@uiowa/digit-only';
import { DoctorModule } from './components/doctor/doctor.module';
import { ExaminationModule } from './components/examination/examination.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { PatientModule } from './components/patient/patient.module';
import { ReferralModule } from './components/referral/referral.module';
import { RouterModule } from '@angular/router';
import { RouterService } from './services/router/router.service';
import {
  AuthModule,
  OidcSecurityService,
  ConfigResult,
  OidcConfigService,
  OpenIdConfiguration
} from 'angular-auth-oidc-client';
import { AuthInterceptor } from './services/auth-interceptor';

export function loadConfig(oidcConfigService: OidcConfigService) {
  console.log('APP_INITIALIZER STARTING');
  // https://login.microsoftonline.com/damienbod.onmicrosoft.com/.well-known/openid-configuration
  // jwt keys: https://login.microsoftonline.com/common/discovery/keys
  // Azure AD does not support CORS, so you need to download the OIDC configuration, and use these from the application.
  // The jwt keys needs to be configured in the well-known-openid-configuration.json
  return () => oidcConfigService.load(`https://localhost:5001/api/config/configuration`);
  //return () => oidcConfigService.load_using_custom_stsServer('https://localhost:44347/well-known-openid-configuration.json');
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
    ExaminationModule,
    HttpClientModule,
    PatientModule,
    ReferralModule,
    RouterModule.forRoot(AppRoutes)
  ],
  providers: [
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
        stsServer: 'https://login.microsoftonline.com/f47807cf-afbc-4184-a579-8678bea3019a/',
        redirect_url: configResult.customConfig.redirect_url,
        client_id: configResult.customConfig.client_id,
        response_type: configResult.customConfig.response_type,
        scope: configResult.customConfig.scope,
        post_logout_redirect_uri: configResult.customConfig.post_logout_redirect_uri,
        start_checksession: configResult.customConfig.start_checksession,
        silent_renew: configResult.customConfig.silent_renew,
        silent_renew_url: 'http://localhost:44311/silent-renew.html',
        post_login_route: '/referral/list',
        forbidden_route: configResult.customConfig.forbidden_route,
        unauthorized_route: configResult.customConfig.unauthorized_route,
        log_console_warning_active: configResult.customConfig.log_console_warning_active,
        log_console_debug_active: configResult.customConfig.log_console_debug_active,
        max_id_token_iat_offset_allowed_in_seconds: configResult.customConfig.max_id_token_iat_offset_allowed_in_seconds,
        auto_userinfo: false,
        history_cleanup_off: true,
        iss_validation_off: true
        // disable_iat_offset_validation: true
      };

      this.oidcSecurityService.setupModule(config, configResult.authWellknownEndpoints);

      this.oidcSecurityService.setCustomRequestParameters(configResult.customConfig.additional_login_parameters);
    });

    console.log('APP STARTING');
  }
}
