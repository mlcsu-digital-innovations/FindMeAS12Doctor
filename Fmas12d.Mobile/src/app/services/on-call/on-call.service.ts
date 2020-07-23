import { ApiService } from '../api/api.service';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { OnCallDoctor } from 'src/app/interfaces/on-call-doctor.interface';

@Injectable({
  providedIn: 'root'
})
export class OnCallService {

  public unconfirmedOnCall: ReplaySubject<number> = new ReplaySubject<number>(1);

  constructor(private apiService: ApiService) { }

  public getListForUser() {
    return (this.apiService.get(
      `${environment.apiEndpoint}/user/oncall/current`,
      null
    ))
    .pipe(
      map((result: OnCallDoctor[]) => {
        const unconfirmedOnCallRequests = result
        .filter((onCall: OnCallDoctor) => onCall.onCallIsConfirmed === null).length;

        this.unconfirmedOnCall.next(unconfirmedOnCallRequests);
        return result;
      })
    );
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
