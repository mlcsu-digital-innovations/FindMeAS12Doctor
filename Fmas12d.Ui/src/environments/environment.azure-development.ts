// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  apiEndpoint: 'https://fmas12d-api-dev.azurewebsites.net/api',
  defaultAssessmentCompletedInHours: 3,
  oidc_redirect_url: "https://fmas12d-dev.azurewebsites.net/",
  locationEndpoint: 'https://www.google.com/maps/@52.9856552,-2.8707448,7z',
  production: false,
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
import 'zone.js/dist/zone-error';  // Included with Angular CLI.
