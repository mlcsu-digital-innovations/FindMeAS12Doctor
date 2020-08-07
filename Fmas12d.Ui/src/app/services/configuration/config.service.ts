import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OpenIdConfiguration } from 'angular-auth-oidc-client';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {
  constructor(private httpClient: HttpClient) {}

  public getConfiguration(): Observable<OpenIdConfiguration> {
    return this.httpClient.get(
      environment.apiEndpoint + `/config/configuration`
    ); }
    // .pipe(map(r => r as OpenIdConfiguration)); }
}
