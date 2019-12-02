import { AmhpAssessmentRequestDetails } from 'src/app/models/amhp-assessment-request-details.model';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastController, NavController } from '@ionic/angular';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { Geolocation } from '@ionic-native/geolocation/ngx';

@Component({
  selector: 'app-amhp-assessment-accept-request',
  templateUrl: './amhp-assessment-accept-request.page.html',
  styleUrls: ['./amhp-assessment-accept-request.page.scss'],
})
export class AmhpAssessmentAcceptRequestPage implements OnInit {

  public assessmentRequest: AmhpAssessmentRequestDetails;
  public currentLocationId: number;
  public latitude: number;
  public longitude: number;

  constructor(
    private assessmentService: AmhpAssessmentService,
    private geolocation: Geolocation,
    private navController: NavController,
    private route: ActivatedRoute,
    private router: Router,
    private toastController: ToastController
  ) {
    this.route.queryParams.subscribe(
      params => {
        if (this.router.getCurrentNavigation().extras.state) {
          console.log(this.router.getCurrentNavigation().extras.state.assessment);
          this.assessmentRequest = this.router.getCurrentNavigation().extras.state.assessment;
        } else {
          this.assessmentRequest = new AmhpAssessmentRequestDetails();
          // show an error page !
        }
      }
    );
  }

  ngOnInit() {
    // ToDo: Get users stored locations from a service

  }

  cancelAcceptance() {
    this.navController.back();
  }

  confirmRequest() {

    if (
      this.currentLocationId === undefined &&
      this.latitude === undefined &&
      this.longitude === undefined
    ) {
      this.assessmentService.acceptRequest(
        this.assessmentRequest.id,
        this.assessmentRequest.doctorDetails.doctorId
      )
      .subscribe(
        result => {
          this.showSuccessToast('Request accepted');
          this.router.navigateByUrl('/amhp-assessment-requests');
        },
        error => {
          this.showErrorToast('Unable to accept request');
        });
    }

    if (this.currentLocationId) {
      this.assessmentService.acceptRequestByContactDetail(
        this.assessmentRequest.id,
        this.assessmentRequest.doctorDetails.doctorId,
        this.currentLocationId
      )
      .subscribe(
        result => {
          this.showSuccessToast('Request accepted');
          this.router.navigateByUrl('/amhp-assessment-requests');
        },
        error => {
          this.showErrorToast('Unable to accept request');
        });
    }

    if (this.latitude && this.longitude) {
      this.assessmentService.acceptRequestByCoordinates(
        this.assessmentRequest.id,
        this.assessmentRequest.doctorDetails.doctorId,
        this.latitude,
        this.longitude
      )
      .subscribe(
        result => {
          this.showSuccessToast('Request accepted');
          this.router.navigateByUrl('/amhp-assessment-requests');
        },
        error => {
          this.showErrorToast('Unable to accept request');
        });
    }


  }

  contactChanged() {
    this.latitude = undefined;
    this.longitude = undefined;
  }

  getCoordinates() {
    this.geolocation.getCurrentPosition().then(resp => {
      this.latitude = resp.coords.latitude;
      this.longitude = resp.coords.longitude;
      this.currentLocationId = undefined;
    });
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
