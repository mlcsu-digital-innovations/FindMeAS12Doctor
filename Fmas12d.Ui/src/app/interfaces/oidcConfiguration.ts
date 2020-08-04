import { OpenIdConfiguration } from 'angular-auth-oidc-client';

export interface OidcConfiguration extends OpenIdConfiguration {
  extraQueryParams?: any;
}
