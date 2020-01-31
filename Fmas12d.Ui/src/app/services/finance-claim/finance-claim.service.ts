import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, delay } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ClaimView } from 'src/app/interfaces/claim-view';

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

}
