import { Component, OnInit } from '@angular/core';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { LoadingController, ToastController, NavController } from '@ionic/angular';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';
import { AmhpAssessmentView } from 'src/app/models/amhp-assessment-view.model';
import { AssessmentRequestDetails } from 'src/app/models/assessment-request-details.model';
import { AssessmentSelectedDoctor } from 'src/app/models/assessment-selected-doctor.model';
import { KnownLocation } from 'src/app/models/known-location.model';
import { UserDetailsService } from 'src/app/services/user-details/user-details.service';
import { UserDetails } from 'src/app/interfaces/user-details';

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

          if (this.userDetails && result.doctorsSelected !== null) {
            this.assessmentRequest.doctorDetails =
            result.doctorsSelected.filter(doctor => doctor.doctorId === this.userDetails.id)[0];
          }

          this.alreadyAllocated =
            result.doctorsAllocated.find(doctor => doctor.doctorId === this.userDetails.id) !== null;

          console.log(result.doctorsAllocated.find(doctor => doctor.doctorId === this.userDetails.id));

          this.expectedLocation
            = this.assessmentRequest.doctorDetails.knownLocation.contactDetailTypeName == null
            ? this.assessmentRequest.doctorDetails.knownLocation.postcode
            : this.assessmentRequest.doctorDetails.knownLocation.contactDetailTypeName;
          console.log(result);
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
      this.loading.dismiss();
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
      spinner: 'lines',
      duration: 5000
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
