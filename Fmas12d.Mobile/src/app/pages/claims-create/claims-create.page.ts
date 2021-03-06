import { ActivatedRoute, Router } from '@angular/router';
import { AssessmentClaimService } from 'src/app/services/assessment-claims/assessment-claims.service';
import { AssessmentContact } from 'src/app/models/assessment-contact.model';
import { AssessmentLocation } from 'src/app/models/assessment-location.model';
import { Component, OnInit } from '@angular/core';
import { LoadingController, NavController } from '@ionic/angular';
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
  public hasValidClaim: boolean;
  public withinContract: boolean;
  public startLocation = {} as AssessmentLocation;
  public endLocation: AssessmentLocation;

  public startLocations: AssessmentLocation[] = [];
  public endLocations: AssessmentLocation[] = [];

  constructor(
    private assessmentClaimService: AssessmentClaimService,
    private loadingController: LoadingController,
    private route: ActivatedRoute,
    private router: Router,
    private navController: NavController,
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

        this.assessment.userContactDetailTypes.forEach(cd => {
          this.startLocations.push(
            {
              address1: cd.name,
              postcode: cd.contactDetails[0].postcode,
              isContactDetail: true
            } as AssessmentLocation
          );
          this.endLocations.push(
            {
              address1: cd.name,
              postcode: cd.contactDetails[0].postcode,
              isContactDetail: true
            } as AssessmentLocation
          );
        });

        result.previousAssessmentLocations.forEach(al => {
          this.startLocations.push(al);
        });

        result.nextAssessmentLocations.forEach(al => {
          this.endLocations.push(al);
        });

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
      setTimeout(() => { this.loading.dismiss(); }, 500);
    }
  }

  confirmClaim() {
    this.assessmentClaimService.confirmClaim(this.assessmentId, this.claim)
    .subscribe((result: UserAssessmentClaimResponse) => {
      this.claimResponse = result;
      this.closeLoading();
      this.hasValidClaim = true;
      this.toastService.displaySuccess({
        message: 'Claim Submitted'
      });
      this.router.navigateByUrl('/claims-list');
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

    if ((this.startLocation.postcode === undefined) ||
       (this.differentReturnDestination && this.endLocation.postcode === undefined)) {
      return;
    }

    // user must answer contractual requirements 
    if (this.withinContract === undefined) {
      return;
    }

    this.claim.withinContract = this.withinContract;

    this.claim.startPostcode = this.startLocation.postcode;
    this.claim.endPostcode =
      this.differentReturnDestination ? this.endLocation.postcode : this.startLocation.postcode;

    this.claim.previousAssessmentId =
      this.startLocation.isContactDetail ? null : this.startLocation.id;

    if (this.differentReturnDestination) {
      this.claim.nextAssessmentId = this.endLocation.isContactDetail ? null : this.endLocation.id;
    }

    this.assessmentClaimService.validateClaim(this.assessmentId, this.claim)
    .subscribe((result: UserAssessmentClaimResponse) => {
      this.claimResponse = result;
      this.hasValidClaim = true;
    },
    error => {
      this.closeLoading();
      this.toastService.displayError({
        message: 'Unable to create claim'
      });
    });
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines'
    });
    await this.loading.present();
  }

}
