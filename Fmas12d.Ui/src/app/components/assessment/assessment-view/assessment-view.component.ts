import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { ParamMap, ActivatedRoute } from '@angular/router';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { ReferralView } from 'src/app/interfaces/referral-view';
import { RouterService } from 'src/app/services/router/router.service';
import { switchMap, map, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';
import * as moment from 'moment';

@Component({
  selector: 'app-assessment-view',
  templateUrl: './assessment-view.component.html',
  styleUrls: ['./assessment-view.component.css']
})
export class AssessmentViewComponent implements OnInit {

  currentAssessmentForm: FormGroup;
  isPatientIdValidated: boolean;
  pageSize: number;
  referral$: Observable<Referral | any>;
  referralCreated: Date;
  referralId: number;

  constructor(
    private formBuilder: FormBuilder,
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService,
  ) { }

  ngOnInit() {
    this.referral$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.referralService.getReferralView(+params.get('referralId'))
            .pipe(
              map(referral => {
                this.InitialiseForm(referral);
                return referral;
              })
            );
        }
      ),
      catchError((err) => {
        console.log(err);
        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving Referral Information'
        });

        const emptyReferral = {} as Referral;

        return of(emptyReferral);
      })
    );

    this.currentAssessmentForm = this.formBuilder.group({
      amhpUserName: [
        ''
      ],
      currentAssessment: [
        ''
      ],
      doctorNamesAccepted: [
        ''
      ],
      doctorNamesAllocated: [
        ''
      ],
      assessmentDetails: [
        ''
      ],
      fullAddress: [
        ''
      ],
      meetingArrangementComment: [
        ''
      ],
      mustBeCompletedBy: [
        ''
      ],
      postCode: [
        ''
      ],
      preferredDoctorGenderTypeName: [
        ''
      ],
      specialityName: [
        ''
      ],
    });
  }

  CancelView() {
    this.routerService.navigatePrevious();
  }

  EditAssessment() {
    this.routerService.navigateByUrl(`/assessment/edit/${this.referralId}`);
  }

  InitialiseForm(referral: ReferralView) {
    this.currentAssessmentForm.controls.amhpUserName.setValue(referral.currentAssessment.amhpUserName);
    this.currentAssessmentForm.controls.doctorNamesAccepted.setValue(referral.currentAssessment.doctorsSelected);
    this.currentAssessmentForm.controls.doctorNamesAllocated.setValue(referral.currentAssessment.doctorsAllocated);
    // this.currentAssessmentForm.controls.assessmentDetails.setValue(referral.currentAssessment.assessmentDetails);
    this.currentAssessmentForm.controls.fullAddress.setValue(referral.currentAssessment.fullAddress);
    this.currentAssessmentForm.controls.meetingArrangementComment.setValue(referral.currentAssessment.meetingArrangementComment);

    const mustBeCompletedBy = moment(referral.currentAssessment.mustBeCompletedBy).format('DD/MM/YYYY HH:mm');

    this.currentAssessmentForm.controls.mustBeCompletedBy.setValue(mustBeCompletedBy);
    this.currentAssessmentForm.controls.postCode.setValue(referral.currentAssessment.postcode);
    this.currentAssessmentForm.controls.preferredDoctorGenderTypeName.setValue(referral.currentAssessment.preferredDoctorGenderType.name);
    this.currentAssessmentForm.controls.specialityName.setValue(referral.currentAssessment.speciality.name);
    this.currentAssessmentForm.disable();
    this.referralId = referral.id;

    console.log(referral);
  }
}
