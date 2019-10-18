import { AmhpListService } from '../../../services/amhp-list/amhp-list.service';
import { CcgListService } from '../../../services/ccg-list/ccg-list.service';
import { Component, OnInit, ViewChild, Renderer2 } from '@angular/core';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GpPracticeListService } from '../../../services/gp-practice-list/gp-practice-list.service';
import { map } from 'rxjs/operators';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NhsNumberValidFormat } from '../../../helpers/nhs-number.validator';
import { Patient } from '../../../interfaces/patient';
import { PatientAction } from 'src/app/enums/PatientModalAction.enum';
import { PatientSearchParams } from '../../../interfaces/patient-search-params';
import { PatientSearchResult } from '../../../interfaces/patient-search-result';
import { PatientSearchService } from '../../../services/patient-search/patient-search.service';
import { PatientService } from '../../../services/patient/patient.service';
import { PostcodeRegex } from '../../../constants/Constants';
import { PostcodeSearchResult } from '../../../interfaces/postcode-search-result';
import { PostcodeValidationService } from '../../../services/postcode-validation/postcode-validation.service';
import { Referral } from '../../../interfaces/referral';
import { ReferralService } from '../../../services/referral/referral.service';
import { Router } from '@angular/router';
import { tap, switchMap, catchError } from 'rxjs/operators';
import { throwError, Observable, of, empty } from 'rxjs';
import { ToastService } from '../../../services/toast/toast.service';
import { TypeAheadResult } from '../../../interfaces/typeahead-result';
import { UNKNOWN_CCG } from '../../../constants/Constants';
import { UNKNOWN_GP_PRACTICE } from '../../../constants/Constants';
import { UNKNOWN_POSTCODE } from '../../../constants/Constants';

@Component({
  selector: 'app-referral-create',
  templateUrl: './referral-create.component.html',
  styleUrls: ['./referral-create.component.css']
})
export class ReferralCreateComponent implements OnInit {

  cancelModal: NgbModalRef;
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
    private router: Router,
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
      amhp: ['']
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
      this.router.navigate(['/referral']);
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
    this.router.navigate(['/referral']);
  }

  CreatePatient() {

    this.patientDetails.nhsNumber =
      +this.nhsNumber === 0 ? null : +this.nhsNumber;
    this.patientDetails.alternativeIdentifier =
      this.alternativeIdentifier === '' ? null : this.alternativeIdentifier;
    this.patientDetails.gpPracticeId =
      this.gpPractice.id === 0 ? null : this.gpPractice.id;
    this.patientDetails.residentialPostcode =
      this.residentialPostcode === '' || this.residentialPostcode === 'Unknown' ? null : this.residentialPostcode;
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

  get nhsNumber(): string {
    return this.patientForm.controls.nhsNumber.value;
  }

  get alternativeIdentifier(): string {
    return this.patientForm.controls.alternativeIdentifier.value;
  }

  get gpPractice(): TypeAheadResult {
    return this.patientForm.controls.gpPractice.value;
  }

  get ccg(): TypeAheadResult {
    return this.patientForm.controls.ccg.value;
  }

  get amhpUser(): TypeAheadResult {
    return this.patientForm.controls.amhp.value;
  }

  get residentialPostcode(): string {
    return this.patientForm.controls.residentialPostcode.value;
  }

  get patient() {
    return this.patientForm.controls;
  }

  get nhsNumberField() {
    return this.patientForm.controls.nhsNumber;
  }

  get residentialPostcodeField() {
    return this.patientForm.controls.residentialPostcode;
  }

  get alternativeIdentifierField() {
    return this.patientForm.controls.alternativeIdentifier;
  }

  get gpPracticeField() {
    return this.patientForm.controls.gpPractice;
  }

  get ccgField() {
    return this.patientForm.controls.ccg;
  }

  get amhpField() {
    return this.patientForm.controls.amhp;
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

    this.referralService.createReferral(referral).subscribe(
      (result: Referral) => {
        this.toastService.displaySuccess({
          message: 'Referral Created'
        });
        this.isCreatingReferral = false;
        // navigate to the create examination page
        this.router.navigate([`/examination/new/${result.id}`]);
      },
      error => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to create new referral ! Please try again in a few moments'
        });
        this.isCreatingReferral = false;
        return throwError(error);
      }
    );
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
      this.SetCcgField(this.unknownCcgId, UNKNOWN_CCG);
      this.SetFieldFocus('#amhp');
    } else {
      this.SetCcgField(null, '');
      this.SetFieldFocus('#ccg');
    }
  }

  ToggleGpPracticeUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the postcode field and set focus
      this.SetGpPracticeField(this.unknownGpPracticeId, UNKNOWN_GP_PRACTICE);
      this.isResidentialPostcodeFieldShown = true;
      this.SetFieldFocus('#residentialPostcode');
    } else {
      this.SetGpPracticeField(null, '');
      this.SetResidentialPostcodeField('');
      this.isResidentialPostcodeFieldShown = false;
      this.SetFieldFocus('#gpPractice');
    }
  }

  ToggleResidentialPostcodeUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the CCG field and set focus
      this.SetResidentialPostcodeField(UNKNOWN_POSTCODE);
      this.isCcgFieldsShown = true;
      this.SetFieldFocus('#ccg');
      this.isPatientPostcodeValidated = true;
    } else {
      this.SetResidentialPostcodeField('');
      this.SetCcgField(null, '');
      this.isCcgFieldsShown = false;
      this.SetFieldFocus('#residentialPostcode');
      this.isPatientPostcodeValidated = false;
    }
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
    this.SetGpPracticeField(
      this.patientResult.gpPracticeId,
      this.patientResult.gpPracticeNameAndPostcode
    );
    this.SetResidentialPostcodeField(this.patientResult.residentialPostcode);
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
      this.patientResult.gpPracticeId == null
    ) {
      this.isResidentialPostcodeFieldShown = true;
    }
    this.isPatientIdValidated = true;
  }

  UseExistingReferral(): void {
    // ToDo: navigate to the existing referral page
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
      (results: PatientSearchResult[]) => {
        this.isSearchingForPatient = false;
        // if there are any matching results then display them in a modal
        switch (results.length) {
          case 0:
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
            break;
          case 1:
            this.nhsNumberField.setErrors(null);
            this.patientResult = results[0];
            this.modalResult = results[0];
            this.patientModal = this.modalService.open(
              this.patientResultTemplate,
              { size: 'lg' }
            );
            break;
          default:
            this.toastService.displayError({
              title: 'Validation Error',
              message: 'Multiple patients found ! Please inform a system administrator'
            });
            this.isPatientIdValidated = false;
            break;
        }
      },
      error => {
        this.isSearchingForPatient = false;
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to validate patient details ! Please try again in a few moments'
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

          if (result.code == null) {
            this.residentialPostcodeField.setErrors(null);
            this.residentialPostcodeValidationMessage =
              'Unable to validate postcode';
            this.residentialPostcodeField.setErrors({
              UnableToValidatePostcode: true
            });
            this.SetFieldFocus('#residentialPostcode');
          } else {
            this.patientDetails.residentialPostcode = result.code;
            this.isPatientPostcodeValidated = true;
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
