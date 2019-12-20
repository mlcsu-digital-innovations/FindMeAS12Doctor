import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LogService } from '../log/log.service';
import { NetworkService, ConnectionStatus } from '../network/network.service';
import { Observable, from, of, throwError } from 'rxjs';
import { OfflineManagerService } from '../offline-manager/offline-manager.service';
import { StorageService } from '../storage/storage.service';
import { tap, map, catchError } from 'rxjs/operators';
import { OAuthSettings } from 'src/oauth';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private contentType = 'application/json';
  private accessToken: string;

  constructor(
    private http: HttpClient,
    private logService: LogService,
    private networkService: NetworkService,
    private offlineManager: OfflineManagerService,
    private storageService: StorageService
    ) { }

  public get(url: string, storageKey: string): Observable<any> {
    if (this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Offline) {
      return this.storageService.getApiRequestData(storageKey);
    } else {
      return this.http.get(url)
        .pipe(
          catchError(error => {
            this.logService.logError(error);
            return throwError(error);
          }),
          map(result => result),
          tap(result => this.storageService.storeApiRequestData(storageKey, result)
        )
      );
    }
  }

  public put(url: string, body: any): Observable<any> {
    if (this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Offline) {
      return from(this.offlineManager.storeRequest(url, 'PUT', body));
    } else {
      return this.http.put(url, body)
        .pipe(
          catchError(error => {
            this.logService.logError(error);
            return throwError(error);
          }
        )
      );
    }
  }

  public post(url: string, body: any): Observable<any> {
    if (this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Offline) {
      return from(this.offlineManager.storeRequest(url, 'POST', body));
    } else {
      return this.http.post(url, body)
        .pipe(
          catchError(error => {
            this.logService.logError(error);
            return throwError(error);
          }
        )
      );
    }
  }

  public login(email: string, password: string): Observable<any> {
    let url = OAuthSettings.oauth2Endpoint;
    
    let headers: HttpHeaders = new HttpHeaders();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');

    let scopes: string = OAuthSettings.scopes.join(" ");

    let params: HttpParams = new HttpParams();
    params.set('client_id', OAuthSettings.appId);
    params.set('client_secret', OAuthSettings.clientSecret);
    params.set('scope', scopes);
    params.set('username', email);
    params.set('password', password);
    params.set('grant_type', 'password');

    return this.http.post(url, params, {headers: headers});
  }

}
