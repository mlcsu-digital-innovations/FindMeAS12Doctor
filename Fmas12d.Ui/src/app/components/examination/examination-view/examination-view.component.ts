import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { ParamMap, ActivatedRoute } from '@angular/router';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { ReferralView } from 'src/app/interfaces/referral-view';
import { switchMap, map, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';
import * as moment from 'moment';
import { RouterService } from 'src/app/services/router/router.service';

@Component({
  selector: 'app-examination-view',
  templateUrl: './examination-view.component.html',
  styleUrls: ['./examination-view.component.css']
})
export class ExaminationViewComponent implements OnInit {

  currentExaminationForm: FormGroup;
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

    this.currentExaminationForm = this.formBuilder.group({
      amhpUserName: [
        ''
      ],
      currentExamination: [
        ''
      ],
      doctorNamesAccepted: [
        ''
      ],
      doctorNamesAllocated: [
        ''
      ],
      examinationDetails: [
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

  EditExamination() {
    this.routerService.navigateByUrl(`/examination/edit/${this.referralId}`);
  }

  InitialiseForm(referral: ReferralView) {
    this.currentExaminationForm.controls.amhpUserName.setValue(referral.currentExamination.amhpUserName);
    this.currentExaminationForm.controls.doctorNamesAccepted.setValue(referral.currentExamination.doctorNamesAccepted);
    this.currentExaminationForm.controls.doctorNamesAllocated.setValue(referral.currentExamination.doctorNamesAllocated);
    // this.currentExaminationForm.controls.examinationDetails.setValue(referral.currentExamination.examinationDetails);
    this.currentExaminationForm.controls.fullAddress.setValue(referral.currentExamination.fullAddress);
    this.currentExaminationForm.controls.meetingArrangementComment.setValue(referral.currentExamination.meetingArrangementComment);

    const mustBeCompletedBy = moment(referral.currentExamination.mustBeCompletedBy).format('DD/MM/YYYY HH:mm');

    this.currentExaminationForm.controls.mustBeCompletedBy.setValue(mustBeCompletedBy);
    this.currentExaminationForm.controls.postCode.setValue(referral.currentExamination.postcode);
    this.currentExaminationForm.controls.preferredDoctorGenderTypeName.setValue(referral.currentExamination.preferredDoctorGenderTypeName);
    this.currentExaminationForm.controls.specialityName.setValue(referral.currentExamination.specialityName);
    this.currentExaminationForm.disable();
    this.referralId = referral.id;

    console.log(referral);
  }
}
