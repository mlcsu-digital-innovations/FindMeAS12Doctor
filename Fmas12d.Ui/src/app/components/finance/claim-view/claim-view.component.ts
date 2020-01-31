import { ClaimView } from 'src/app/interfaces/claim-view';
import { Component, OnInit, Renderer2, ViewChild } from '@angular/core';
import { FinanceClaimService } from 'src/app/services/finance-claim/finance-claim.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GpPracticeListService } from 'src/app/services/gp-practice-list/gp-practice-list.service';
import { NgbModalRef, NgbModal, NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of, throwError } from 'rxjs';
import { ParamMap, ActivatedRoute, Router } from '@angular/router';
import { PatientService } from 'src/app/services/patient/patient.service';
import { RouterService } from 'src/app/services/router/router.service';
import { switchMap, map, catchError, tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';
import * as moment from 'moment';

@Component({
  selector: 'app-claim-view',
  templateUrl: './claim-view.component.html',
  styleUrls: ['./claim-view.component.css']
})
export class ClaimViewComponent implements OnInit {

  claimForm: FormGroup;
  claimId: number;
  claim$: Observable<ClaimView | any>;

  constructor(
    private formBuilder: FormBuilder,
    private claimsService: FinanceClaimService,
    private gpPracticeListService: GpPracticeListService,
    private modalService: NgbModal,
    private patientService: PatientService,
    private renderer: Renderer2,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService
  ) { }

  @ViewChild('patientResults', {static: true}) patientResultTemplate;
  @ViewChild('cancelUpdate', null) cancelUpdateTemplate;
  @ViewChild('confirmClosure', null) closeTemplate;

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
    });

  }

  InitialiseForm(claim: ClaimView) {
    console.log(claim);

    this.claimForm.controls['claimReference'].setValue(claim.claimReference);
    this.claimForm.controls['claimStatus'].setValue(claim.claimStatus.name);
    this.claimForm.controls['ccg'].setValue(claim.ccg.name);
    this.claimForm.controls['claimant'].setValue(claim.claimant.displayName);
    if (claim.claimant.hasBankDetails) {
      this.claimForm.controls['vsr'].setValue(claim.claimant.bankDetails[0].vsrNumber);
    }
  }
}
