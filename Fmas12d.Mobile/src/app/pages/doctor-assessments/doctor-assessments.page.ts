import { AmhpAssessmentRequest } from 'src/app/models/amhp-assessment-request.model';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import {
  ASSESSMENTSCHEDULED, ASSESSMENTRESCHEDULING, AWAITINGREVIEW, DOCTORSTATUSSELECTED,
  DOCTORSTATUSALLOCATED, REFERRALSTATUSOPEN
} from 'src/app/constants/app.constants';
import { BroadcastService } from '@azure/msal-angular';
import { Component } from '@angular/core';
import { LoadingController, ToastController } from '@ionic/angular';

import * as moment from 'moment';

@Component({
  selector: 'app-doctor-assessments',
  templateUrl: './doctor-assessments.page.html',
  styleUrls: ['./doctor-assessments.page.scss'],
})
export class DoctorAssessmentsPage {

  public assessmentRequestsLastUpdated: Date;
  // public assessmentRequests$: Observable<AmhpAssessmentRequest[]>;

  public allAssessments: AmhpAssessmentRequest[] = [];
  public assessmentRequests: AmhpAssessmentRequest[] = [];
  public scheduledAssessments: AmhpAssessmentRequest[] = [];

  private loading: HTMLIonLoadingElement;
  private hasData: boolean;
  public showAll: boolean;

  private readonly scheduled = [ASSESSMENTSCHEDULED, ASSESSMENTRESCHEDULING, AWAITINGREVIEW];

  constructor(
    private assessmentService: AmhpAssessmentService,
    private broadcastService: BroadcastService,
    private loadingController: LoadingController,
    private toastController: ToastController
  ) { }

  ionViewDidEnter() {
    this.refreshPage();
  }

  toggleChanged(showAll: boolean) {
    this.filterData();
  }

  filterData() {

    this.scheduledAssessments = [];
    this.assessmentRequests = [];

    // scheduled assessments will have a referralId of 6, 7 or 8
    this.scheduledAssessments = this.allAssessments.filter
      (assessment => this.scheduled.includes(assessment.referralStatusId));

    this.assessmentRequests = this.allAssessments
      .filter(assessment => !this.scheduled.includes(assessment.referralStatusId))
      .filter(assessment => assessment.referralStatusId !== REFERRALSTATUSOPEN);

    if (!this.showAll) {
      const currentDate = moment().startOf('day');

      this.scheduledAssessments =
        this.scheduledAssessments
        .filter(assessment => moment(assessment.dateTime).isAfter(currentDate));

      this.assessmentRequests =
        this.assessmentRequests
        .filter(assessment => moment(assessment.dateTime).isAfter(currentDate));
    }
  }

  refreshPage($event?: any) {
    // load data each time the page is shown
    // this may be changed if offline data is to be used
    const request = this.assessmentService.getRequests();
    this.showLoading();

    request
      .subscribe(
        result => {

          this.hasData = true;
          this.allAssessments = result;

          if (result && result.length > 0) {
            this.filterData();
          }

          this.broadcastService.broadcast('assessments:requiringaction',
            this.assessmentRequests.filter(
              assessment => assessment.doctorStatusId === DOCTORSTATUSSELECTED &&
                assessment.doctorHasAccepted === null
            ));

          this.assessmentRequestsLastUpdated = new Date();
          this.closeLoading();
          this.closeRefreshing($event);
        }, error => {
          this.showErrorToast(error);
          this.closeLoading();
          this.closeRefreshing($event);
        }
      );
  }

  assessmentIsRescheduling(assessment: AmhpAssessmentRequest): boolean {
    return assessment.referralStatusId === ASSESSMENTRESCHEDULING;
  }
  assessmentIsReviewing(assessment: AmhpAssessmentRequest): boolean {
    return assessment.referralStatusId === AWAITINGREVIEW;
  }
  assessmentIsScheduled(assessment: AmhpAssessmentRequest): boolean {
    return assessment.referralStatusId === ASSESSMENTSCHEDULED;
  }

  closeLoading() {
    if (this.loading) {
      setTimeout(() => { this.loading.dismiss(); }, 500);
    }
  }

  closeRefreshing($event?: any) {
    if ($event) {
      $event.target.complete();
    }
  }

  doctorIsAllocated(assessment: AmhpAssessmentRequest): boolean {
    return assessment.doctorStatusId === DOCTORSTATUSALLOCATED;
  }

  nothingToDisplay(): boolean {
    return this.scheduledAssessments.length === 0
      && this.assessmentRequests.length === 0
      && this.hasData;
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines'
    });
    await this.loading.present();
  }

  showErrorToast(msg: string) {
    this.showToast(msg, 'danger', 'Error!', 5000);
  }
  showSuccessToast(msg: string) {
    this.showToast(msg, 'success', 'Success');
  }

  async showToast(msg: string, colour: string, hdr: string, dur?: number) {
    const toast = await this.toastController.create({
      message: msg,
      header: hdr,
      color: colour,
      duration: dur ? dur : 2000,
      position: 'top'
    });
    await toast.present();
  }
}
