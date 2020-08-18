import { AppComponent } from './app.component';
import { AppRoutes } from './app.routes';
import { AssessmentModule } from './components/assessment/assessment.module';
import { AuthInterceptor } from './services/auth-interceptor';
import { AuthModule, OidcConfigService, OidcSecurityService } from 'angular-auth-oidc-client';
import { AuthorizationGuard } from './authorization.guard';
import { BrowserModule } from '@angular/platform-browser';
import { DigitOnlyModule } from '@uiowa/digit-only';
import { DoctorModule } from './components/doctor/doctor.module';
import { FinanceModule } from './components/finance/finance.module';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { map, switchMap } from 'rxjs/operators';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { oidcConfig } from 'src/environments/environment';
import { OnCallDoctorModule } from './components/on-call-doctor/on-call-doctor.module';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { PatientModule } from './components/patient/patient.module';
import { ReferralModule } from './components/referral/referral.module';
import { RouterModule } from '@angular/router';
import { SecurityService } from './services/security/security.service';
import { UserProfileModule } from './components/user-profile/user-profile.module';
import { NoCacheHeadersInterceptor } from './services/no-cache-headers-interceptor';

export function loadConfig(oidcConfigService: OidcConfigService, httpClient: HttpClient) {

  const setupAction$ =
    httpClient.get<any>(`${oidcConfig.stsServer}/.well-known/openid-configuration`)
      .pipe(
        map(() => {
        return oidcConfig;
    }),
      switchMap((config) => oidcConfigService.withConfig(config))
    );

  return () => setupAction$.toPromise();
}

@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent
  ],
  imports: [
    AuthModule.forRoot(),
    AssessmentModule,
    BrowserModule,
    DigitOnlyModule,
    DoctorModule,
    FinanceModule,
    HttpClientModule,
    OnCallDoctorModule,
    PatientModule,
    ReferralModule,
    RouterModule.forRoot(AppRoutes),
    UserProfileModule
  ],
  providers: [
    AuthorizationGuard,
    OidcSecurityService,
    SecurityService,
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: loadConfig,
      deps: [OidcConfigService, HttpClient],
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: NoCacheHeadersInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
