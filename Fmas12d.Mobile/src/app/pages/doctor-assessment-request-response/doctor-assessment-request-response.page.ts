import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { AmhpAssessmentView } from 'src/app/models/amhp-assessment-view.model';
import { AssessmentRequestDetails } from 'src/app/models/assessment-request-details.model';
import { AssessmentSelectedDoctor } from 'src/app/models/assessment-selected-doctor.model';
import { Component, OnInit } from '@angular/core';
import { KnownLocation } from 'src/app/models/known-location.model';
import { LoadingController, ToastController, NavController } from '@ionic/angular';
import { UserDetails } from 'src/app/interfaces/user-details';
import { UserDetailsService } from 'src/app/services/user-details/user-details.service';

@Component({
  selector: 'app-doctor-assessment-request-response',
  templateUrl: './doctor-assessment-request-response.page.html',
  styleUrls: ['./doctor-assessment-request-response.page.scss'],
})
export class DoctorAssessmentRequestResponsePage implements OnInit {

  
  public alreadyAllocated: boolean;
  private assessmentId: number;
  public assessmentRequest: AssessmentRequestDetails;
  public expectedLocation: string;
  private loading: HTMLIonLoadingElement;
  private userDetails: UserDetails;

  constructor(
    private assessmentService: AmhpAssessmentService,
    private loadingController: LoadingController,
    private navController: NavController,
    private route: ActivatedRoute,
    private router: Router,
    private toastController: ToastController,
    private userDetailsService: UserDetailsService
  ) { }

  ngOnInit() {
    this.assessmentId = +this.route.snapshot.paramMap.get('id');
    const request = this.assessmentService.getView(this.assessmentId);

    this.assessmentRequest = new AssessmentRequestDetails();
    this.assessmentRequest.doctorDetails = new AssessmentSelectedDoctor();
    this.assessmentRequest.doctorDetails.knownLocation = new KnownLocation();

    this.userDetails = this.userDetailsService.fetchUserDetails();

    this.showLoading();

    request
      .subscribe(
        (result: AmhpAssessmentView) => {

          this.assessmentRequest.dateTime = result.dateTime;
          this.assessmentRequest.postcode = result.postcode;
          this.assessmentRequest.id = result.id;
          this.assessmentRequest.detailTypes = result.detailTypes;
          this.assessmentRequest.amhpUserName = result.amhpUserName;
          this.assessmentRequest.isPlanned = result.isPlanned;

          // user could be allocated OR selected
          if (
            this.userDetails &&
            result.doctorsSelected !== null &&
            result.doctorsSelected.map(ds => ds.doctorId).includes(this.userDetails.id)
            ) {
              this.assessmentRequest.doctorDetails =
              result.doctorsSelected.filter(doctor => doctor.doctorId === this.userDetails.id)[0];
          }

          if (
            this.userDetails &&
            result.doctorsAllocated !== null &&
            result.doctorsAllocated.map(ds => ds.doctorId).includes(this.userDetails.id)
            ) {
              this.assessmentRequest.doctorDetails =
              result.doctorsAllocated.filter(doctor => doctor.doctorId === this.userDetails.id)[0];
          }

          this.alreadyAllocated = result.doctorsAllocated === null
            ? false
            : result.doctorsAllocated
              .find(doctor => doctor.doctorId === this.userDetails.id) !== undefined;

          this.expectedLocation
            = this.assessmentRequest.doctorDetails.knownLocation.contactDetailTypeName == null
            ? this.assessmentRequest.doctorDetails.knownLocation.postcode
            : this.assessmentRequest.doctorDetails.knownLocation.contactDetailTypeName;

          this.closeLoading();
        }, error => {
          this.showErrorToast('Unable to retrieve assessment details');
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

    this.router.navigate(['/doctor-assessment-accept-request'], navigationExtras);
  }

  closeLoading() {
    if (this.loading) {
      setTimeout(() => { this.loading.dismiss(); }, 500);
    }
  }

  declineRequest() {

    this.assessmentService.declineAssessmentRequest(this.assessmentId)
      .subscribe(
        result => {
          this.showSuccessToast('Request declined');
          this.router.navigateByUrl('/doctor-assessments');
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
