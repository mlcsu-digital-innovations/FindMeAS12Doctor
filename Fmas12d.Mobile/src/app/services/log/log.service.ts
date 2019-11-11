import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StorageService } from '../storage/storage.service';
import { StoredError } from 'src/app/interfaces/stored-error.interface';

@Injectable({
  providedIn: 'root'
})
export class LogService {

  constructor( 
    private storageService: StorageService
    ) { }

  public logError(error: any): Observable<any> {     
    let err: StoredError = {
      code: error.status,
      url: error.url,
      message: `${error.error && error.error.title ? error.error.title : error.message}`,
      dateTime: new Date(),
      id: Math.random().toString(36).replace(/[^a-z]+/g, '').substr(0, 5)
    };    
    
    return this.storageService.getErrors().do((storedErrors: string) => {
      let storedObj: any = JSON.parse(storedErrors);

      if (storedObj) {
        storedObj.push(err);
      } else {
        storedObj = [err];
      }
      
      return this.storageService.storeErrors(JSON.stringify(storedObj));
    });
  }
}
