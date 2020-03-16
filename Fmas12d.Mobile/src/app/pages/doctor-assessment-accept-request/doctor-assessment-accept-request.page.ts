import { AssessmentRequestDetails } from 'src/app/models/assessment-request-details.model';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastController, NavController } from '@ionic/angular';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { Geolocation } from '@ionic-native/geolocation/ngx';
import { ContactDetailService } from 'src/app/services/contact-details/contact-detail.service';
import { NameId } from 'src/app/interfaces/name-id.interface';

@Component({
  selector: 'app-doctor-assessment-accept-request',
  templateUrl: './doctor-assessment-accept-request.page.html',
  styleUrls: ['./doctor-assessment-accept-request.page.scss'],
})
export class DoctorAssessmentAcceptRequestPage implements OnInit {

  public assessmentRequest: AssessmentRequestDetails;
  public currentLocationId: number;
  public latitude: number;
  public longitude: number;
  public contactDetails: NameId[] = [];

  constructor(
    private assessmentService: AmhpAssessmentService,
    private contactDetailService: ContactDetailService,
    private geolocation: Geolocation,
    private navController: NavController,
    private route: ActivatedRoute,
    private router: Router,
    private toastController: ToastController
  ) {
    this.route.queryParams.subscribe(
      params => {
        if (this.router.getCurrentNavigation().extras.state) {
          this.assessmentRequest = this.router.getCurrentNavigation().extras.state.assessment;
          this.getContactDetails();
        } else {
          this.assessmentRequest = new AssessmentRequestDetails();
          // show an error page !
        }
      }
    );
  }

  ngOnInit() {

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
        this.assessmentRequest.id
      )
      .subscribe(
        result => {
          this.showSuccessToast('Request accepted');
          this.router.navigateByUrl('/doctor-assessments');
        },
        error => {
          this.showErrorToast('Unable to accept request');
        });
    }

    if (this.currentLocationId) {
      this.assessmentService.acceptRequestByContactDetail(
        this.assessmentRequest.id,
        this.currentLocationId
      )
      .subscribe(
        result => {
          this.showSuccessToast('Request accepted');
          this.router.navigateByUrl('/doctor-assessments');
        },
        error => {
          this.showErrorToast('Unable to accept request');
        });
    }

    if (this.latitude && this.longitude) {
      this.assessmentService.acceptRequestByCoordinates(
        this.assessmentRequest.id,
        this.latitude,
        this.longitude
      )
      .subscribe(
        result => {
          this.showSuccessToast('Request accepted');
          this.router.navigateByUrl('/doctor-assessments');
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

  getContactDetails() {
    this.contactDetailService.getContactDetailsForAssessment(this.assessmentRequest.id)
      .subscribe(
        result => {
          if (result !== null) {
            result.forEach(contact => {
              this.contactDetails.push({id: contact.contactDetails[0].id, name: contact.name});
            });
          }
        }, error => {
          this.showErrorToast('Unable to retrieve contact details for user');
        }
      );
  }

  getCoordinates() {
    this.geolocation.getCurrentPosition().then(resp => {
      this.latitude = resp.coords.latitude;
      this.longitude = resp.coords.longitude;
      this.currentLocationId = undefined;
    }, error => {
      this.showErrorToast('Unable to obtain current location for user');
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
