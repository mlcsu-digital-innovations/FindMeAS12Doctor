import { AmhpListService } from 'src/app/services/amhp-list/amhp-list.service';
import { CcgListService } from 'src/app/services/ccg-list/ccg-list.service';
import { Component, OnInit, Renderer2, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GpPracticeListService } from 'src/app/services/gp-practice-list/gp-practice-list.service';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NhsNumberValidFormat } from 'src/app/helpers/nhs-number.validator';
import { Observable, of, throwError } from 'rxjs';
import { ParamMap, ActivatedRoute, Router } from '@angular/router';
import { Patient } from 'src/app/interfaces/patient';
import { PatientAction } from 'src/app/enums/PatientModalAction.enum';
import { PatientSearchParams } from 'src/app/interfaces/patient-search-params';
import { PatientSearchResult } from 'src/app/interfaces/patient-search-result';
import { PatientSearchService } from 'src/app/services/patient-search/patient-search.service';
import { PostcodeRegex } from 'src/app/constants/Constants';
import { PostcodeSearchResult } from 'src/app/interfaces/postcode-search-result';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralEdit } from 'src/app/interfaces/referralEdit';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { RouterService } from 'src/app/services/router/router.service';
import { switchMap, map, catchError, tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';

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
  isCcgSearching: boolean;
  isGpFieldsShown: boolean;
  isGpSearching: boolean;
  isPatientIdValidated: boolean;
  isPatientPostcodeValidated: boolean;
  isSearchingForPatient: boolean;
  isSearchingForPostcode: boolean;
  modalResult: PatientSearchResult;
  patientDetails: Patient;
  patientModal: NgbModalRef;
  patientResult: PatientSearchResult;
  referral$: Observable<ReferralEdit | any>;
  referralCreated: Date;
  referralForm: FormGroup;
  referralId: number;
  residentialPostcodeValidationMessage: string;
  updatedReferral: ReferralEdit;

  constructor(
    private amhpListService: AmhpListService,
    private ccgListService: CcgListService,
    private formBuilder: FormBuilder,
    private gpPracticeListService: GpPracticeListService,
    private modalService: NgbModal,
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

  get ccgField() {
    return this.referralForm.controls.ccg;
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

  get residentialPostcodeField() {
    return this.referralForm.controls.residentialPostcode;
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

  HasValidAlternativeIdentifier(): boolean {
    return (
      this.alternativeIdentifierField.value !== '' &&
      this.alternativeIdentifierField.value !== null &&
      this.alternativeIdentifierField.errors === null
    );
  }

  HasValidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' &&
      this.nhsNumberField.value !== null &&
      this.nhsNumberField.errors === null
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

    this.initialReferralDetails = referral;

    const gpPracticeValue = referral.patientGpPracticeId === null
      ? {
        id: 0,
        resultText: 'Unknown'
      }
      : {
        id: referral.patientGpPracticeId,
        resultText: referral.patientGpNameAndPostcode
      };

    this.gpPracticeField.setValue(gpPracticeValue);
    this.unknownGpPractice.setValue(gpPracticeValue.id === 0);

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
    } else {
      this.gpPracticeField.setValue(null, '');
      this.SetFieldFocus('#gpPractice');
    }
  }

  ToggleResidentialPostcodeUnknown(event: any) {
    if (event.target.checked) {
      this.residentialPostcodeField.setValue('Unknown');
      this.residentialPostcodeField.disable();
    } else {
      this.residentialPostcodeField.enable();
      this.residentialPostcodeField.setValue('');
      this.residentialPostcodeField.updateValueAndValidity();

      this.SetFieldFocus('#residentialPostcode');
    }
  }

  UpdateReferral() {
    // ToDo: add the code for this
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
