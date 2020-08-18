// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { LogLevel, OpenIdConfiguration } from 'angular-auth-oidc-client';

export const environment = {
  apiEndpoint: 'https://localhost:5001',
  defaultAssessmentCompletedInHours: 3,
  locationEndpoint: 'https://www.google.com/maps/@52.9856552,-2.8707448,7z',
  oidc_redirect_url: 'http://localhost:4200/',
  production: false
};

export const oidcConfig: OpenIdConfiguration = {
  stsServer: 'https://login.microsoftonline.com/f47807cf-afbc-4184-a579-8678bea3019a/',
  redirectUrl: 'http://localhost:4200/',
  clientId: '9a667831-799d-4a8a-bce2-c168424cdabe',
  responseType: 'id_token token',
  scope: 'openid profile email https://graph.microsoft.com/User.Read',
  postLoginRoute: '/welcome',
  postLogoutRedirectUri: 'https://www.digitalinnovationwm.nhs.uk/',
  startCheckSession: true,
  silentRenew: true,
  silentRenewUrl: 'http://localhost:4200/silent-renew.html',
  forbiddenRoute: '/forbidden',
  unauthorizedRoute: '/unauthorized',
  logLevel: LogLevel.Debug,
  maxIdTokenIatOffsetAllowedInSeconds: 1000,
  customParams: {
    resource: '9a667831-799d-4a8a-bce2-c168424cdabe',
    response_mode: 'fragment'
  },
  autoUserinfo: false
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
import 'zone.js/dist/zone-error';  // Included with Angular CLI.import { LogLevel } from 'angular-auth-oidc-client';

