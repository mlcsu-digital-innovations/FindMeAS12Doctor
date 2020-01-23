import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AssessmentClaim } from 'src/app/models/assessment-claim.model';
import { UserAssessmentClaim } from 'src/app/models/user-assessment-claim-model';

@Injectable({
  providedIn: 'root'
})
export class AssessmentClaimService {

  constructor(
    private apiService: ApiService
  ) { }

  public getClaim(claimId: number): Observable<UserAssessmentClaim> {
    return this.apiService.get(
      `${environment.apiEndpoint}/assessmentclaim/${claimId}`,
      null
    );
  }

  public prepareClaim(assessmentId: number): Observable<UserAssessmentClaim> {
    return this.apiService.get(
      `${environment.apiEndpoint}/assessmentclaim/${assessmentId}/prepare`,
      null
    );
  }

  public getList(): Observable<AssessmentClaim[]> {
    // return (this.apiService.get(
    //   `${environment.apiEndpoint}/assessment/claims`,
    //   'AmhpUserList'
    // ) as Observable<AssessmentClaim[]>)
    // .pipe(
    //   map(result => this.claimsListSort(result)),
    // );

    const claim1 = {} as AssessmentClaim;
    const claim2 = {} as AssessmentClaim;
    const claim3 = {} as AssessmentClaim;

    claim1.assessmentDate = new Date();
    claim1.address1 = '121 Main Road';
    claim1.postcode = 'ST4 4LX';
    claim1.id = 1;
    claim1.isSuccessful = true;
    claim1.claim = {} as UserAssessmentClaim;
    claim1.claim.claimStatusId = 1;
    claim1.claim.claimStatus = 'Accepted';
    claim1.claim.isClaimable = true;
    claim1.claim.mileage = 25;
    claim1.claim.mileagePayment = 12.5;
    claim1.claim.assessmentPayment = 35;
    claim1.claim.id = 7;


    claim2.assessmentDate = new Date();
    claim2.address1 = '999 Letsbe Avenue';
    claim2.postcode = 'PO32 6EY';
    claim2.id = 3005;
    claim2.isSuccessful = true;
    claim2.isCompleted = false;

    claim3.assessmentDate = new Date();
    claim3.address1 = '12 Manor Avenue';
    claim3.postcode = 'NG5 1PB';
    claim3.id = 3003;
    claim3.isSuccessful = false;
    claim3.unsuccessfulAssessmentType = 'Patient unavailable';
    claim3.isCompleted = true;
    
    const claims = [claim1, claim2, claim3];

    return of(claims);

  }

  private claimsListSort(claimList: AssessmentClaim[]): AssessmentClaim[] {
    return claimList.sort(
      (claim1: AssessmentClaim, claim2: AssessmentClaim) => {
        if (claim1.assessmentDate > claim2.assessmentDate) {
          return 1;
        } else if (claim1.assessmentDate < claim2.assessmentDate) {
          return -1;
        }
        return 0;
      });
  }

}
