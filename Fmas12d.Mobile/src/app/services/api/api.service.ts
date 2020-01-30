import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LogService } from '../log/log.service';
import { NetworkService, ConnectionStatus } from '../network/network.service';
import { Observable, from, of, throwError } from 'rxjs';
import { OfflineManagerService } from '../offline-manager/offline-manager.service';
import { StorageService } from '../storage/storage.service';
import { ToastService } from '../toast/toast.service';
import { tap, map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private http: HttpClient,
    private logService: LogService,
    private networkService: NetworkService,
    private offlineManager: OfflineManagerService,
    private storageService: StorageService,
    private toastService: ToastService
  ) { }

  public delete(url: string): Observable<any> {
    if (this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Offline) {
      return from(this.offlineManager.storeRequest(url, 'DELETE', null));
    } else {
      return this.http.delete(url)
        .pipe(
          catchError(error => {
            this.logService.logError(error);
            this.toastService.displayError({ message: error.message });
            return throwError(error);
          }
          )
        );
    }
  }

  public get(url: string, storageKey: string): Observable<any> {
    if (this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Offline) {
      return this.storageService.getApiRequestData(storageKey);
    } else {
      return this.http.get(url)
        .pipe(
          catchError(error => {
            this.logService.logError(error);
            this.toastService.displayError({ message: error.message });
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
            this.toastService.displayError({ message: error.message });
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
            this.toastService.displayError({ message: error.message });
            return throwError(error);
          }
          )
        );
    }
  }

}
