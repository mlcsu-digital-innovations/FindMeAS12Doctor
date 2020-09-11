import { AssessmentService } from 'src/app/services/assessment/assessment.service';
import { AssessmentUser } from 'src/app/interfaces/assessment-user';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePickerFormat } from 'src/app/helpers/date-picker.validator';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { NgbModal, NgbModalRef, NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { ParamMap, ActivatedRoute } from '@angular/router';
import { Referral } from 'src/app/interfaces/referral';
import { REFERRAL_STATUS_AWAITING_REVIEW, REFERRAL_STATUS_OPEN, REFERRAL_STATUS_ASSESSMENT_SCHEDULED } from 'src/app/constants/Constants';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { ReferralView } from 'src/app/interfaces/referral-view';
import { RouterService } from 'src/app/services/router/router.service';
import { switchMap, map, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';
import * as moment from 'moment';

@Component({
  selector: 'app-assessment-outcome',
  templateUrl: './assessment-outcome.component.html',
  styleUrls: ['./assessment-outcome.component.css']
})
export class AssessmentOutcomeComponent implements OnInit {

  assessmentConfirmedDate: NgbDateStruct;
  assessmentConfirmedTime: NgbTimeStruct;
  assessmentId: number;
  assessmentOutcomeForm: FormGroup;
  attendingDoctors: AssessmentUser[];
  closeModal: NgbModalRef;
  completeModal: NgbModalRef;
  currentAssessmentForm: FormGroup;
  isInReviewState: boolean;
  isPatientIdValidated: boolean;
  isScheduled: boolean;
  maxDate: NgbDateStruct;
  minDate: NgbDateStruct;
  outcomes: NameIdList[];
  pageSize: number;
  referral$: Observable<ReferralView>;
  referralCreated: Date;
  referralId: number;
  referralStatus: string;
  referralStatusId: number;
  showDateTitle: string;
  showDateValue: Date;

  @ViewChild('confirmClosure', null) closeTemplate;
  @ViewChild('confirmCompletion', null) completeTemplate;

  constructor(
    private assessmentService: AssessmentService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService,
    private nameIdListService: NameIdListService
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
        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving Referral Information'
        });

        const emptyReferral = {} as ReferralView;
        return of(emptyReferral);
      })
    );

    this.nameIdListService.GetListData('unsuccessfulassessmenttype')
      .subscribe(outcomes => {
        this.outcomes = outcomes;

        const successful = {id: 0, name: 'Successful', resultText: 'Successful'} as NameIdList;
        this.outcomes.unshift(successful);
      },
        (err) => {
          this.toastService.displayError({
            title: 'Error',
            message: 'Error Retrieving Outcome Data'
          });
        });

    this.currentAssessmentForm = this.formBuilder.group({
      amhpUserName: [
        ''
      ],
      completedAt: [
        {
          value: '',
          disabled: true
        }
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
      mustBeCompletedBy: [
        ''
      ],
      postCode: [
        ''
      ]
    });

    this.assessmentOutcomeForm = this.formBuilder.group({
      confirmedAssessmentDate: [
        this.assessmentConfirmedDate,
        [
          DatePickerFormat
        ]
      ],
      confirmedAssessmentTime: [this.assessmentConfirmedTime],
      outcome: [ '' ]
    });

  }

  CancelView() {
    this.routerService.navigatePrevious();
  }

  CloseReferral() {
    let forceClose = false;

    if (this.referralStatusId !== REFERRAL_STATUS_AWAITING_REVIEW
        && this.referralStatusId !== REFERRAL_STATUS_OPEN ) {
          forceClose = true;
    }

    this.referralService.closeReferral(this.referralId, forceClose).subscribe(
      () => {
        this.toastService.displaySuccess({
          message: 'Referral closed'
        });
        this.routerService.navigateByUrl('/referral/list');
      },
      error => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to close referral! Please try again in a few moments'
        });
      }
    );

  }

  CloseReferralConfirmation() {
    this.closeModal = this.modalService.open(this.closeTemplate, {
      size: 'lg'
    });
  }

  CompleteReview() {

    this.assessmentService.completeReview(this.assessmentId)
    .subscribe(
      () => {
        this.toastService.displaySuccess({
          message: 'Review complete'
        });
        this.routerService.navigateByUrl('/referral/list');
      },
      error => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to complete review! Please try again in a few moments'
        });
      }
    );

  }

  CompleteReviewConfirmation() {
    this.completeModal = this.modalService.open(this.completeTemplate, {
      size: 'lg'
    });
  }

  ConvertToDateStruct(dateValue: Date): NgbDateStruct {

    const momentDate = moment(dateValue);
    const dateStruct = {} as NgbDateStruct;
    dateStruct.day = momentDate.date();
    dateStruct.month = momentDate.month() + 1;
    dateStruct.year = momentDate.year();

    return dateStruct;
  }

  ConvertToTimeStruct(dateValue: Date): NgbTimeStruct {

    // round up to the next 5 minute interval
    const start = moment(dateValue);
    const remainder = 5 - (start.minute() % 5);
    const adjustment = remainder === 5 ? 0 : remainder;
    const momentDate = moment(start).add(adjustment, 'minutes');
    const timeStruct = {} as NgbTimeStruct;
    timeStruct.hour = momentDate.hour();
    timeStruct.minute = momentDate.minutes();
    timeStruct.second = momentDate.seconds();

    return timeStruct;
  }

  ConfirmOutcome() {

    let hasErrors = false;

    const outcome = this.assessmentOutcomeForm.controls.outcome;

    if (outcome === undefined || outcome.value === '' ) {
      outcome.setErrors({MissingOutcome: true});
      hasErrors = true;
    }

    if (
      this.confirmedAssessmentDateField.value === null ||
      this.confirmedAssessmentDateField.value === '' ||
      this.confirmedAssessmentTimeField.value === null ||
      this.confirmedAssessmentTimeField.value === ''
      ) {
      this.confirmedAssessmentDateField.setErrors({MissingDate: true});
      hasErrors = true;
    }

    if (hasErrors) {
      return;
    }

  }

  CreateDateFromPickerObjects(datePart: NgbDateStruct, timePart: NgbTimeStruct): Date {
    return new Date(
      datePart.year,
      datePart.month - 1,
      datePart.day,
      timePart.hour,
      timePart.minute,
      timePart.second,
      0
    );
  }

  CreateJsonDateFromPickerObjects(datePart: NgbDateStruct, timePart: NgbTimeStruct): string {
    return `${datePart.year}-${datePart.month}-${datePart.day}T${timePart.hour}`;
  }

  EditAssessment() {
    this.routerService.navigateByUrl(`/assessment/edit/${this.referralId}`);
  }

  get confirmedAssessmentDateField() {
    return this.assessmentOutcomeForm.controls.confirmedAssessmentDate;
  }

  get confirmedAssessmentTimeField() {
    return this.assessmentOutcomeForm.controls.confirmedAssessmentTime;
  }

  get outcomeField() {
    return this.assessmentOutcomeForm.controls.outcome;
  }

  InitialiseForm(referral: ReferralView) {

    this.minDate = this.ConvertToDateStruct(referral.createdAt);
    this.maxDate = this.ConvertToDateStruct(new Date());

    this.assessmentId = referral.currentAssessment.id;

    this.currentAssessmentForm.controls.amhpUserName.setValue(
      referral.currentAssessment.amhpUser.displayName
    );
    this.currentAssessmentForm.controls.doctorNamesAccepted.setValue(
      referral.currentAssessment.doctorsSelected
    );
    this.currentAssessmentForm.controls.doctorNamesAllocated.setValue(
      referral.currentAssessment.doctorsAllocated
    );
    this.currentAssessmentForm.controls.fullAddress.setValue(
      referral.currentAssessment.fullAddress
    );
    this.currentAssessmentForm.controls.postCode.setValue(
      referral.currentAssessment.postcode
    );

    this.currentAssessmentForm.disable();
    this.referralId = referral.id;
    this.referralStatusId = referral.referralStatusId;
    this.referralStatus = referral.statusName;

    this.isInReviewState = referral.referralStatusId === REFERRAL_STATUS_AWAITING_REVIEW;
    this.isScheduled = referral.referralStatusId === REFERRAL_STATUS_ASSESSMENT_SCHEDULED;

    if (referral.currentAssessment.scheduledTime !== null) {
      this.showDateTitle = 'Scheduled Date / Time';
      this.showDateValue = referral.currentAssessment.scheduledTime;
    } else {
      this.showDateTitle = 'Must Be Completed By';
      this.showDateValue = referral.currentAssessment.mustBeCompletedBy;
    }
    const mustBeCompletedBy = moment(this.showDateValue).format('DD/MM/YYYY HH:mm');
    this.currentAssessmentForm.controls.mustBeCompletedBy.setValue(mustBeCompletedBy);

    if (referral.currentAssessment.completedAt !== null) {
      const formattedCompletedAt =
        moment(referral.currentAssessment.completedAt).format('DD/MM/YYYY HH:mm');
      this.currentAssessmentForm.controls.completedAt.setValue(formattedCompletedAt);
    }

    this.assessmentConfirmedDate = this.ConvertToDateStruct(this.showDateValue);
    this.confirmedAssessmentDateField.setValue(this.assessmentConfirmedDate);

    this.assessmentConfirmedTime = this.ConvertToTimeStruct(this.showDateValue);
    this.confirmedAssessmentTimeField.setValue(this.assessmentConfirmedTime);

    this.attendingDoctors = referral.currentAssessment.doctorsAllocated;
    this.attendingDoctors.forEach(doctor => { doctor.selected = true; });

  }

  OnModalAction(event: any) {
    this.closeModal.close();
    if (event) {
      this.CloseReferral();
    }
  }

  OnCompletionModalAction(event: any) {
    this.completeModal.close();
    if (event) {
      this.CompleteReview();
    }
  }

  ToggleSelection(id: number, event) {
    console.log(id, event);
    const idx = this.attendingDoctors.findIndex(x => x.id === id);
    this.attendingDoctors[idx].selected = event.target.checked;
  }

}
