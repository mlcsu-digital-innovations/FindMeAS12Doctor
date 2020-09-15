import { AssessmentOutcome } from 'src/app/interfaces/assessment-outcome';
import { AssessmentOutcomeDoctor } from 'src/app/interfaces/assessment-outcome-doctor';
import { AssessmentService } from 'src/app/services/assessment/assessment.service';
import { AssessmentUser } from 'src/app/interfaces/assessment-user';
import { Component, OnInit, ViewChild } from '@angular/core';
import { CurrentAssessment } from 'src/app/interfaces/current-assessment';
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
  assessmentOutcomeDateTime: string;
  assessmentOutcomeForm: FormGroup;
  attendingDoctors: AssessmentUser[];
  confirmModal: NgbModalRef;
  currentAssessment: CurrentAssessment;
  currentAssessmentForm: FormGroup;
  isInReviewState: boolean;
  isPatientIdValidated: boolean;
  isScheduled: boolean;
  maxDate: NgbDateStruct;
  minDate: NgbDateStruct;
  outcomes: NameIdList[];
  pageSize: number;
  patientIdentifier: string;
  referral$: Observable<ReferralView>;
  referralCreated: Date;
  referralId: number;
  referralStatus: string;
  referralStatusId: number;
  selectedOutcome: NameIdList;
  showDateTitle: string;
  showDateValue: Date;

  @ViewChild('confirmOutcome', null) confirmTemplate;

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
    } else {
      this.selectedOutcome = this.outcomes.find(selected => selected.id === parseInt(outcome.value, 10));
    }

    if (
      this.confirmedAssessmentDateField.value === null ||
      this.confirmedAssessmentDateField.value === '' ||
      this.confirmedAssessmentTimeField.value === null ||
      this.confirmedAssessmentTimeField.value === ''
      ) {
      this.confirmedAssessmentDateField.setErrors({MissingDate: true});
      hasErrors = true;
    } else {
      const confirmedDateTime =
        this.CreateDateFromPickerObjects(
          this.confirmedAssessmentDateField.value,
          this.confirmedAssessmentTimeField.value
        );

      if (moment(confirmedDateTime).isBefore(moment(this.referralCreated)) ||
          moment(confirmedDateTime).isAfter(moment())) {
        this.confirmedAssessmentDateField.setErrors({InvalidDateTime: true});
        hasErrors = true;
      }

      this.assessmentOutcomeDateTime = moment(confirmedDateTime).format('DD/MM/YYYY HH:mm');
    }

    if (this.attendingDoctorCount < 1) {
      hasErrors = true;
    }

    if (hasErrors) {
      return;
    }

    this.confirmModal = this.modalService.open(this.confirmTemplate, {
      size: 'lg'
    });
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

  get attendingDoctorCount(): number {
    const doctorList = this.attendingDoctors.filter(doctor => doctor.selected === true);

    return doctorList.length;
  }

  InitialiseForm(referral: ReferralView) {

    this.minDate = this.ConvertToDateStruct(referral.createdAt);
    this.maxDate = this.ConvertToDateStruct(new Date());

    this.assessmentId = referral.currentAssessment.id;

    this.currentAssessment = referral.currentAssessment;
    this.patientIdentifier = referral.patientIdentifier;

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
    this.referralCreated = referral.createdAt;

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

  OnConfirmModalAction(event: any) {
    this.confirmModal.close();
    if (event) {
      this.SaveOutcome();
    }
  }

  SaveOutcome() {

    const confirmedDateTime =
    this.CreateDateFromPickerObjects(
      this.confirmedAssessmentDateField.value,
      this.confirmedAssessmentTimeField.value
    );

    const doctorList: AssessmentOutcomeDoctor[] = [];

    this.attendingDoctors.forEach(doctor => {
      const attendingDoctor =  {
        attended: doctor.selected,
        id: doctor.doctorId
      } as AssessmentOutcomeDoctor;

      doctorList.push(attendingDoctor);
    });

    const assessmentOutcome = {
      assessmentId: this.assessmentId,
      attendingDoctors: doctorList,
      completedTime: confirmedDateTime,
      unsuccessfulAssessmentTypeId: this.selectedOutcome.id
    } as AssessmentOutcome;

    this.assessmentService.putOutcome(
      assessmentOutcome,
      this.assessmentId,
      this.selectedOutcome.id === 0)
    .subscribe((success) => {
      this.toastService.displaySuccess({
        message: 'Assessment Outcome Saved'
      });
      this.routerService.navigateByUrl('/referral/list');
    }, (err) => {
      this.toastService.displayError({
        title: 'Server Error',
        message: 'Unable to update assessment! Please try again in a few moments'
      });
    });
  }

  ToggleSelection(id: number, event) {
    const idx = this.attendingDoctors.findIndex(x => x.id === id);
    this.attendingDoctors[idx].selected = !this.attendingDoctors[idx].selected;
  }

}
