import { AmhpAssessmentList } from '../../models/amhp-assessment-list.model';
import { AmhpAssessmentOutcome } from 'src/app/models/amhp-assessment-outcome.model';
import { AmhpAssessmentRequest } from 'src/app/models/amhp-assessment-request.model';
import { AmhpAssessmentView } from '../../models/amhp-assessment-view.model';
import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { map, delay } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { DOCTORSTATUSSELECTED } from 'src/app/constants/app.constants';

@Injectable({
  providedIn: 'root'
})
export class AmhpAssessmentService {
  private assessmentView: AmhpAssessmentView;

  constructor(
    private apiService: ApiService
  ) { }

  public acceptRequestByContactDetail(
    assessmentId: number,
    doctorUserId: number,
    contactDetailId: number
    ) {

    const contactDetailObject = {contactDetailId};

    return (this.apiService.put(
      `${environment.apiEndpoint}/assessment/${assessmentId}/doctors/${doctorUserId}/accepted/contactDetail`,
      contactDetailObject
    ));
  }

  public getList(amhpUserId: number): Observable<AmhpAssessmentList[]> {   
    return (this.apiService.get(
      `${environment.apiEndpoint}/assessment?amhpUserId=${amhpUserId}`, 
      'AmhpUserList'
    ) as Observable<AmhpAssessmentList[]>)
    .pipe(
      map(result => this.assessmentListSort(result)),
    );
  }

  public getRequests(doctorUserId: number): Observable<AmhpAssessmentRequest[]> {
    return (this.apiService.get(
      `${environment.apiEndpoint}/assessment?doctorUserId=${doctorUserId}
      &doctorStatusId=${DOCTORSTATUSSELECTED}`,
      'AmhpUserList'
    ) as Observable<AmhpAssessmentList[]>)
    .pipe(delay(1000))
    .pipe(
      map(result => this.assessmentListSort(result)),
    );
  }

  public getView(assessmentId: number): Observable<AmhpAssessmentView> {
    return this.apiService.get(
      `${environment.apiEndpoint}/assessment/${assessmentId}`,
      `AmhpUserView-${assessmentId}`
    ) as Observable<AmhpAssessmentView>;
  }

  public declineAssessmentRequest(assessmentId: number, doctorId: number) {
    const url = `${environment.apiEndpoint}/assessment/${assessmentId}/doctors/${doctorId}/declined`;
    return this.apiService.put(url, null);
  }

  public putOutcome(
    assessmentOutcome: AmhpAssessmentOutcome,
    assessmentId: number,
    success: boolean):
    Observable<any> {
    let url: string = `${environment.apiEndpoint}/assessment/${assessmentId}/outcome/${success ?
      'success' : 'failure'}`;

    return this.apiService.put(url, assessmentOutcome);  
  }

  public storeView(assessmentView: AmhpAssessmentView): void {
    this.assessmentView = assessmentView;
  }

  public retrieveView(): AmhpAssessmentView {
    return this.assessmentView;
  }

  private assessmentListSort(assessmentList: AmhpAssessmentList[]): AmhpAssessmentList[] {
    return assessmentList.sort(
      (assessment1: AmhpAssessmentList, assessment2: AmhpAssessmentList) => {
        if (assessment1.dateTime > assessment2.dateTime) {
          return 1;
        }
        else if (assessment1.dateTime < assessment2.dateTime) {
          return -1;
        }
        return 0;
      });
  }
}
