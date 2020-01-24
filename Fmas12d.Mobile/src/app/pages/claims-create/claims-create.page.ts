import { ActivatedRoute } from '@angular/router';
import { AmhpAssessmentView } from 'src/app/models/amhp-assessment-view.model';
import { AssessmentClaimService } from 'src/app/services/assessment-claims/assessment-claims.service';
import { AssessmentContact } from 'src/app/models/assessment-contact.model';
import { Component, OnInit } from '@angular/core';
import { NavController, LoadingController } from '@ionic/angular';
import { ToastService } from 'src/app/services/toast/toast.service';
import { InitialClaimRequest } from 'src/app/models/initial-claim-request.model';
import { InitialClaimResponse } from 'src/app/models/initial-claim-response.model';

@Component({
  selector: 'app-claims-create',
  templateUrl: './claims-create.page.html',
  styleUrls: ['./claims-create.page.scss'],
})
export class ClaimsCreatePage implements OnInit {

  public assessmentLastUpdated: Date;
  public assessment: AssessmentContact;
  public assessmentId: number;
  private loading: HTMLIonLoadingElement;
  public startLocationId: number;
  public endLocationId: number;
  public differentReturnDestination: boolean;
  public ownPatient: boolean;
  public hasValidClaim: boolean;

  constructor(
    private route: ActivatedRoute,
    private assessmentClaimService: AssessmentClaimService,
    private navCtrl: NavController,
    private loadingController: LoadingController,
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
    
  }

  registerClaim() {
    const claim = {} as InitialClaimRequest;
    claim.assessmentId = this.assessmentId;
    claim.ownPatient = this.ownPatient || false;

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

    claim.startPostcode = startContact.contactDetails[0].postcode;
    claim.endPostcode = endContact.contactDetails[0].postcode;

    this.assessmentClaimService.validateClaim(this.assessmentId, claim)
    .subscribe((result: InitialClaimResponse) => {
      console.log(result);
      this.hasValidClaim = true;
    });

    console.log(claim);

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
