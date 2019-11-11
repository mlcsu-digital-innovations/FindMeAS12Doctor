import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, from, of, forkJoin } from 'rxjs';
import { StorageService } from '../storage/storage.service';
import { StoredRequest } from 'src/app/interfaces/stored-request.interface';
import { ToastService } from 'src/app/services/toast/toast.service';
import { switchMap, finalize } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OfflineManagerService {

  constructor(    
    private http: HttpClient, 
    private storageService: StorageService,
    private toastService: ToastService
    ) { }
 
  public checkForEvents(): Observable<any> {    
    return this.storageService.getRequests().pipe(
      switchMap(storedOperations => {
        let storedObj = JSON.parse(storedOperations);
        if (storedObj && storedObj.length > 0) {
          return this.sendRequests(storedObj).pipe(
            finalize(() => {
              this.toastService.displayMessage({
                message: `Local data succesfully synced to API`                          
              });              
               
              this.storageService.clearRequests();
            })
          );
        } else {          
          return of(false);
        }
      })
    )
  }
 
  public storeRequest(url, type, data): Observable<any> {
    this.toastService.displayMessage({
      message: 'Your data will be stored locally until you are next online.'  
    });   
 
    let action: StoredRequest = {
      url: url,
      type: type,
      data: data,
      time: new Date().getTime(),
      id: Math.random().toString(36).replace(/[^a-z]+/g, '').substr(0, 5)
    };    
     
    return this.storageService.getRequests().do((storedOperations: string) => {
      let storedObj = JSON.parse(storedOperations);
 
      if (storedObj) {
        storedObj.push(action);
      } else {
        storedObj = [action];
      }

      return this.storageService.storeRequests(JSON.stringify(storedObj));
    });
  }
 
  public sendRequests(operations: StoredRequest[]): Observable<unknown> {
    let obs: any[] = [];
 
    for (let op of operations) {      
      let oneObs = this.http.request(op.type, op.url, op.data);
      obs.push(oneObs);
    }
 
    return forkJoin(obs);
  }
}
