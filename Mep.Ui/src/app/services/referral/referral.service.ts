import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Referral } from '../../interfaces/referral';
import { ReferralStatus } from '../../enums/ReferralStatus.enum';
import { Observable, empty, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReferralService {
  constructor(private httpClient: HttpClient) {}

  public getReferral(referralId: number): Observable<Referral> {
    return this.httpClient.get(
      environment.apiEndpoint + `/referral/${referralId}`
    )
    .pipe
      (map(r => r as Referral)
    );
  }

  public createReferral(newReferral: Referral) {

    // ToDo: Get the id of the logged on user
    newReferral.createdByUserId = 1;

    newReferral.referralStatusId = ReferralStatus.NewReferral;

    // ToDo: sort the time offset !
    newReferral.createdAt = new Date();

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      environment.apiEndpoint + '/referral',
      newReferral,
      { headers }
    );
  }
}
