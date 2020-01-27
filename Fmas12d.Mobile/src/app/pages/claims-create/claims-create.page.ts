import {  LoadingController } from '@ionic/angular';
import { ActivatedRoute, Router } from '@angular/router';
import { AssessmentClaimService } from 'src/app/services/assessment-claims/assessment-claims.service';
import { AssessmentContact } from 'src/app/models/assessment-contact.model';
import { Component, OnInit } from '@angular/core';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserAssessmentClaimRequest } from 'src/app/models/user-assessment-claim-request.model';
import { UserAssessmentClaimResponse } from 'src/app/models/user-assessment-claim-response.model';

@Component({
  selector: 'app-claims-create',
  templateUrl: './claims-create.page.html',
  styleUrls: ['./claims-create.page.scss'],
})
export class ClaimsCreatePage implements OnInit {

  private loading: HTMLIonLoadingElement;
  public assessment: AssessmentContact;
  public assessmentId: number;
  public assessmentLastUpdated: Date;
  public claim = {} as UserAssessmentClaimRequest;
  public claimResponse: UserAssessmentClaimResponse;
  public differentReturnDestination: boolean;
  public endLocationId: number;
  public hasValidClaim: boolean;
  public ownPatient: boolean;
  public startLocationId: number;

  constructor(
    private assessmentClaimService: AssessmentClaimService,
    private loadingController: LoadingController,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService
  ) { }

  ngOnInit() {
    this.assessmentId = +this.route.snapshot.paramMap.get('assessmentId');

    if (this.assessmentId) {
      const request = this.assessmentClaimService.getAssessmentAndLocations(this.assessmentId);
      this.showLoading();

      request.subscribe((result: AssessmentContact) => {
        this.assessmentLastUpdated = new Date();
        this.assessment = result;
        
        console.log(result);

        this.closeLoading();
      }, error => {
        this.closeLoading();
        this.toastService.displayError({
          message: 'Unable to retrieve assessment details'
        });
      });
    }
  }

  closeLoading() {
    if (this.loading) {
      this.loading.dismiss();
    }
  }

  confirmClaim() {
    console.log(this.claim);
    this.assessmentClaimService.confirmClaim(this.assessmentId, this.claim)
    .subscribe((result: UserAssessmentClaimResponse) => {
      this.claimResponse = result;
      this.hasValidClaim = true;
      this.toastService.displaySuccess({
        message: 'Claim Submitted'
      });
      this.router.navigateByUrl('/home');
    },
    error => {
      this.closeLoading();
      this.toastService.displayError({
        message: 'Unable to confirm claim'
      });
    });
  }

  validateClaim() {
    this.claim.assessmentId = this.assessmentId;
    this.claim.ownPatient = this.ownPatient || false;

    if (this.startLocationId === 0 || this.endLocationId === 0) {
      return;
    }

    const availableContactTypes = this.assessment.userContactDetailTypes;
    this.startLocationId = +this.startLocationId;

    this.endLocationId =
      this.differentReturnDestination ? +this.endLocationId : this.startLocationId;

    const startContact =
      availableContactTypes.find(cd => cd.id === this.startLocationId);
    const endContact =
      availableContactTypes.find(cd => cd.id === this.endLocationId);

    this.claim.startPostcode = startContact.contactDetails[0].postcode;
    this.claim.endPostcode = endContact.contactDetails[0].postcode;

    this.assessmentClaimService.validateClaim(this.assessmentId, this.claim)
    .subscribe((result: UserAssessmentClaimResponse) => {
      console.log(result);
      this.claimResponse = result;
      this.hasValidClaim = true;
    },
    error => {
      this.closeLoading();
      this.toastService.displayError({
        message: 'Unable to create claim'
      });
    });

    console.log(this.claim);

  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines',
      duration: 5000
    });
    await this.loading.present();
  }

}
