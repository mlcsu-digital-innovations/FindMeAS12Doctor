import { AmhpListService } from '../../../services/amhp-list/amhp-list.service';
import { CcgListService } from '../../../services/ccg-list/ccg-list.service';
import { Component, OnInit, ViewChild, Renderer2 } from '@angular/core';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GpPracticeListService } from '../../../services/gp-practice-list/gp-practice-list.service';
import { map } from 'rxjs/operators';
import { NgbModal, NgbModalRef, NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { NhsNumberValidFormat } from '../../../helpers/nhs-number.validator';
import { Patient } from '../../../interfaces/patient';
import { PatientAction } from 'src/app/enums/PatientModalAction.enum';
import { PatientSearchParams } from '../../../interfaces/patient-search-params';
import { PatientSearchResult } from '../../../interfaces/patient-search-result';
import { PatientSearchService } from '../../../services/patient-search/patient-search.service';
import { PatientService } from '../../../services/patient/patient.service';
import { PostcodeRegex, UNKNOWN } from '../../../constants/Constants';
import { PostcodeSearchResult } from '../../../interfaces/postcode-search-result';
import { PostcodeValidationService } from '../../../services/postcode-validation/postcode-validation.service';
import { Referral } from '../../../interfaces/referral';
import { ReferralService } from '../../../services/referral/referral.service';
import { RouterService } from 'src/app/services/router/router.service';
import { tap, switchMap, catchError } from 'rxjs/operators';
import { throwError, Observable, of, empty } from 'rxjs';
import { ToastService } from '../../../services/toast/toast.service';
import { TypeAheadResult } from '../../../interfaces/typeahead-result';
import { DatePickerFormat } from 'src/app/helpers/date-picker.validator';
import * as moment from 'moment';

@Component({
  selector: 'app-referral-create',
  templateUrl: './referral-create.component.html',
  styleUrls: ['./referral-create.component.css']
})
export class ReferralCreateComponent implements OnInit {

  cancelModal: NgbModalRef;
  existingPatientDetailsUsed: boolean;
  hasAmhpSearchFailed: boolean;
  hasCcgSearchFailed: boolean;
  hasGpSearchFailed: boolean;
  isAmhpFieldsShown: boolean;
  isAmhpSearching: boolean;
  isCcgFieldsShown: boolean;
  isCcgSearching: boolean;
  isCreatingReferral: boolean;
  isGpFieldsShown: boolean;
  isGpSearching: boolean;
  isPatientIdValidated: boolean;
  isPatientPostcodeValidated: boolean;
  isResidentialPostcodeFieldShown: boolean;
  isSearchingForPatient: boolean;
  isSearchingForPostcode: boolean;
  maxDate: NgbDateStruct;
  modalResult: PatientSearchResult;
  myForm: FormGroup;
  patientDetails: Patient;
  patientForm: FormGroup;
  patientModal: NgbModalRef;
  patientResult: PatientSearchResult;
  residentialPostcodeValidationMessage: string;
  unknownCcgId: number;
  unknownGpPracticeId: number;
  value = false;

  @ViewChild('patientResults', { static: true }) patientResultTemplate;
  @ViewChild('cancelReferral', null) cancelReferralTemplate;

  constructor(
    private amhpListService: AmhpListService,
    private ccgListService: CcgListService,
    private formBuilder: FormBuilder,
    private gpPracticeListService: GpPracticeListService,
    private modalService: NgbModal,
    private patientSearchService: PatientSearchService,
    private patientService: PatientService,
    private postcodeValidationService: PostcodeValidationService,
    private referralService: ReferralService,
    private renderer: Renderer2,
    private routerService: RouterService,
    private toastService: ToastService
  ) { }

  ngOnInit() {

    this.unknownCcgId = 0;
    this.unknownGpPracticeId = 0;

    this.modalResult = {} as PatientSearchResult;

    this.residentialPostcodeValidationMessage = 'Invalid Postcode';

    this.patientForm = this.formBuilder.group({
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

    this.maxDate = this.ConvertToDateStruct(new Date());
    this.referralDateField.setValue(this.ConvertToDateStruct(new Date()));
    this.referralTimeField.setValue(this.ConvertToTimeStruct(new Date()));

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
          tap((results: any[]) => (this.ValidateTypeAheadResults(results, 'amhp'))),
          catchError(() => {
            this.hasAmhpSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.isAmhpSearching = false))
    )

  CancelCancellation(): void {
    // close the modal
    this.cancelModal.close();
  }

  async CancelPatientResultsModal() {
    this.alternativeIdentifierField.setValue('');
    this.nhsNumberField.setValue('');
    this.patientModal.close();
    this.SetFieldFocus('#nhsNumber');
  }

  CancelReferral(): void {
    if (this.patientForm.dirty) {
      this.cancelModal = this.modalService.open(this.cancelReferralTemplate, {
        size: 'lg'
      });
    } else {
      this.routerService.navigate(['/referral']);
    }
  }

  CcgSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isCcgSearching = true)),
      switchMap(term =>
        this.ccgListService.GetCcgList(term).pipe(
          tap(() => (this.hasCcgSearchFailed = false)),
          tap((results: any[]) => (this.ValidateTypeAheadResults(results, 'ccg'))),
          catchError(() => {
            this.hasCcgSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.isCcgSearching = false))
    )

  ConfirmCancellation(): void {
    // close the modal
    this.cancelModal.close();
    this.routerService.navigate(['/referral']);
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

    if (datePart === null || timePart === null) {
      return;
    }

    return new Date(
      datePart.year,
      datePart.month - 1,
      datePart.day,
      timePart.hour,
      timePart.minute,
      0,
      0
    );
  }

  CreatePatient() {

    this.patientDetails.nhsNumber =
      +this.nhsNumber === 0 ? null : +this.nhsNumber;
    this.patientDetails.alternativeIdentifier =
      this.alternativeIdentifier === '' ? null : this.alternativeIdentifier;
    this.patientDetails.gpPracticeId =
      this.gpPractice.id === 0 ? null : this.gpPractice.id;
    this.patientDetails.residentialPostcode =
      this.residentialPostcode === '' || this.residentialPostcode === 'Unknown'
        ? null
        : this.residentialPostcode;
    this.patientDetails.ccgId = this.ccg.id === 0 ? null : this.ccg.id;

    return this.patientService.createPatient(this.patientDetails).pipe(
      map((result: any) => {
        this.patientDetails.id = result.id;
        this.SaveReferralDetails();
      }),
      catchError((err, caught) => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to create patient for referral'
        });
        this.isCreatingReferral = false;
        return empty();
      })
    );
  }

  CreateReferral() {

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
        this.referralDateField.setErrors({ MissingDate: true });
        canContinue = false;
      }

      if (referralDate > new Date()) {
        this.referralDateField.setErrors({ FutureDate: true });
        canContinue = false;
      }
    }

    if (canContinue) {
      this.isCreatingReferral = true;
      // create a new patient ?
      if (this.patientDetails.isExistingPatient) {
        this.SaveReferralDetails();
      } else {
        this.CreatePatient().subscribe();
      }
    } else {
      return;
    }
  }

  async Delay(milliseconds: number) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
  }

  DisableIfFieldHasValue(fieldName: string): boolean {
    if (fieldName in this.patientForm.controls) {
      return this.patientForm.controls[fieldName].value !== '';
    } else {
      throw new Error(
        `DisableIfFieldHasValue(fieldName: string) unable to find field [${fieldName}]`
      );
    }
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
      this.residentialPostcodeField.errors !== null
    );
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  FormReset(): void {
    this.nhsNumberField.setValue(null);
    this.alternativeIdentifierField.setValue(null);
    this.alternativeIdentifierField.markAsUntouched();

    this.SetGpPracticeField(null, '');
    this.SetResidentialPostcodeField(null);
    this.SetCcgField(null, '');
    this.amhpField.setValue(null);

    this.isGpFieldsShown = false;
    this.isResidentialPostcodeFieldShown = false;
    this.isCcgFieldsShown = false;

    this.SetFieldFocus('#nhsNumber');
  }

  get alternativeIdentifier(): string {
    return this.patientForm.controls.alternativeIdentifier.value;
  }

  get alternativeIdentifierField() {
    return this.patientForm.controls.alternativeIdentifier;
  }

  get amhpField() {
    return this.patientForm.controls.amhp;
  }

  get amhpUser(): TypeAheadResult {
    return this.patientForm.controls.amhp.value;
  }

  get ccg(): TypeAheadResult {
    return this.patientForm.controls.ccg.value;
  }

  get ccgField() {
    return this.patientForm.controls.ccg;
  }

  get gpPractice(): TypeAheadResult {
    return this.patientForm.controls.gpPractice.value;
  }

  get gpPracticeField() {
    return this.patientForm.controls.gpPractice;
  }

  get nhsNumber(): string {
    return this.patientForm.controls.nhsNumber.value;
  }

  get nhsNumberField() {
    return this.patientForm.controls.nhsNumber;
  }

  get patient() {
    return this.patientForm.controls;
  }

  get residentialPostcode(): string {
    return this.patientForm.controls.residentialPostcode.value;
  }

  get residentialPostcodeField() {
    return this.patientForm.controls.residentialPostcode;
  }

  get retrospectiveReferralField() {
    return this.patientForm.controls.retrospectiveReferral;
  }

  get referralDateField() {
    return this.patientForm.controls.referralDate;
  }

  get referralTimeField() {
    return this.patientForm.controls.referralTime;
  }

  get unknownCcgField() {
    return this.patientForm.controls.unknownCcg;
  }

  get unknownGpPracticeField() {
    return this.patientForm.controls.unknownGpPractice;
  }

  get unknownPostcodeField() {
    return this.patientForm.controls.unknownResidentialPostcode;
  }

  GpSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isGpSearching = true)),
      switchMap(term =>
        this.gpPracticeListService.GetGpPracticeList(term).pipe(
          tap(() => (this.hasGpSearchFailed = false)),
          tap((results: any[]) => (this.ValidateTypeAheadResults(results, 'gpPractice'))),
          catchError(() => {
            this.hasGpSearchFailed = true;
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

  HasValidAlternativeIdentifier(): boolean {
    return (
      this.alternativeIdentifierField.value !== '' &&
      this.alternativeIdentifierField.value !== null &&
      this.alternativeIdentifierField.errors == null
    );
  }

  HasValidGpOrPostcodeOrCcg(): boolean {

    // All 3 fields can be 'unknown' OR at least 1 field must be populated
    if (this.gpPractice.id === this.unknownGpPracticeId &&
      this.residentialPostcode === UNKNOWN &&
      this.ccg.id === this.unknownCcgId) {
      return true;
    }

    return (
      (this.gpPractice.id !== undefined && this.gpPractice.id !== this.unknownGpPracticeId) ||
      (this.residentialPostcode !== '' && this.residentialPostcode !== UNKNOWN) ||
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
      this.nhsNumberField.errors == null
    );
  }

  HasValidNhsNumberOrAlternativeIdentifier(): boolean {
    return (
      (this.HasValidNHSNumber() || this.HasValidAlternativeIdentifier()) &&
      this.HasNoPatientIdErrors()
    );
  }

  HasNoPatientIdErrors(): boolean {
    return (
      this.nhsNumberField.errors == null &&
      this.alternativeIdentifierField.errors == null
    );
  }

  HasValidPostcode(): boolean {
    return (
      this.residentialPostcodeField.value !== '' &&
      this.residentialPostcodeField.errors == null
    );
  }

  HasValidReferral(): boolean {
    // referral needs the following to be valid:
    // NHS number OR Alternative Identifier
    // GP Practice Or Postcode OR CCG
    // Lead AMHP details

    return (
      this.HasValidNhsNumberOrAlternativeIdentifier() &&
      this.HasValidGpOrPostcodeOrCcg() &&
      this.HasValidLeadAmhp()
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
    return this.patientForm.get(fieldName).value;
  }

  OnCancelModalAction(action: boolean) {
    if (action) {
      this.ConfirmCancellation();
    } else {
      this.CancelCancellation();
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

    this.residentialPostcodeField.valueChanges.subscribe((val: string) => {
      if (this.patientDetails.residentialPostcode && this.patientDetails.residentialPostcode !== '') {
        this.isPatientPostcodeValidated =
          this.RemoveWhiteSpace(val).toUpperCase() === this.RemoveWhiteSpace(this.patientDetails.residentialPostcode).toUpperCase();
      }
    });
  }

  PostcodeIsUnknown(): boolean {
    return this.unknownPostcodeField.value;
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

  RemoveWhiteSpace(postcode: string): string {
    return postcode.replace(/ /g, '');
  }

  SaveReferralDetails(): void {
    const referral = {} as Referral;
    referral.leadAmhpUserId = this.amhpUser.id;
    referral.patientId = this.patientDetails.id;

    let retrospective = false;

    if (this.retrospectiveReferralField.value === true) {
      referral.createdAt = this.CreateDateFromPickerObjects(this.referralDateField.value, this.referralTimeField.value);
      retrospective = true;
    } else {
      referral.createdAt = new Date();
    }


    this.referralService.createReferral(referral, retrospective).subscribe(
      (result: Referral) => {
        this.toastService.displaySuccess({
          message: 'Referral Created'
        });
        this.isCreatingReferral = false;
        // navigate to the create assessment page
        this.routerService.navigate([`/assessment/new/${result.id}`]);
      },
      error => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to create new referral! Please try again in a few moments'
        });
        this.isCreatingReferral = false;
        return throwError(error);
      }
    );
  }

  SetLeadAmhpField(text: string | null) {
    this.amhpField.setValue(text);
  }

  SetCcgField(id: number | null, text: string | null) {
    const ccg = {} as TypeAheadResult;
    ccg.id = id;
    ccg.resultText = text;

    this.ccgField.setValue(ccg);
  }

  async SetFieldFocus(fieldName: string) {
    // ToDo: Find a better way to do this !
    await this.Delay(100);
    this.renderer.selectRootElement(fieldName).focus();
  }

  SetGpPracticeField(id: number | null, text: string | null) {
    const gpPractice = {} as TypeAheadResult;

    gpPractice.id = id;
    gpPractice.resultText = text;

    this.gpPracticeField.setValue(gpPractice);
  }

  SetResidentialPostcodeField(text: string | null) {
    this.residentialPostcodeField.setValue(text);
  }

  ToggleCcgUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the CCG field and set focus
      this.SetCcgField(this.unknownCcgId, UNKNOWN);
      this.SetFieldFocus('#amhp');
    } else {
      this.SetCcgField(null, '');
      this.SetFieldFocus('#ccg');
    }
  }

  ToggleGpPracticeUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the postcode field and set focus
      this.SetGpPracticeField(this.unknownGpPracticeId, UNKNOWN);
      this.isResidentialPostcodeFieldShown = true;
      this.SetResidentialPostcodeField('');
      this.unknownPostcodeField.setValue(false);
      this.SetFieldFocus('#residentialPostcode');
    } else {
      this.SetGpPracticeField(null, '');
      this.SetResidentialPostcodeField('');
      this.isResidentialPostcodeFieldShown = false;
      this.SetCcgField(null, '');
      this.isCcgFieldsShown = false;
      this.isPatientPostcodeValidated = false;
      this.SetFieldFocus('#gpPractice');
    }
  }

  ToggleResidentialPostcodeUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the CCG field and set focus
      this.SetResidentialPostcodeField(UNKNOWN);
      this.isCcgFieldsShown = true;
      this.isPatientPostcodeValidated = true;
      this.SetCcgField(null, '');
      this.unknownCcgField.setValue(false);
      this.SetFieldFocus('#ccg');
    } else {
      this.SetResidentialPostcodeField('');
      this.SetCcgField(null, '');
      this.unknownCcgField.setValue(false);
      this.isCcgFieldsShown = false;
      this.isPatientPostcodeValidated = false;
      this.SetFieldFocus('#residentialPostcode');
    }
  }

  async UseExistingPatient() {

    this.existingPatientDetailsUsed = true;
    this.patientDetails.id = this.patientResult.patientId;
    this.patientDetails.alternativeIdentifier = this.patientResult.alternativeIdentifier;
    this.patientDetails.nhsNumber = this.patientResult.nhsNumber;
    this.patientDetails.gpPracticeId = this.patientResult.gpPracticeId;
    this.patientDetails.residentialPostcode = this.patientResult.residentialPostcode;
    this.patientDetails.ccgId = this.patientResult.ccgId;
    this.patientDetails.isExistingPatient = true;

    this.isGpFieldsShown = true;
    if (this.patientResult.gpPracticeId === null) {
      this.SetGpPracticeField(
        0,
        UNKNOWN
      );
      this.unknownGpPracticeField.setValue(true);
    } else {
      this.SetGpPracticeField(
        this.patientResult.gpPracticeId,
        this.patientResult.gpPracticeNameAndPostcode
      );
    }

    if (this.patientResult.residentialPostcode === null) {
      this.SetResidentialPostcodeField(UNKNOWN);
      this.unknownPostcodeField.setValue(true);
    } else {
      this.SetResidentialPostcodeField(this.patientResult.residentialPostcode);
    }

    if (this.patientResult.ccgId === null) {
      this.SetCcgField(
        0,
        UNKNOWN
      );
      this.unknownCcgField.setValue(true);
    } else {
      this.SetCcgField(
        this.patientResult.ccgId,
        this.patientResult.ccgName
      );
    }


    this.patientModal.close();
    this.SetFieldFocus('#amhp');

    this.nhsNumberField.markAsPristine();
    this.nhsNumberField.setValue(this.patientResult.nhsNumber);

    // only show the postcode field if the gpPractice field is null
    if (
      this.patientResult.residentialPostcode !== '' &&
      this.patientResult.gpPracticeId == null
    ) {
      this.isResidentialPostcodeFieldShown = true;
    }

    // only show the ccg field if the postcode field is null
    if (
      this.patientResult.residentialPostcode !== '' &&
      this.patientResult.residentialPostcode !== 'Unknown'
    ) {
      this.isCcgFieldsShown = true;
    }
    this.isPatientIdValidated = true;
  }

  UseExistingReferral(): void {

    // navigate to the referral edit page
    this.patientModal.close();
    this.routerService.navigate([`/referral/edit/${this.patientResult.currentReferralId}`]);
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
        if (result === null) {
          // no matching patients found, inform user with toast ?
          this.toastService.displayInfo({
            message: 'No existing patients found'
          });
          this.isPatientIdValidated = true;
          this.patientDetails.nhsNumber = +this.nhsNumber;
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

          if (result.postcode == null) {
            this.residentialPostcodeField.setErrors(null);
            this.residentialPostcodeValidationMessage =
              'Unable to validate postcode';
            this.residentialPostcodeField.setErrors({
              UnableToValidatePostcode: true
            });
            this.SetFieldFocus('#residentialPostcode');
          } else {
            this.patientDetails.residentialPostcode = result.postcode;
            this.isPatientPostcodeValidated = true;
            this.toastService.displaySuccess({
              title: 'Postcode Validation',
              message: `${result.postcode} is a valid postcode`
            });
          }
        },
        error => {
          this.isSearchingForPostcode = false;
          this.toastService.displayError({
            title: 'Server Error',
            message: 'Unable to validate residential postcode! Please try again in a few moments'
          });
          return throwError(error);
        }
      );
  }

  ValidateTypeAheadResults(results: any[], fieldName: string) {

    if (results == null) {
      this.patientForm.controls[fieldName].setErrors({ NoMatchingResults: true });
    }
  }
}
