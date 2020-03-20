import { CLAIM_STATUS_QUERY, CLAIM_STATUS_PROCESSING } from 'src/app/constants/Constants';
import { ClaimView } from 'src/app/interfaces/claim-view';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FinanceClaimService } from 'src/app/services/finance-claim/finance-claim.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { ParamMap, ActivatedRoute } from '@angular/router';
import { RouterService } from 'src/app/services/router/router.service';
import { switchMap, map, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Ccg } from 'src/app/interfaces/ccg';
import { Claimant } from 'src/app/interfaces/claimant';
import { UserProfileService } from 'src/app/services/user-profile/user-profile.service';
import { UserDetails } from 'src/app/interfaces/user-details';
import { UserProfile } from 'src/app/interfaces/user-profile';

@Component({
  selector: 'app-claim-view',
  templateUrl: './claim-view.component.html',
  styleUrls: ['./claim-view.component.css']
})
export class ClaimViewComponent implements OnInit {

  claim$: Observable<ClaimView | any>;
  claimForm: FormGroup;
  claimId: number;
  vsrModal: NgbModalRef;
  claimant: Claimant;
  ccg: Ccg;

  @ViewChild('updateVsr', null) vsrTemplate;

  constructor(
    private claimsService: FinanceClaimService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService,
    private userProfileService: UserProfileService
  ) { }

  ngOnInit() {

    this.GetData();

    this.claimForm = this.formBuilder.group({
      claimReference: [{ value: '', disabled: true }],
      claimStatus: [{ value: '', disabled: true }],
      ccg: [{ value: '', disabled: true }],
      claimant: [{ value: '', disabled: true }],
      vsr: [{ value: '', disabled: true }],
      assessmentPayment: [{value: '', disabled: true}],
      mileagePayment: [{value: '', disabled: true}]
    });

  }

  AddVsr() {
    this.vsrModal = this.modalService.open(
      this.vsrTemplate,
      { size: 'lg' }
    );
  }

  GetData() {
    this.claim$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.claimsService.getClaimView(+params.get('claimId'))
            .pipe(
              map(claim => {
                this.InitialiseForm(claim);
                return claim;
              })
            );
        }
      ),
      catchError((err) => {

        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving Claim Information'
        });

        const emptyClaim = {} as ClaimView;
        return of(emptyClaim);
      })
    );
  }

  InitialiseForm(claim: ClaimView) {

    this.claimId = claim.id;
    this.claimForm.controls.claimReference.setValue(claim.claimReference);
    this.claimForm.controls.claimStatus.setValue(claim.claimStatus.name);
    this.claimForm.controls.ccg.setValue(claim.ccg.name);
    this.claimForm.controls.claimant.setValue(claim.claimant.displayName);
    if (claim.claimant.hasBankDetails) {
      this.claimForm.controls.vsr.setValue(claim.claimant.bankDetails[0].vsrNumber);
    }
    this.claimForm.controls.assessmentPayment.setValue(claim.assessmentPayment);
    this.claimForm.controls.mileagePayment.setValue(claim.mileagePayment);

    this.claimant = claim.claimant;
    this.ccg = claim.ccg;
  }

  IsProcessing(claim: ClaimView): boolean {
    return claim.claimStatus.id === CLAIM_STATUS_PROCESSING;
  }

  IsQuerying(claim: ClaimView): boolean {
    return claim.claimStatus.id === CLAIM_STATUS_QUERY;
  }

  OnUpdateVsrAction(action: {ok: boolean, vsr: string}) {

    this.vsrModal.close();

    if (action.ok) {
      this.userProfileService.UpdateUserVsrNumber(this.claimant.id, parseInt(action.vsr, 10), this.ccg.id)
      .subscribe((result: UserProfile) => {
        this.toastService.displaySuccess({
          message: 'VSR number Updated'
        });
        this.UpdateClaimStatus(this.claimsService.updateClaimStatusToSubmitted(this.claimId), false);
        setTimeout(() => this.GetData(), 1000);
      }, err => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to update VSR number! Please try again in a few moments'
        });
      });
    }
  }

  SetClaimAsProcessing() {
    this.UpdateClaim(false);
  }

  SetClaimAsQuerying() {
    this.UpdateClaim(true);
  }

  UpdateClaimStatus(updateRequest: Observable<ClaimView>, navigateToList: boolean) {
    updateRequest.subscribe(
      () => {
        this.toastService.displaySuccess({
          message: 'Claim Updated'
        });
        if (navigateToList){
          this.routerService.navigateByUrl('/finance/claims/list');
        }
      },
      error => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to update claim! Please try again in a few moments'
        });
      }
    );
  }

  UpdateClaim(isQuery: boolean) {

    let updateRequest: Observable<ClaimView>;

    if (isQuery === true) {
      updateRequest = this.claimsService.updateClaimStatusToQuerying(this.claimId);
    } else {
      updateRequest = this.claimsService.updateClaimStatusToProcessing(this.claimId);
    }
    this.UpdateClaimStatus(updateRequest, true);
  }
}
