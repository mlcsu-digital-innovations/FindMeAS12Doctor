import { AmhpListService } from '../services/amhp-list/amhp-list.service';
import { CcgListService } from '../services/ccg-list/ccg-list.service';
import { Component, OnInit, ViewChild, Renderer2 } from '@angular/core';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GpPracticeListService } from '../services/gp-practice-list/gp-practice-list.service';
import { map } from 'rxjs/operators';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NhsNumberValidFormat } from '../helpers/nhs-number.validator';
import { Patient } from '../interfaces/patient';
import { PatientSearchParams } from '../interfaces/patient-search-params';
import { PatientSearchResult } from '../interfaces/patient-search-result';
import { PatientSearchService } from '../services/patient-search/patient-search.service';
import { PatientService } from '../services/patient/patient.service';
import { PostcodeSearchResult } from '../interfaces/postcode-search-result';
import { PostcodeValidationService } from '../services/postcode-validation/postcode-validation.service';
import { Referral } from '../interfaces/referral';
import { ReferralService } from '../services/referral/referral.service';
import { tap, switchMap, catchError } from 'rxjs/operators';
import { throwError, Observable, of, empty } from 'rxjs';
import { ToastService } from '../services/toast/toast.service';
import { TypeAheadResult } from '../interfaces/typeahead-result';

@Component({
  selector: 'app-referral-create',
  templateUrl: './referral-create.component.html',
  styleUrls: ['./referral-create.component.css']
})
export class ReferralCreateComponent implements OnInit {
  myForm: FormGroup;
  patientForm: FormGroup;
  value = false;
  isSearchingForPatient: boolean;
  isSearchingForPostcode: boolean;
  isCreatingReferral: boolean;
  dangerMessage: string;
  successMessage: string;
  patientResult: PatientSearchResult;
  patientModal: NgbModalRef;
  cancelModal: NgbModalRef;
  isGpSearching: boolean;
  hasGpSearchFailed: boolean;
  isGpFieldsShown: boolean;
  isResidentialPostcodeFieldShown: boolean;
  residentialPostcodeValidationMessage: string;
  isCcgSearching: boolean;
  hasCcgSearchFailed: boolean;
  isCcgFieldsShown: boolean;
  isAmhpSearching: boolean;
  hasAmhpSearchFailed: boolean;
  isAmhpFieldsShown: boolean;
  patientDetails: Patient;
  isPatientIdValidated: boolean;
  isPatientPostcodeValidated: boolean;

  unknownGpPracticeId: number;
  unknownCcgId: number;

  @ViewChild('dangerTpl', null) dangerTemplate;
  @ViewChild('successTpl', null) successTemplate;
  @ViewChild('patientResults', null) patientResultTemplate;
  @ViewChild('cancelReferral', null) cancelReferralTemplate;

  constructor(
    private formBuilder: FormBuilder,
    private patientSearchService: PatientSearchService,
    private toastService: ToastService,
    private modalService: NgbModal,
    private renderer: Renderer2,
    private gpPracticeListService: GpPracticeListService,
    private postcodeValidationService: PostcodeValidationService,
    private ccgListService: CcgListService,
    private amhpListService: AmhpListService,
    private patientService: PatientService,
    private referralService: ReferralService
  ) {}

