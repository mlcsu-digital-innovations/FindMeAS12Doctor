import { ConfigService } from '../config/config.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '../../pages/amhp-assessment-list/node_modules/@angular/core';
import { LogService } from '../log/log.service';
import { NetworkService, ConnectionStatus } from '../network/network.service';
import { Observable, from } from '../../pages/amhp-assessment-list/node_modules/rxjs';
import { OfflineManagerService } from '../offline-manager/offline-manager.service';
import { Storage } from '@ionic/storage';
import { tap, map, catchError } from '../amhp-assessment/node_modules/rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {  
  private headers: HttpHeaders = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(
    private configService: ConfigService,
    private http: HttpClient, 
    private networkService: NetworkService,
    private offlineManager: OfflineManagerService,
    private storage: Storage,
    private logService: LogService
    ) { }
 
  public get(url: string, storageKey: string): Observable<any> {
    if (this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Offline) {
      return from(this.getLocalData(storageKey));
    }    
    else {
      return this.http.get(url)
        .pipe(
          catchError(error => {      
            this.logService.logError(error);
            throw new Error(error);
          }),
          map(result => result),
          tap(result => this.setLocalData(storageKey, result)
        )      
      );  
    }         
  }

  public put(url: string, body: any): Observable<any> {
    if (this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Offline) {
      return from(this.offlineManager.storeRequest(url, "PUT", body));
    }
    else {
      return this.http.put(url, body, { headers: this.headers })
        .pipe(
          catchError(error => {
            this.logService.logError(error);
            throw new Error(error)
          }
        )        
      );
    }
    
  }

  private setLocalData(key, data) {
    this.storage.set(`${this.configService.API_STORAGE_KEY}-${key}`, data);
  }
 
  private getLocalData(key) {
    return this.storage.get(`${this.configService.API_STORAGE_KEY}-${key}`);
  }
}
