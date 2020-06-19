import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { Observable, from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  private ACCESS_TOKEN_STORAGE_KEY = 'FMAS12DAccessTokenStorageKey';
  private API_STORAGE_KEY = 'FMAS12DMobileApiStorageKey';
  private ERROR_STORAGE_KEY = 'FMAS12DMobileErrorStorageKey';
  private REQ_STORAGE_KEY = 'FMAS12DMobileMobileRequestStorageKey';

  constructor(private storage: Storage) { }

  public clearAccessToken(): Observable<any> {
    return from(this.storage.remove(this.ACCESS_TOKEN_STORAGE_KEY));
  }

  public getAccessToken(): Observable<any> {
    console.log('Get Access Token');
    return from(this.storage.get(this.ACCESS_TOKEN_STORAGE_KEY));
  }

  public storeAccessToken(accessToken: string): Observable<any> {
    console.log('Store Access Token', accessToken);
    return from(this.storage.set(this.ACCESS_TOKEN_STORAGE_KEY, accessToken));
  }

  public getApiRequestData(key: string): Observable<any> {
    return from(this.storage.get(`${this.API_STORAGE_KEY}-${key}`));
  }

  public storeApiRequestData(key: string, data: any): Observable<any> {
    return from(this.storage.set(`${this.API_STORAGE_KEY}-${key}`, data));
  }

  public getErrors(): Observable<any> {
    return from(this.storage.get(this.ERROR_STORAGE_KEY));
  }

  public storeErrors(errors: string): Observable<any> {
    return from(this.storage.set(this.ERROR_STORAGE_KEY, errors));
  }

  public clearRequests(): Observable<any> {
    return from(this.storage.remove(this.REQ_STORAGE_KEY));
  }

  public getRequests(): Observable<any> {
    return from(this.storage.get(this.REQ_STORAGE_KEY));
  }

  public storeRequests(requests: string): Observable<any> {
    return from(this.storage.set(this.REQ_STORAGE_KEY, requests));
  }
}
