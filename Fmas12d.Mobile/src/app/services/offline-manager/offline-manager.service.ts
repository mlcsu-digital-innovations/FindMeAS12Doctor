import { ConfigService } from '../config/config.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '../../pages/amhp-assessment-list/node_modules/@angular/core';
import { Observable, from, of, forkJoin } from '../../pages/amhp-assessment-list/node_modules/rxjs';
import { Storage } from '@ionic/storage';
import { ToastService } from 'src/app/services/toast/toast.service';
import { switchMap, finalize } from '../amhp-assessment/node_modules/rxjs/operators';
import { StoredRequest } from 'src/app/interfaces/stored-request.interface';

@Injectable({
  providedIn: 'root'
})
export class OfflineManagerService {

  constructor(
    private configService: ConfigService,
    private storage: Storage, 
    private http: HttpClient, 
    private toastService: ToastService
    ) { }
 
  public checkForEvents(): Observable<any> {
    return from(this.storage.get(this.configService.REQ_STORAGE_KEY)).pipe(
      switchMap(storedOperations => {
        let storedObj = JSON.parse(storedOperations);
        if (storedObj && storedObj.length > 0) {
          return this.sendRequests(storedObj).pipe(
            finalize(() => {
              this.toastService.displayMessage({
                message: `Local data succesfully synced to API`                          
              });              
 
              this.storage.remove(this.configService.REQ_STORAGE_KEY);
            })
          );
        } else {          
          return of(false);
        }
      })
    )
  }
 
  public storeRequest(url, type, data): Promise<any> {
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
 
    return this.storage.get(this.configService.REQ_STORAGE_KEY).then(storedOperations => {
      let storedObj = JSON.parse(storedOperations);
 
      if (storedObj) {
        storedObj.push(action);
      } else {
        storedObj = [action];
      }

      return this.storage.set(this.configService.REQ_STORAGE_KEY, JSON.stringify(storedObj));
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
