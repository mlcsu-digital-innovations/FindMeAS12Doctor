import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  public API_STORAGE_KEY: string = 'FMAS12DMobileApiStorageKey';
  public ERROR_STORAGE_KEY: string = "FMAS12DErrorStorageKey";
  public REQ_STORAGE_KEY = 'MEPMobileRequestStorageKey';

  constructor() { }
}
