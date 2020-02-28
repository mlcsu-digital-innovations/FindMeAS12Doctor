import { AmhpAssessmentRequest } from 'src/app/models/amhp-assessment-request.model';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { LoadingController, ToastController } from '@ionic/angular';
import { ASSESSMENTSCHEDULED, ASSESSMENTRESCHEDULING, AWAITINGREVIEW } from 'src/app/constants/app.constants';

@Component({
  selector: 'app-doctor-assessments',
  templateUrl: './doctor-assessments.page.html',
  styleUrls: ['./doctor-assessments.page.scss'],
})
export class DoctorAssessmentsPage implements OnInit {

  public assessmentRequestsLastUpdated: Date;
  public assessmentRequests$: Observable<AmhpAssessmentRequest[]>;

  public allAssessments: AmhpAssessmentRequest[] = [];
  public assessmentRequests: AmhpAssessmentRequest[] = [];
  public scheduledAssessments: AmhpAssessmentRequest[] = [];

  private loading: HTMLIonLoadingElement;

  constructor(
    private assessmentService: AmhpAssessmentService,
    private loadingController: LoadingController,
    private toastController: ToastController) { }

  ngOnInit() {
    this.refreshPage();
  }

  ionViewDidEnter() {
    this.refreshPage();
  }

  refreshPage($event?: any) {
    // load data each time the page is shown
    // this may be changed if offline data is to be used
    const request = this.assessmentService.getRequests();
    this.showLoading();

    request
      .subscribe(
        result => {

          this.allAssessments = result;

          if (result && result.length > 0) {

            const scheduled = [ASSESSMENTSCHEDULED, ASSESSMENTRESCHEDULING, AWAITINGREVIEW];

            // scheduled assessments will have a referralId of 6, 7 or 8
            this.scheduledAssessments = this.allAssessments.filter
              (assessment => scheduled.includes(assessment.referralStatusId));

            this.assessmentRequests = this.allAssessments.filter
              (assessment => !scheduled.includes(assessment.referralStatusId));
          } else {
            this.scheduledAssessments = [];
            this.assessmentRequests = [];
          }

          this.assessmentRequestsLastUpdated = new Date();
          this.closeLoading();
          this.closeRefreshing($event);
        }, error => {
          this.showErrorToast(error)
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
      this.loading.dismiss();
    }
  }

  closeRefreshing($event?: any) {
    if ($event) {
      $event.target.complete();
    }
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