  ngOnInit() {

    // ToDo: Get the correct values for these ?
    this.unknownCcgId = 1;
    this.unknownGpPracticeId = 1;

    const postcodeRegex =
      '^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})|(Unknown)$';

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
          Validators.pattern(postcodeRegex)
        ]
      ],
      unknownResidentialPostcode: false,
      ccg: [''],
      unknownCcg: false,
      amhp: ['']
    });

    this.patientDetails = {} as Patient;
    this.isPatientIdValidated = false;

    this.onChanges();
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

  onChanges(): void {

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

  RemoveWhiteSpace(postcode: string): string {
    return postcode.replace(/ /g, '');
  }

  IsUnknownFieldChecked(fieldName: string): boolean {
    return this.patientForm.get(fieldName).value;
  }

  IsSearchingForPatient(): boolean {
    return this.isSearchingForPatient;
  }

  IsSearchingForPostcode(): boolean {
    return this.isSearchingForPostcode;
  }

  IsPatientIdValidated(): boolean {
    return this.isPatientIdValidated;
  }

  HasNoPatientIdErrors(): boolean {
    return (
      this.nhsNumberField.errors == null &&
      this.alternativeIdentifierField.errors == null
    );
  }

  HasValidNhsNumberOrAlternativeIdentifier(): boolean {
    return (
      (this.HasValidNHSNumber() || this.HasValidAlternativeIdentifier()) &&
      this.HasNoPatientIdErrors()
    );
  }

  HasValidGpOrPostcodeOrCcg(): boolean {

    // All 3 fields can be 'unknown' OR at least 1 field must be populated
    if (this.gpPractice.id === this.unknownGpPracticeId && this.residentialPostcode === 'Unknown' && this.ccg.id === this.unknownCcgId) {
      return true;
    }

    return (
      (this.gpPractice.id && this.gpPractice.id !== this.unknownGpPracticeId) ||
      (this.residentialPostcode !== '' && this.residentialPostcode !== 'Unknown') ||
      (this.ccg.id && this.ccg.id !== this.unknownCcgId)
    );
  }

  HasValidLeadAmhp(): boolean {
    return this.amhpUser.id !== undefined;
  }

  SetGpPracticeField(id: number | null, text: string | null) {
    const gpPractice = {} as TypeAheadResult;

    gpPractice.id = id;
    gpPractice.resultText = text;

    this.gpPracticeField.setValue(gpPractice);
  }

  SetCcgField(id: number | null, text: string | null) {
    const ccg = {} as TypeAheadResult;
    ccg.id = id;
    ccg.resultText = text;

    this.ccgField.setValue(ccg);
  }

  SetResidentialPostcodeField(text: string | null) {
    this.residentialPostcodeField.setValue(text);
  }

  ToggleGpPracticeUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the postcode field and set focus
      this.SetGpPracticeField(this.unknownGpPracticeId, 'Unknown');
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
      this.SetResidentialPostcodeField('Unknown');
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

  ToggleCcgUnknown(event: any) {
    if (event.target.checked) {
      // set the field to unknown, show the CCG field and set focus
      this.SetCcgField(this.unknownCcgId, 'Unknown');
      this.SetFieldFocus('#amhp');
    } else {
      this.SetCcgField(null, '');
      this.SetFieldFocus('#ccg');
    }
  }

  HasInvalidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' && this.nhsNumberField.errors !== null
    );
  }

  HasValidPostcode(): boolean {
    return (
      this.residentialPostcodeField.value !== '' &&
      this.residentialPostcodeField.errors == null
    );
  }

  HasInvalidPostcode(): boolean {
    return (
      this.residentialPostcodeField.value !== '' &&
      this.residentialPostcodeField.errors !== null
    );
  }

  HasValidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' &&
      this.nhsNumberField.errors == null
    );
  }

  HasInvalidAlternativeIdentifier(): boolean {
    return (
      this.alternativeIdentifierField.value !== '' &&
      this.alternativeIdentifierField.errors !== null
    );
  }

  HasValidAlternativeIdentifier(): boolean {
    return (
      this.alternativeIdentifierField.value !== '' &&
      this.alternativeIdentifierField.errors == null
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

  CancelReferral(): void {
    this.cancelModal = this.modalService.open(this.cancelReferralTemplate, {
      size: 'lg'
    });
  }

  CreatePatient() {

    this.patientDetails.nhsNumber =
      +this.nhsNumber === 0 ? null : +this.nhsNumber;
    this.patientDetails.alternativeIdentifier =
      this.alternativeIdentifier === '' ? null : this.alternativeIdentifier;
    this.patientDetails.gpPracticeId =
      this.gpPractice.id === 0 ? null : this.gpPractice.id;
    this.patientDetails.residentialPostcode =
      this.residentialPostcode === '' ? null : this.residentialPostcode;
    this.patientDetails.ccgId = this.ccg.id === 0 ? null : this.ccg.id;

    return this.patientService.createPatient(this.patientDetails).pipe(
      map((result: any) => {
        this.patientDetails.id = result.id;
        this.SaveReferralDetails();
      }),
      catchError((err, caught) => {
        this.dangerMessage =
          'Server Error: Unable to create patient for referral !';
        this.toastService.show(this.dangerTemplate, {
          classname: 'bg-danger text-light',
          delay: 10000
        });
        this.isCreatingReferral = false;
        return empty();
      })
    );
  }

  SaveReferralDetails(): void {
    const referral = {} as Referral;
    referral.leadAmhpUserId = this.amhpUser.id;
    referral.patientId = this.patientDetails.id;

    this.referralService.createReferral(referral).subscribe(
      (result: Referral) => {
        this.successMessage = 'Referral Created';
        this.toastService.show(this.successTemplate, {
          classname: 'bg-success text-light',
          delay: 5000
        });
        this.isCreatingReferral = false;
        // navigate to the create examination page
      },
      error => {
        this.dangerMessage =
          'Server Error: Unable to create new referral ! Please try again in a few moments';
        this.toastService.show(this.dangerTemplate, {
          classname: 'bg-danger text-light',
          delay: 10000
        });
        this.isCreatingReferral = false;
        return throwError(error);
      }
    );
  }

  CreateReferral() {
    this.isCreatingReferral = true;
    // create a new patient ?
    if (this.patientDetails.isExistingPatient) {
      this.SaveReferralDetails();
    } else {
      this.CreatePatient().subscribe();
    }
  }

  CancelCancellation(): void {
    // close the modal
    this.cancelModal.close();
  }

  ConfirmCancellation(): void {
    // close the modal
    this.cancelModal.close();

    // reset the form
    this.FormReset();

    // ToDo: navigate to the previous page
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

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  amhpSearch = (text$: Observable<string>) =>
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

  ccgSearch = (text$: Observable<string>) =>
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

  gpSearch = (text$: Observable<string>) =>
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

  async Delay(milliseconds: number) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
  }

  async SetFieldFocus(fieldName: string) {
    // ToDo: Find a better way to do this !
    await this.Delay(100);
    this.renderer.selectRootElement(fieldName).focus();
  }

  async CancelPatientResultsModal() {
    this.alternativeIdentifierField.setValue('');
    this.nhsNumberField.setValue('');
    this.patientModal.close();
    this.SetFieldFocus('#nhsNumber');
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
          this.dangerMessage =
            'Server Error: Unable to validate residential postcode ! Please try again in a few moments';
          this.toastService.show(this.dangerTemplate, {
            classname: 'bg-danger text-light',
            delay: 15000
          });
          return throwError(error);
        }
      );
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
            this.successMessage = 'No existing patients found';
            this.toastService.show(this.successTemplate, {
              classname: 'bg-success text-light',
              delay: 3000
            });
            this.isPatientIdValidated = true;
            this.patientDetails.nhsNumber = +this.nhsNumber;
            this.patientDetails.alternativeIdentifier = this.alternativeIdentifier;
            this.isGpFieldsShown = true;
            this.SetFieldFocus('#gpPractice');
            break;
          case 1:
            this.patientResult = results[0];
            this.patientModal = this.modalService.open(
              this.patientResultTemplate,
              { size: 'lg' }
            );
            break;
          default:
            this.dangerMessage =
              'Validation Error: Multiple patients found ! Please inform a system administrator';
            this.toastService.show(this.dangerTemplate, {
              classname: 'bg-danger text-light',
              delay: 10000
            });
            this.isPatientIdValidated = false;
            break;
        }
      },
      error => {
        this.isSearchingForPatient = false;
        this.dangerMessage =
          'Server Error: Unable to validate patient details ! Please try again in a few moments';
        this.toastService.show(this.dangerTemplate, {
          classname: 'bg-danger text-light',
          delay: 10000
        });
        return throwError(error);
      }
    );
  }
}
