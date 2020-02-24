import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, delay } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ClaimView } from 'src/app/interfaces/claim-view';
import { CLAIM_STATUS_PROCESSING, CLAIM_STATUS_QUERY } from 'src/app/constants/Constants';

@Injectable({
  providedIn: 'root'
})
export class FinanceClaimService {
  constructor(private httpClient: HttpClient) {}

  public getClaimView(claimId: number): Observable<ClaimView> {
    return this.httpClient.get(
      environment.apiEndpoint + `/financeassessmentclaim/${claimId}`
    )
    .pipe(delay(1000))
    .pipe
      (map(r => r as ClaimView)
    );
  }

  public updateClaimStatusToProcessing(claimId: number): Observable<ClaimView> {
    return this.updateClaimStatus(claimId, CLAIM_STATUS_PROCESSING);
  }

  public updateClaimStatusToQuerying(claimId: number): Observable<ClaimView> {
    return this.updateClaimStatus(claimId, CLAIM_STATUS_QUERY);
  }

  private updateClaimStatus(id: number, claimStatusId: number): Observable<any> {

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.httpClient.put(
      `${environment.apiEndpoint}/financeassessmentclaim/${id}`,
      {claimStatusId},
      { headers }
    );
  }

}