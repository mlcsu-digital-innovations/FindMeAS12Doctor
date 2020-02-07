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
  selector: 'app-doctor-claim-view',
  templateUrl: './doctor-claim-view.component.html',
  styleUrls: ['./doctor-claim-view.component.css']
})
export class DoctorClaimViewComponent implements OnInit {

  claimForm: FormGroup;
  claimId: number;
  claim$: Observable<ClaimView | any>;

  constructor(
    private formBuilder: FormBuilder,
    private claimsService: FinanceClaimService,
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
                console.log(claim);
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
      assessmentAddress: [{value: '', disabled: true}],
      assessmentDate: [{value: '', disabled: true}],
      assessmentPayment: [{value: '', disabled: true}],
      mileagePayment: [{value: '', disabled: true}]
    });

  }

  InitialiseForm(claim: ClaimView) {
    console.log(claim);

    this.claimId = claim.id;
    this.claimForm.controls['claimReference'].setValue(claim.claimReference);
    this.claimForm.controls['claimStatus'].setValue(claim.claimStatus.name);
    this.claimForm.controls['ccg'].setValue(claim.ccg.name);
    this.claimForm.controls['assessmentPayment'].setValue(claim.assessmentPayment);
    this.claimForm.controls['mileagePayment'].setValue(claim.mileagePayment);

    let address = claim.assessment.address1;
    address += claim.assessment.address2 == null ? '' : `, ${claim.assessment.address2}`;
    address += claim.assessment.address3 == null ? '' : `, ${claim.assessment.address3}`;
    address += claim.assessment.address4 == null ? '' : `, ${claim.assessment.address4}`;
    address += claim.assessment.postcode == null ? '' : `, ${claim.assessment.postcode}`;

    console.log(address);

    this.claimForm.controls['assessmentAddress'].setValue(address);
    this.claimForm.controls['assessmentDate'].setValue(moment(claim.assessment.scheduledTime).format('DD MMM YYYY HH:mm'));

  }
}
