import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LogService } from '../log/log.service';
import { NetworkService, ConnectionStatus } from '../network/network.service';
import { Observable, from, of } from 'rxjs';
import { OfflineManagerService } from '../offline-manager/offline-manager.service';
import { StorageService } from '../storage/storage.service';
import { tap, map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {    
  private contentType: string = "application/json";
  private accessToken: string;

  constructor(        
    private http: HttpClient, 
    private logService: LogService,
    private networkService: NetworkService,
    private offlineManager: OfflineManagerService,    
    private storageService: StorageService
    ) 
    { }
 
  public get(url: string, storageKey: string): Observable<any> {
    if (this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Offline) {      
      return this.storageService.getApiRequestData(storageKey);
    }    
    else {       
      return this.http.get(url)          
        .pipe(
          catchError(error => {      
            this.logService.logError(error);            
            throw new Error(error);            
          }),
          map(result => result),
          tap(result => this.storageService.storeApiRequestData(storageKey, result)
        )      
      );       
    }                             
  }

  public put(url: string, body: any): Observable<any> {
    if (this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Offline) {
      return from(this.offlineManager.storeRequest(url, "PUT", body));
    }
    else {      
      return this.http.put(url, body)
        .pipe(
          catchError(error => {
            this.logService.logError(error);
            throw new Error(error)
          }
        )        
      );               
    }        
  }

}
