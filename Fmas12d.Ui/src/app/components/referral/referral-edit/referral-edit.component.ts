import { AmhpListService } from 'src/app/services/amhp-list/amhp-list.service';
import { CcgListService } from 'src/app/services/ccg-list/ccg-list.service';
import { Component, OnInit, Renderer2, ViewChild } from '@angular/core';
import { DatePickerFormat } from 'src/app/helpers/date-picker.validator';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GpPracticeListService } from 'src/app/services/gp-practice-list/gp-practice-list.service';
import { NgbModalRef, NgbModal, NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { NhsNumberValidFormat } from 'src/app/helpers/nhs-number.validator';
import { Observable, of, throwError } from 'rxjs';
import { ParamMap, ActivatedRoute, Router } from '@angular/router';
import { Patient } from 'src/app/interfaces/patient';
import { PatientAction } from 'src/app/enums/PatientModalAction.enum';
import { PatientSearchParams } from 'src/app/interfaces/patient-search-params';
import { PatientSearchResult } from 'src/app/interfaces/patient-search-result';
import { PatientSearchService } from 'src/app/services/patient-search/patient-search.service';
import { PostcodeRegex, UNKNOWN_POSTCODE } from 'src/app/constants/Constants';
import { PostcodeSearchResult } from 'src/app/interfaces/postcode-search-result';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralEdit } from 'src/app/interfaces/referralEdit';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { RouterService } from 'src/app/services/router/router.service';
import { switchMap, map, catchError, tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';
import { TypeAheadResult } from 'src/app/interfaces/typeahead-result';
import * as moment from 'moment';
import { PatientService } from 'src/app/services/patient/patient.service';

@Component({
  selector: 'app-referral-edit',
  templateUrl: './referral-edit.component.html',
  styleUrls: ['./referral-edit.component.css']
})
export class ReferralEditComponent implements OnInit {

  cancelModal: NgbModalRef;
  hasAmhpSearchFailed: boolean;
  hasCcgSearchFailed: boolean;
  hasGpSearchFailed: boolean;
  initialReferralDetails: ReferralEdit;
  isAmhpSearching: boolean;
  isCcgFieldShown: boolean;
  isCcgSearching: boolean;
  isGpFieldsShown: boolean;
  isGpSearching: boolean;
  isPatientIdValidated: boolean;
  isPatientPostcodeValidated: boolean;
  isPostcodeFieldShown: boolean;
  isSearchingForPatient: boolean;
  isSearchingForPostcode: boolean;
  isUpdatingReferral: boolean;
  maxDate: NgbDateStruct;
  modalResult: PatientSearchResult;
  patientDetails: Patient;
  patientModal: NgbModalRef;
  patientResult: PatientSearchResult;
  referral$: Observable<ReferralEdit | any>;
  referralCreated: Date;
  referralForm: FormGroup;
  referralId: number;
  residentialPostcodeValidationMessage: string;
  unknownCcgId: number;
  unknownGpPracticeId: number;
  updatedReferral: ReferralEdit;

  constructor(
    private amhpListService: AmhpListService,
    private ccgListService: CcgListService,
    private formBuilder: FormBuilder,
    private gpPracticeListService: GpPracticeListService,
    private modalService: NgbModal,
    private patientService: PatientService,
    private patientSearchService: PatientSearchService,
    private postcodeValidationService: PostcodeValidationService,
    private referralService: ReferralService,
    private renderer: Renderer2,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService
  ) { }

  @ViewChild('patientResults', {static: true}) patientResultTemplate;
  @ViewChild('cancelUpdate', null) cancelUpdateTemplate;

  ngOnInit() {

    this.unknownCcgId = 0;
    this.unknownGpPracticeId = 0;

    this.referral$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.referralService.getReferralEdit(+params.get('referralId'))
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

        const emptyReferral = {} as Referral;
        return of(emptyReferral);
      })
    );

    this.referralForm = this.formBuilder.group({
      nhsNumber: [
        '',
        [
          Validators.maxLength(10),
          Validators.pattern('^[1-9]\\d{9}$'),
          NhsNumberValidFormat
        ]
      ],
      alternativeIdentifier: [
        '',
        [Validators.maxLength(200), Validators.pattern('.*[0-9].*')]
      ],
      gpPractice: [''],
      unknownGpPractice: false,
      residentialPostcode: [
        '',
        [
          Validators.minLength(6),
          Validators.maxLength(8),
          Validators.pattern(`${PostcodeRegex}|(Unknown)$`)
        ]
      ],
      unknownResidentialPostcode: false,
      ccg: [''],
      unknownCcg: false,
      amhp: [''],
      retrospectiveReferral: false,
      referralDate: [
        '',
        [
          DatePickerFormat
        ]
      ],
      referralTime: ['']
    });

    this.patientDetails = {} as Patient;
    this.isPatientIdValidated = false;
    this.OnChanges();

  }

  AmhpSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isAmhpSearching = true)),
      switchMap(term =>
        this.amhpListService.GetAmhpList(term).pipe(
          tap(() => (this.hasAmhpSearchFailed = false)),
          catchError(() => {
            this.hasAmhpSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.isAmhpSearching = false))
    )

  CancelEdit() {
    if (this.referralForm.dirty) {
      this.cancelModal = this.modalService.open(this.cancelUpdateTemplate, {
        size: 'lg'
      });
    } else {
      this.routerService.navigate(['/referral']);
    }
  }

  async CancelPatientResultsModal() {
    this.patientModal.close();
    this.SetFieldFocus('#nhsNumber');
  }

  CcgSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isCcgSearching = true)),
      switchMap(term =>
        this.ccgListService.GetCcgList(term).pipe(
          tap(() => (this.hasCcgSearchFailed = false)),
          catchError(() => {
            this.hasCcgSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.isCcgSearching = false))
    )

  ClearField(fieldName: string) {
    if (this.referralForm.contains(fieldName)) {
      this.referralForm.controls[fieldName].setValue('');
      this.SetFieldFocus(`#${fieldName}`);
    }
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
    const momentDate = moment(start).add(remainder, 'minutes');
    const timeStruct = {} as NgbTimeStruct;
    timeStruct.hour = momentDate.hour();
    timeStruct.minute = momentDate.minutes();
    timeStruct.second = momentDate.seconds();

    return timeStruct;
  }

  CreateDateFromPickerObjects(datePart: NgbDateStruct, timePart: NgbTimeStruct): Date {

    if (datePart === null || timePart === null ) {
      return;
    }

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

  async Delay(milliseconds: number) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
  }

  DisableIfFieldHasValue(fieldName: string): boolean {
    if (fieldName in this.referralForm.controls) {
      return this.referralForm.controls[fieldName].value !== null &&
        this.referralForm.controls[fieldName].value !== '';
    } else {
      throw new Error(
        `DisableIfFieldHasValue(fieldName: string) unable to find field [${fieldName}]`
      );
    }
  }

  DisableIfParentIsDisabled(fieldName: string): boolean {
    return this.referralForm.controls[fieldName].disabled;
  }

  DisablePatientValidationButtonIfFieldsAreInvalid(): boolean {
    // field is only valid if it has a value and there aren't any errors
    const nhsNumberFieldInValid =
      this.nhsNumberField.value === '' ||
      this.nhsNumberField.errors !== null;
    const alternativeIdentifierFieldInValid =
      this.alternativeIdentifierField.value === '' ||
      this.alternativeIdentifierField.errors !== null;

    return nhsNumberFieldInValid &&
      alternativeIdentifierFieldInValid;
  }

  DisablePostcodeValidationButtonIfFieldIsInvalid(): boolean {
    return (
      this.residentialPostcodeField.value === '' ||
      this.residentialPostcodeField.errors !== null ||
      this.unknownResidentialPostcode.value === true
    );
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  get alternativeIdentifier() {
    return this.referralForm.controls.alternativeIdentifier.value;
  }

  get alternativeIdentifierField() {
    return this.referralForm.controls.alternativeIdentifier;
  }

  get amhpField() {
    return this.referralForm.controls.amhp;
  }

  get amhpUser(): TypeAheadResult {
    return this.referralForm.controls.amhp.value;
  }

  get ccg(): TypeAheadResult {
    return this.referralForm.controls.ccg.value;
  }

  get ccgField() {
    return this.referralForm.controls.ccg;
  }

  get gpPractice(): TypeAheadResult {
    return this.referralForm.controls.gpPractice.value;
  }

  get gpPracticeField() {
    return this.referralForm.controls.gpPractice;
  }

  get nhsNumber(): string {
    return this.referralForm.controls.nhsNumber.value;
  }

  get nhsNumberField() {
    return this.referralForm.controls.nhsNumber;
  }

  get referralDateField() {
    return this.referralForm.controls.referralDate;
  }

  get referralTimeField() {
    return this.referralForm.controls.referralTime;
  }

  get residentialPostcode(): string {
    return this.referralForm.controls.residentialPostcode.value;
  }

  get residentialPostcodeField() {
    return this.referralForm.controls.residentialPostcode;
  }

  get retrospectiveReferralField() {
    return this.referralForm.controls.retrospectiveReferral;
  }

  get unknownCcg() {
    return this.referralForm.controls.unknownCcg;
  }

  get unknownGpPractice() {
    return this.referralForm.controls.unknownGpPractice;
  }

  get unknownResidentialPostcode() {
    return this.referralForm.controls.unknownResidentialPostcode;
  }

  GpSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isGpSearching = true)),
      switchMap(term =>
        this.gpPracticeListService.GetGpPracticeList(term).pipe(
          tap(() => (this.hasGpSearchFailed = false)),
          catchError(() => {
            this.hasGpSearchFailed = true;
            this.gpPracticeField.setErrors({ServiceUnavailable: true});
            return of([]);
          })
        )
      ),
      tap(() => (this.isGpSearching = false))
    )

  HasInvalidAlternativeIdentifier(): boolean {
    return (
      this.alternativeIdentifierField.value !== '' &&
      this.alternativeIdentifierField.errors !== null
    );
  }

  HasInvalidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' && this.nhsNumberField.errors !== null
    );
  }

  HasInvalidPostcode(): boolean {
    return (
      this.residentialPostcodeField.value !== '' &&
      this.residentialPostcodeField.errors !== null
    );
  }

  HasNoPatientIdErrors(): boolean {
    return (
      this.nhsNumberField.errors === null &&
      this.alternativeIdentifierField.errors === null
    );
  }

  HasPatientBeenUpdated(): boolean {

    let isUpdated = false;

    if (this.initialReferralDetails.patientAlternativeIdentifier !==
        this.alternativeIdentifierField.value) {
      isUpdated = true;
    }

    if (this.initialReferralDetails.patientNhsNumber !== this.nhsNumberField.value) {
      isUpdated = true;
    }

    if ((this.initialReferralDetails.patientCcgId === null
      ? 0
      : this.initialReferralDetails.patientCcgId) !== this.ccgField.value.id) {
        isUpdated = true;
    }

    if ((this.initialReferralDetails.patientGpPracticeId === null
      ? 0
      : this.initialReferralDetails.patientGpPracticeId) !== this.gpPracticeField.value.id) {
        isUpdated = true;
    }

    if ((this.initialReferralDetails.patientResidentialPostcode === null
        ? 'Unknown'
        : this.initialReferralDetails.patientResidentialPostcode)
          !== this.residentialPostcodeField.value) {
      isUpdated = true;
    }

    return isUpdated;
  }

  HasValidAlternativeIdentifier(): boolean {
    return (
      this.alternativeIdentifierField.value !== '' &&
      this.alternativeIdentifierField.value !== null &&
      this.alternativeIdentifierField.errors === null
    );
  }


  HasValidGpOrPostcodeOrCcg(): boolean {

    // All 3 fields can be 'unknown' OR at least 1 field must be populated
    if (this.gpPractice.id === this.unknownGpPracticeId &&
      this.residentialPostcode === UNKNOWN_POSTCODE &&
      this.ccg.id === this.unknownCcgId) {
      return true;
    }

    return (
      (this.gpPractice.id !== undefined && this.gpPractice.id !== this.unknownGpPracticeId) ||
      (this.residentialPostcode !== '' && this.residentialPostcode !== UNKNOWN_POSTCODE) ||
      (this.ccg.id !== undefined && this.ccg.id !== this.unknownCcgId)
    );
  }

  HasValidLeadAmhp(): boolean {
    return this.amhpUser.id !== undefined;
  }

  HasValidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' &&
      this.nhsNumberField.value !== null &&
      this.nhsNumberField.errors === null
    );
  }

  HasValidNhsNumberOrAlternativeIdentifier(): boolean {
    return (
      (this.HasValidNHSNumber() || this.HasValidAlternativeIdentifier()) &&
      this.HasNoPatientIdErrors()
    );
  }

  HasValidPostcode(): boolean {
    return (
      this.residentialPostcodeField.value !== '' &&
      this.residentialPostcodeField.errors == null
    );
  }

  InitialiseForm(referral: ReferralEdit) {
    console.log(referral);
    this.referralCreated = referral.createdAt;
    this.referralId = referral.id;
    this.alternativeIdentifierField.setValue(referral.patientAlternativeIdentifier);
    this.nhsNumberField.setValue(referral.patientNhsNumber);

    this.referralDateField.setValue(this.ConvertToDateStruct(referral.createdAt));
    this.referralTimeField.setValue(this.ConvertToTimeStruct(referral.createdAt));

    this.initialReferralDetails = referral;

    const gpPracticeValue = referral.patientGpPracticeId === null
      ? {
        id: 0,
        resultText: 'Unknown'
      }
      : {
        id: referral.patientGpPracticeId,
        resultText: referral.patientGpPracticeNameAndPostcode
      };

    this.gpPracticeField.setValue(gpPracticeValue);
    this.unknownGpPractice.setValue(gpPracticeValue.id === 0);

    // only show the postcode field if GP practice is not known
    this.isPostcodeFieldShown = referral.patientGpPracticeId === null ? true : false;

    // only show the CCG field if both GP and Postcode are not known
    this.isCcgFieldShown =
      referral.patientResidentialPostcode === null &&
      referral.patientGpPracticeId === null
        ? true
        : false;

    this.residentialPostcodeField.setValue(
      referral.patientResidentialPostcode === null
        ? 'Unknown'
        : referral.patientResidentialPostcode
    );

    this.unknownResidentialPostcode.setValue(referral.patientResidentialPostcode === null);
    if (this.unknownResidentialPostcode.value === true) {
      this.residentialPostcodeField.disable();
    }

    const ccgValue = referral.patientCcgId === null
      ? {
        id: 0,
        resultText: 'Unknown'
      }
      : {
        id: referral.patientCcgId,
        resultText: referral.patientCcgName
      };

    this.ccgField.setValue(ccgValue);
    this.unknownCcg.setValue(ccgValue.id === 0);

    const amhpUser = referral.leadAmhpUserId === null
      ? {
        id: 0,
        resultText: 'Unknown'
      }
      : {
        id: referral.leadAmhpUserId,
        resultText: referral.leadAmhpUserDisplayName
      };

    this.amhpField.setValue(amhpUser);
  }

  IsPatientIdUnchanged(): boolean {

    if (this.initialReferralDetails === undefined) {
      return true;
    }

    return (
      this.initialReferralDetails.patientNhsNumber ===
        (+this.nhsNumber === 0 ? null : +this.nhsNumber) &&
      this.initialReferralDetails.patientAlternativeIdentifier ===
        this.alternativeIdentifier
    );
  }

  IsPatientIdValidated(): boolean {
    return this.isPatientIdValidated;
  }

  IsSearchingForPatient(): boolean {
    return this.isSearchingForPatient;
  }

  IsSearchingForPostcode(): boolean {
    return this.isSearchingForPostcode;
  }

  IsUnknownFieldChecked(fieldName: string): boolean {
    return this.referralForm.get(fieldName).value;
  }

  OnCancelModalAction(action: boolean) {
    this.cancelModal.close();
    if (action) {
      this.routerService.navigate(['/referral']);
    }
  }

  OnChanges(): void {

    // fields are NOT validated if they are changed after initial validation
    this.nhsNumberField.valueChanges.subscribe(val => {
      this.isPatientIdValidated = val === this.patientDetails.nhsNumber;
    });

    this.alternativeIdentifierField.valueChanges.subscribe((val: string) => {
      if (this.patientDetails.alternativeIdentifier && this.patientDetails.alternativeIdentifier !== '') {
        this.isPatientIdValidated = val.toUpperCase() === this.patientDetails.alternativeIdentifier.toUpperCase();
      }
    });

    this.gpPracticeField.valueChanges.subscribe(val => {
      if (val === '' || val === null) {
        this.unknownGpPractice.setValue(false);
      }
    });

    this.residentialPostcodeField.valueChanges.subscribe(val => {
      if (val !== 'Unknown' && val !== null) {
        this.unknownResidentialPostcode.setValue(false);
      }
    });
  }

  OnPatientModalAction(action: number) {
    switch (action) {
      case PatientAction.Cancel:
        this.CancelPatientResultsModal();
        break;
      case PatientAction.ExistingPatient:
        this.UseExistingPatient();
        break;
      case PatientAction.ExistingReferral:
        this.UseExistingReferral();
        break;
    }
  }

  async SetFieldFocus(fieldName: string) {
    // ToDo: Find a better way to do this !
    await this.Delay(100);
    this.renderer.selectRootElement(fieldName).focus();
  }

  ToggleCcgUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the CCG field and set focus
      this.ccgField.setValue({id: 0, resultText: 'Unknown'});
    } else {
      this.ccgField.setValue(null, '');
      this.SetFieldFocus('#ccg');
    }
  }

  ToggleGpPracticeUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the postcode field and set focus
      this.gpPracticeField.setValue({id: 0, resultText: 'Unknown'});
      this.isPostcodeFieldShown = true;
    } else {
      this.gpPracticeField.setValue(null, '');
      this.SetFieldFocus('#gpPractice');
      this.isPostcodeFieldShown = false;
    }
  }

  ToggleResidentialPostcodeUnknown(event: any) {
    if (event.target.checked) {
      this.residentialPostcodeField.setValue('Unknown');
      this.residentialPostcodeField.disable();
      this.isCcgFieldShown = true;
    } else {
      this.residentialPostcodeField.enable();
      this.residentialPostcodeField.setValue('');
      this.residentialPostcodeField.updateValueAndValidity();
      this.isCcgFieldShown = false;
      this.SetFieldFocus('#residentialPostcode');
    }
  }

  UpdatePatient() {
    const patient = {} as Patient;
    patient.alternativeIdentifier = this.alternativeIdentifierField.value;
    patient.nhsNumber = this.nhsNumberField.value;
    patient.gpPracticeId = this.gpPractice.id;
    patient.residentialPostcode = this.residentialPostcode;
    patient.ccgId = this.ccg.id;

    this.patientService.updatePatient(patient).subscribe(
      (result: Referral) => {
        this.toastService.displaySuccess({
          message: 'Referral Updated'
        });
        this.isUpdatingReferral = false;
        this.routerService.navigate([`/referral/list`]);
      },
      error => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to update referral! Please try again in a few moments'
        });
        this.isUpdatingReferral = false;
        return throwError(error);
      }
    );

  }

  UpdateReferral() {

    let canContinue = true;

    // only continue if the referral is valid
    if (!this.HasValidNhsNumberOrAlternativeIdentifier()) {
      this.nhsNumberField.setErrors({ InvalidPatientIdentifier: true });
      canContinue = false;
    }

    if (!this.HasValidGpOrPostcodeOrCcg()) {
      this.isGpFieldsShown = true;
      this.gpPracticeField.enable();
      this.gpPracticeField.setErrors({ InvalidGpPostcodeCcg: true });
      canContinue = false;
    }

    if (!this.HasValidLeadAmhp()) {
      this.amhpField.setErrors({ InvalidAmhp: true });
      canContinue = false;
    }

    if (this.retrospectiveReferralField.value === true) {
      const referralDate = this.CreateDateFromPickerObjects(this.referralDateField.value, this.referralTimeField.value);

      if (referralDate === undefined) {
        this.referralDateField.setErrors({ MissingDate: true});
        canContinue = false;
      }

      if (referralDate > new Date()) {
        this.referralDateField.setErrors({ FutureDate: true});
        canContinue = false;
      }
    }

    if (canContinue === true) {
      this.UpdateReferralDetails();
    }
  }

  UpdateReferralDetails() {
    // ToDo: Need a service / api call for the update
    const referral = {} as Referral;
    referral.id = this.referralId;
    referral.createdAt = this.retrospectiveReferralField.value === true
      ? this.CreateDateFromPickerObjects(this.referralDateField.value, this.referralTimeField.value)
      : this.referralCreated;

    referral.leadAmhpUserId = this.amhpUser.id;

    this.isUpdatingReferral = true;

    let serviceCall: Observable<object> = this.referralService.updateReferral(referral);

    if (this.retrospectiveReferralField.value === true) {
      serviceCall = this.referralService.updateRetrospectiveReferral(referral);
    }

    serviceCall.subscribe(
      (result: Referral) => {
        this.toastService.displaySuccess({
          message: 'Referral Updated'
        });

        if (this.HasPatientBeenUpdated()) {
          this.UpdatePatient();
        } else {
          this.isUpdatingReferral = false;
          this.routerService.navigate([`/referral/list`]);
        }
      },
      error => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to update referral! Please try again in a few moments'
        });
        this.isUpdatingReferral = false;
        return throwError(error);
      }
    );


  }

  async UseExistingPatient() {
    // ToDo: copy the existing patient details

    this.patientDetails.id = this.patientResult.patientId;
    this.patientDetails.alternativeIdentifier = this.patientResult.alternativeIdentifier;
    this.patientDetails.nhsNumber = this.patientResult.nhsNumber;
    this.patientDetails.gpPracticeId = this.patientResult.gpPracticeId;
    this.patientDetails.residentialPostcode = this.patientResult.residentialPostcode;
    this.patientDetails.ccgId = this.patientResult.ccgId;
    this.patientDetails.isExistingPatient = true;

    this.isGpFieldsShown = true;
    this.gpPracticeField.setValue(
      {
        id: this.patientResult.gpPracticeId,
        resultText: this.patientResult.gpPracticeNameAndPostcode
      }
    );
    this.patientModal.close();
    this.nhsNumberField.markAsPristine();
    this.nhsNumberField.setValue(this.patientResult.nhsNumber);
    this.isPatientIdValidated = true;
  }

  UseExistingReferral(): void {
    // ToDo: Query what to do in this instance !!
    // can't have multiple active referrals for a patient
    this.patientModal.close();
  }

  ValidatePatient(): void {
    if (
      this.IsSearchingForPatient() ||
      this.HasInvalidNHSNumber() ||
      this.HasInvalidAlternativeIdentifier()
    ) {
      return;
    }

    // prevent further buttons clicks and update the page
    this.isSearchingForPatient = true;
    const params = {} as PatientSearchParams;

    if (this.HasValidNHSNumber()) {
      params.nhsNumber = this.nhsNumberField.value;
    } else {
      params.alternativeIdentifier = this.alternativeIdentifierField.value;
    }

    this.patientSearchService.patientSearch(params).subscribe(
      (result: PatientSearchResult) => {
        this.isSearchingForPatient = false;
        // if there are any matching results then display them in a modal
        if (result == null) {
          // no matching patients found, inform user with toast ?
          this.toastService.displayInfo({
            message: 'No existing patients found'
          });
          this.isPatientIdValidated = true;
          this.patientDetails.nhsNumber = (+this.nhsNumber === 0 ? null : +this.nhsNumber);
          this.patientDetails.alternativeIdentifier = this.alternativeIdentifier;
          this.isGpFieldsShown = true;
          this.nhsNumberField.setErrors(null);
          this.SetFieldFocus('#gpPractice');
        } else {
          this.nhsNumberField.setErrors(null);
          this.patientResult = result;
          this.modalResult = result;
          this.patientModal = this.modalService.open(
            this.patientResultTemplate,
            { size: 'lg' }
          );
        }
      },
      error => {
        this.isSearchingForPatient = false;
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to validate patient details! Please try again in a few moments'
        });
        return throwError(error);
      }
    );
  }

  ValidatePostcode(): void {
    if (this.isSearchingForPostcode || this.HasInvalidPostcode()) {
      return;
    }

    this.isSearchingForPostcode = true;

    this.postcodeValidationService
      .validatePostcode(this.residentialPostcodeField.value)
      .subscribe(
        (result: PostcodeSearchResult) => {
          this.isSearchingForPostcode = false;
          if (result.code === null) {
            this.residentialPostcodeValidationMessage =
              'Unable to validate postcode';
            this.residentialPostcodeField.setErrors({
              UnableToValidatePostcode: true
            });
            this.SetFieldFocus('#residentialPostcode');
          } else {
            this.residentialPostcodeField.setErrors(null);
            this.patientDetails.residentialPostcode = result.code;
            this.isPatientPostcodeValidated = true;
            this.residentialPostcodeField.updateValueAndValidity();
            this.toastService.displaySuccess({
              title: 'Postcode Validation',
              message: `${result.code} is a valid postcode`
            });
          }
        },
        error => {
          this.isSearchingForPostcode = false;
          this.toastService.displayError({
            title: 'Server Error',
            message: 'Unable to validate residential postcode ! Please try again in a few moments'
          });
          return throwError(error);
        }
      );
  }
}
