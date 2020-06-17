import { ClaimView } from 'src/app/interfaces/claim-view';
import { Component, OnInit } from '@angular/core';
import { DoctorClaimService } from 'src/app/services/doctor-claim/doctor-claim-service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { ParamMap, ActivatedRoute } from '@angular/router';
import { switchMap, map, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';
import * as moment from 'moment';

@Component({
  selector: 'app-doctor-claim-view',
  templateUrl: './doctor-claim-view.component.html',
  styleUrls: ['./doctor-claim-view.component.css']
})
export class DoctorClaimViewComponent implements OnInit {

  claim$: Observable<ClaimView | any>;
  claimForm: FormGroup;
  claimId: number;

  constructor(
    private claimsService: DoctorClaimService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
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
      assessmentAddress: [{value: '', disabled: true}],
      assessmentDate: [{value: '', disabled: true}],
      assessmentOutcome: [{value: '', disabled: true}],
      assessmentPayment: [{value: '', disabled: true}],
      ccg: [{ value: '', disabled: true }],
      claimReference: [{ value: '', disabled: true }],
      claimStatus: [{ value: '', disabled: true }],
      mileagePayment: [{value: '', disabled: true}]
    });

  }

  InitialiseForm(claim: ClaimView) {

    this.claimId = claim.id;

    let address = claim.assessment.address1;
    address += claim.assessment.address2 == null ? '' : `, ${claim.assessment.address2}`;
    address += claim.assessment.address3 == null ? '' : `, ${claim.assessment.address3}`;
    address += claim.assessment.address4 == null ? '' : `, ${claim.assessment.address4}`;
    address += claim.assessment.postcode == null ? '' : `, ${claim.assessment.postcode}`;

    this.claimForm.patchValue({
      claimReference: claim.claimReference,
      claimStatus: claim.claimStatus.name,
      assessmentPayment: claim.assessmentPayment,
      mileagePayment: claim.mileagePayment,
      ccg: claim.assessment.ccg.name,
      assessmentAddress: address,
      assessmentDate: moment(claim.assessment.scheduledTime).format('DD MMM YYYY HH:mm'),
      assessmentOutcome: claim.assessment.isSuccessful ?
        'Successful Assessment' : 'Unsuccessful Assessment'
    });
  }
}
