import { ApiService } from '../api/api.service';
import { AssessmentClaim } from 'src/app/models/assessment-claim.model';
import { AssessmentContact } from 'src/app/models/assessment-contact.model';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { UserAssessmentClaim } from 'src/app/models/user-assessment-claim.model';
import { UserAssessmentClaimResponse } from 'src/app/models/user-assessment-claim-response.model';
import { UserAssessmentClaimRequest } from 'src/app/models/user-assessment-claim-request.model';
import { UserAssessmentClaimList } from 'src/app/models/user-assessment-claim-list.model';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AssessmentClaimService {

  public readonly claimsCount: ReplaySubject<number> = new ReplaySubject<number>(1);

  constructor(
    private apiService: ApiService
  ) { }

  public getClaim(claimId: number): Observable<UserAssessmentClaim> {
    return this.apiService.get(
      `${environment.apiEndpoint}/assessmentclaim/${claimId}`,
      null
    );
  }

  public getAssessmentAndLocations(assessmentId: number): Observable<AssessmentContact> {
    return this.apiService.get(
      `${environment.apiEndpoint}/assessmentclaim/${assessmentId}/assessmentandlocations`,
      null
    );
  }

  public validateClaim(
    assessmentId: number,
    claimRequest: UserAssessmentClaimRequest
  ): Observable<UserAssessmentClaimResponse> {
    return this.apiService.post(
      `${environment.apiEndpoint}/assessmentclaim/${assessmentId}/validate`,
      claimRequest
    );
  }

  public confirmClaim(
    assessmentId: number,
    claimRequest: UserAssessmentClaimRequest
  ): Observable<UserAssessmentClaimResponse> {
    return this.apiService.post(
      `${environment.apiEndpoint}/assessmentclaim/${assessmentId}/confirm`,
      claimRequest
    );
  }

  public getList(): Observable<UserAssessmentClaimList> {
    return (this.apiService.get(
      `${environment.apiEndpoint}/assessmentclaim/list`,
      null
    ) as Observable<UserAssessmentClaimList>)
    .pipe(
      map(result => this.queryPossibleClaims(result)),
    );
  }

  private queryPossibleClaims(claimsList: UserAssessmentClaimList): UserAssessmentClaimList {

    if (typeof claimsList.assessments !== 'undefined') {
      // Determine how many assessments can now have a claim made.
      const completedAssessmentCount =
        claimsList.assessments.filter(assessment => assessment.hasBeenReviewed === true).length;
      this.claimsCount.next(completedAssessmentCount);
    }

    return claimsList;
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
