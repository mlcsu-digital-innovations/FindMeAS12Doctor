import { ApiService } from '../api/api.service';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OnCallService {

  constructor(private apiService: ApiService) { }

  public getListForUser() {
    return (this.apiService.get(
      `${environment.apiEndpoint}/user/oncall/current`,
      null
    ));
  }

  public confirm(id: number) {
    return (this.apiService.put(
      `${environment.apiEndpoint}/user/oncall/${id}/confirm`, 
      null
    ));
  }

  public reject(id: number, onCallPutReject: any) {
    return (this.apiService.put(
      `${environment.apiEndpoint}/user/oncall/${id}/reject`, 
      onCallPutReject
    ));
  }

}
