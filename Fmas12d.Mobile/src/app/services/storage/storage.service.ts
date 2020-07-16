import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { Observable, from } from 'rxjs';
import * as jwt_decode from 'jwt-decode';

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
    return from(this.storage.get(this.ACCESS_TOKEN_STORAGE_KEY));
  }

  public storeAccessToken(accessToken: string): Observable<any> {
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

  public storePin(oid: string, pin: string): Observable<any> {
    return from(this.storage.set(`${oid}-lock`, pin));
  }

  public setPin(pin: string): Observable<boolean> {

    let oid = '';
    const setPin = new Observable<boolean>((observer) => {

      this.storage.get(this.ACCESS_TOKEN_STORAGE_KEY)
      .then(token => {
        if (token !== null) {
          const details = jwt_decode(token);

          if (details.oid !== null) {
            oid = details.oid;
          }

          this.storage.set(`${oid}-lock`, pin)
            .then(() => {
              observer.next();
              observer.complete();
            }, err => {
              observer.error(err);
            });
        }
      });
   });

    return setPin;
  }

  public clearPin(): Observable<boolean> {

    let oid = '';
    const clearPin = new Observable<boolean>((observer) => {

      this.storage.get(this.ACCESS_TOKEN_STORAGE_KEY)
      .then(token => {
        if (token !== null) {
          const details = jwt_decode(token);

          if (details.oid !== null) {
            oid = details.oid;
          }

          this.storage.remove(`${oid}-lock`)
            .then(() => {
              observer.next();
              observer.complete();
            }, err => {
              observer.error(err);
            });
        }
      });
   });

    return clearPin;
  }


  public hasPin(): Observable<boolean> {

    let oid = '';
    const checkPin = new Observable<boolean>((observer) => {

      this.storage.get(this.ACCESS_TOKEN_STORAGE_KEY)
      .then(token => {
        if (token !== null) {
          const details = jwt_decode(token);

          if (details.oid !== null) {
            oid = details.oid;
          }

          this.storage.get(`${oid}-lock`)
            .then(pin => {
              observer.next(pin !== null);
              observer.complete();
            }, err => {
              observer.error(err);
            });
        }
      });
   });

    return checkPin;
  }

  public comparePin(userPin: string): Observable<any> {

    let oid = '';
    const comparePin = new Observable((observer) => {

      this.storage.get(this.ACCESS_TOKEN_STORAGE_KEY)
      .then(token => {
        if (token !== null) {
          const details = jwt_decode(token);

          if (details.oid !== null) {
            oid = details.oid;
          }

          this.storage.get(`${oid}-lock`)
            .then(pin => {
              observer.next(pin === userPin);
              observer.complete();
            }, err => {
              observer.error(err);
            });
        }
      });
   });

    return comparePin;
  }

  public getUserNameFromToken(): Observable<any> {

    const getUsername = new Observable((observer) => {

      this.storage.get(this.ACCESS_TOKEN_STORAGE_KEY)
      .then(token => {

        if (token !== null) {

          const details = jwt_decode(token);

          if (details.name) {
            observer.next(details.name);
            observer.complete();
          } else {
            observer.error();
          }
        } else {
          observer.error();
        }
      }, err => { console.log(err); } );
   });

    return getUsername;
  }

}
