import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, delay } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Referral } from '../../interfaces/referral';
import { ReferralEdit } from 'src/app/interfaces/referralEdit';
import { ReferralStatus } from '../../enums/ReferralStatus.enum';
import { ReferralView } from 'src/app/interfaces/referral-view';

@Injectable({
  providedIn: 'root'
})
export class ReferralService {
  constructor(private httpClient: HttpClient) {}

  public closeReferral(referralId: number, force?: boolean) {

    let endpoint = `${environment.apiEndpoint}/referral/${referralId}/close`;

    if ( force === true ) {
      endpoint += '/force';
    }

    return this.httpClient.put(
      endpoint,
      null
    );
  }

  public getReferralSummary(referralId: number): Observable<Referral> {
    return this.httpClient.get(
      environment.apiEndpoint + `/referral/view/${referralId}/summary`
    )
    .pipe(delay(1000))
    .pipe
      (map(r => r as Referral)
    );
  }

  public getReferralEdit(referralId: number): Observable<ReferralEdit> {
    return this.httpClient.get(
      environment.apiEndpoint + `/referral/edit/${referralId}`
    )
    .pipe(delay(1000))
    .pipe
      (map(r => r as ReferralEdit)
    );
  }

  public getReferralView(referralId: number): Observable<ReferralView> {
    return this.httpClient.get(
      environment.apiEndpoint + `/referral/view/${referralId}`
    )
    .pipe(delay(1000))
    .pipe
      (map(r => r as ReferralView)
    );
  }

  public createReferral(newReferral: Referral, retrospective: boolean) {

    // ToDo: Get the id of the logged on user
    newReferral.createdByUserId = 1;

    newReferral.referralStatusId = ReferralStatus.NewReferral;

    const endpoint = retrospective ? '/retrospective' : '';

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      `${environment.apiEndpoint}/referral${endpoint}`,
      newReferral,
      { headers }
    );
  }


  public updateReferral(referral: Referral) {

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.put(
      `${environment.apiEndpoint}/referral/${referral.id}`,
      referral,
      { headers }
    );
  }

  public updateRetrospectiveReferral(referral: Referral) {

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.put(
      `${environment.apiEndpoint}/referral/${referral.id}/retrospective`,
      referral,
      { headers }
    );
  }
}
