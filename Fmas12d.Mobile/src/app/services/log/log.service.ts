import { ConfigService } from '../config/config.service';
import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { StoredError } from 'src/app/interfaces/stored-error.interface';

@Injectable({
  providedIn: 'root'
})
export class LogService {

  constructor(
    private configService: ConfigService,
    private storage: Storage
    ) { }

  public logError(error: any): Promise<any> {     
    let err: StoredError = {
      code: error.status,
      url: error.url,
      message: `${error.error && error.error.title ? error.error.title : error.message}`,
      dateTime: new Date(),
      id: Math.random().toString(36).replace(/[^a-z]+/g, '').substr(0, 5)
    };    

    return this.storage.get(this.configService.ERROR_STORAGE_KEY).then(storedErrors => {
      let storedObj: any = JSON.parse(storedErrors);

      if (storedObj) {
        storedObj.push(err);
      } else {
        storedObj = [err];
      }

      return this.storage.set(this.configService.ERROR_STORAGE_KEY, JSON.stringify(storedObj));
    });
  }
}
