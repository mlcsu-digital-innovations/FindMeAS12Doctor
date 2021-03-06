import { AmhpAssessmentList } from '../../models/amhp-assessment-list.model';
import { AmhpAssessmentOutcome } from 'src/app/models/amhp-assessment-outcome.model';
import { AmhpAssessmentRequest } from 'src/app/models/amhp-assessment-request.model';
import { AmhpAssessmentView } from '../../models/amhp-assessment-view.model';
import { ApiService } from '../api/api.service';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { map, delay } from 'rxjs/operators';
import { Observable, ReplaySubject } from 'rxjs';
import { DOCTORSTATUSSELECTED, REFERRALSTATUS_NEW, REFERRALSTATUS_SELECTING, REFERRALSTATUS_AWAITING_RESPONSES, REFERRALSTATUS_RESPONSES_PARTIAL, REFERRALSTATUS_RESPONSES_COMPLETE, REFERRALSTATUSASSESSMENTSCHEDULED } from 'src/app/constants/app.constants';

@Injectable({
  providedIn: 'root'
})
export class AmhpAssessmentService {
  private assessmentView: AmhpAssessmentView;

  public readonly assessmentCount: ReplaySubject<number> = new ReplaySubject<number>(1);
  public readonly scheduledAssessmentCount: ReplaySubject<number> = new ReplaySubject<number>(1);

  constructor(
    private apiService: ApiService
  ) { }

  public acceptRequest(
    assessmentId: number
    ) {

    return (this.apiService.put(
      `${environment.apiEndpoint}/assessment/${assessmentId}/accepted`,
      null
    ));
  }

  public acceptRequestByContactDetail(
    assessmentId: number,
    contactDetailId: number
    ) {

    const contactDetailObject = {contactDetailId};
    return (this.apiService.put(
      `${environment.apiEndpoint}/assessment/${assessmentId}/accepted/contactDetail`,
      contactDetailObject
    ));
  }

  public acceptRequestByCoordinates(
    assessmentId: number,
    latitude: number,
    longitude: number,
    ) {

    const coordinateObject = {latitude, longitude};

    return (this.apiService.put(
      `${environment.apiEndpoint}/assessment/${assessmentId}/accepted/location`,
      coordinateObject
    ));
  }

  public acceptRequestByPostcode(
    assessmentId: number,
    postcode: string
    ) {

    const postcodeObject = {postcode};

    return (this.apiService.put(
      `${environment.apiEndpoint}/assessment/${assessmentId}/accepted/postcode`,
      postcodeObject
    ));
  }

  public getList(): Observable<AmhpAssessmentList[]> {
    return (this.apiService.get(
      `${environment.apiEndpoint}/assessment/list`,
      'AmhpUserList'
    ) as Observable<AmhpAssessmentList[]>)
    .pipe(
      map(result => this.assessmentListSort(result)),
    );
  }

  public getRequests(): Observable<AmhpAssessmentRequest[]> {
    return (this.apiService.get(
      `${environment.apiEndpoint}/assessment/list`,
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

  public declineAssessmentRequest(assessmentId: number) {
    const url = `${environment.apiEndpoint}/assessment/${assessmentId}/declined`;
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
    if (assessmentList === null) { assessmentList = []; }

    // Calculate the number of assessments that need action.
    const unscheduledStatuses: number[] =
      [REFERRALSTATUS_NEW, REFERRALSTATUS_SELECTING, REFERRALSTATUS_AWAITING_RESPONSES,
        REFERRALSTATUS_RESPONSES_PARTIAL, REFERRALSTATUS_RESPONSES_COMPLETE];

    const scheduledStatuses: number[] = [REFERRALSTATUSASSESSMENTSCHEDULED];

    const assessmentsAwaitingAction =
      assessmentList
        .filter(assessment => unscheduledStatuses.includes(assessment.referralStatusId))
        .filter(assessment => assessment.doctorHasAccepted === null);

    this.assessmentCount.next(assessmentsAwaitingAction.length);

    const scheduledAssessments =
        assessmentList
          .filter(assessment => scheduledStatuses.includes(assessment.referralStatusId));

    this.scheduledAssessmentCount.next(scheduledAssessments.length);

    return assessmentList.sort(
      (assessment1: AmhpAssessmentList, assessment2: AmhpAssessmentList) => {
        if (assessment1.dateTime > assessment2.dateTime) {
          return 1;
        } else if (assessment1.dateTime < assessment2.dateTime) {
          return -1;
        }
        return 0;
      });
  }
}
