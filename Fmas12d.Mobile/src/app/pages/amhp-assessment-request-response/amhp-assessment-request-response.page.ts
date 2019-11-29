import { Component, OnInit } from '@angular/core';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { LoadingController, ToastController, NavController } from '@ionic/angular';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';
import { AmhpAssessmentView } from 'src/app/models/amhp-assessment-view.model';
import { AmhpAssessmentRequestDetails } from 'src/app/models/amhp-assessment-request-details.model';
import { AmhpAssessmentSelectedDoctor } from 'src/app/models/amhp-assessment-selected-doctor.model';
import { KnownLocation } from 'src/app/models/known-location.model';

@Component({
  selector: 'app-amhp-assessment-request-response',
  templateUrl: './amhp-assessment-request-response.page.html',
  styleUrls: ['./amhp-assessment-request-response.page.scss'],
})
export class AmhpAssessmentRequestResponsePage implements OnInit {

  private assessmentId: number;
  private loading: HTMLIonLoadingElement;
  public assessmentRequest: AmhpAssessmentRequestDetails;

  constructor(
    private assessmentService: AmhpAssessmentService,
    private loadingController: LoadingController,
    private navController: NavController,
    private route: ActivatedRoute,
    private router: Router,
    private toastController: ToastController
  ) { }

  ngOnInit() {
    this.assessmentId = +this.route.snapshot.paramMap.get('id');
    const request = this.assessmentService.getView(this.assessmentId);

    this.assessmentRequest = new AmhpAssessmentRequestDetails();
    this.assessmentRequest.doctorDetails = new AmhpAssessmentSelectedDoctor();
    this.assessmentRequest.doctorDetails.knownLocation = new KnownLocation();

    this.showLoading();

    request
      .subscribe(
        (result: AmhpAssessmentView) => {
          console.log(result);
          this.assessmentRequest.dateTime = result.dateTime;
          this.assessmentRequest.postcode = result.postcode;
          this.assessmentRequest.id = result.id;
          this.assessmentRequest.detailTypes = result.detailTypes;
          this.assessmentRequest.doctorDetails =
            result.doctorsSelected.find(doctor => doctor.doctorId === 4);

          console.log(this.assessmentRequest);

          this.closeLoading();
        }, error => {
          this.closeLoading();
        }
      );
  }

  acceptRequest() {
    const navigationExtras: NavigationExtras = {
      state: {
      assessment: this.assessmentRequest
      }
    };

    this.router.navigate(['/amhp-assessment-accept-request'], navigationExtras);
  }

  closeLoading() {
    if (this.loading) {
      this.loading.dismiss();
    }
  }

  declineRequest() {
    console.log('decline request');

    this.assessmentService.declineAssessmentRequest(this.assessmentId, this.assessmentRequest.doctorDetails.doctorId)
      .subscribe(
        result => {
          this.showSuccessToast('Request declined');
          this.navController.back();
        },
        error => {
          this.showErrorToast('Unable to decline request');
        });
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines'
    });
    await this.loading.present();
  }

  showErrorToast(msg: string) {
    this.showToast(msg, 'danger', 'Error!');
  }
  showSuccessToast(msg: string) {
    this.showToast(msg, 'success', 'Success');
  }

  async showToast(msg: string, colour: string, hdr: string) {
    const toast = await this.toastController.create({
      message: msg,
      header: hdr,
      color: colour,
      duration: 2000,
      position: 'top'
    });
    await toast.present();
  }

}
