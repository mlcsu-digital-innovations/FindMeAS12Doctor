import { ClaimView } from 'src/app/interfaces/claim-view';
import { Component, OnInit } from '@angular/core';
import { FinanceClaimService } from 'src/app/services/finance-claim/finance-claim.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { ParamMap, ActivatedRoute } from '@angular/router';
import { RouterService } from 'src/app/services/router/router.service';
import { switchMap, map, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';
import * as moment from 'moment';

@Component({
  selector: 'app-claim-view',
  templateUrl: './claim-view.component.html',
  styleUrls: ['./claim-view.component.css']
})
export class ClaimViewComponent implements OnInit {

  claim$: Observable<ClaimView | any>;
  claimForm: FormGroup;
  claimId: number;

  constructor(
    private claimsService: FinanceClaimService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService
  ) { }

  ngOnInit() {

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
          message: 'Error Retrieving Referral Information'
        });

        const emptyClaim = {} as ClaimView;
        return of(emptyClaim);
      })
    );

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

  InitialiseForm(claim: ClaimView) {

    this.claimId = claim.id;
    this.claimForm.controls['claimReference'].setValue(claim.claimReference);
    this.claimForm.controls['claimStatus'].setValue(claim.claimStatus.name);
    this.claimForm.controls['ccg'].setValue(claim.ccg.name);
    this.claimForm.controls['claimant'].setValue(claim.claimant.displayName);
    if (claim.claimant.hasBankDetails) {
      this.claimForm.controls['vsr'].setValue(claim.claimant.bankDetails[0].vsrNumber);
    }
    this.claimForm.controls['assessmentPayment'].setValue(claim.assessmentPayment);
    this.claimForm.controls['mileagePayment'].setValue(claim.mileagePayment);
  }

  SetClaimAsProcessing() {
    this.UpdateClaim(false);
  }

  SetClaimAsQuerying() {
    this.UpdateClaim(true);
  }

  UpdateClaim(isQuery: boolean) {

    let updateRequest: Observable<ClaimView>;

    if (isQuery === true) {
      updateRequest = this.claimsService.updateClaimStatusToQuerying(this.claimId);
    } else {
      updateRequest = this.claimsService.updateClaimStatusToProcessing(this.claimId);
    }

    updateRequest.subscribe(
      () => {
        this.toastService.displaySuccess({
          message: 'Claim Updated'
        });
        this.routerService.navigateByUrl('/finance/claims/list');
      },
      error => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to update claim! Please try again in a few moments'
        });
      }
    );
  }
}
