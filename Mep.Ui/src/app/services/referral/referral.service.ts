import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Referral } from '../../interfaces/referral';

@Injectable({
  providedIn: 'root'
})
export class ReferralService {
  constructor(private httpClient: HttpClient) {}

  public createReferral(newReferral: Referral ) {

    console.log(newReferral);

    // ToDo: Get the id of the logged on user
    newReferral.CreatedByUserId = 1;

    // ToDo: Get the id of a new referral status
    newReferral.ReferralStatusId = 1;

    // ToDo: sort the time offset !
    newReferral.CreatedAt = new Date();

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.post(
      environment.apiEndpoint + '/referral',
      newReferral,
      { headers }
    );
  }
}
