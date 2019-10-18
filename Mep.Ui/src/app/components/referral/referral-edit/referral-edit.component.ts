import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NhsNumberValidFormat } from 'src/app/helpers/nhs-number.validator';
import { Observable, of } from 'rxjs';
import { ParamMap, ActivatedRoute, Router } from '@angular/router';
import { Patient } from 'src/app/interfaces/patient';
import { PatientSearchParams } from 'src/app/interfaces/patient-search-params';
import { PatientSearchResult } from 'src/app/interfaces/patient-search-result';
import { PatientSearchService } from 'src/app/services/patient-search/patient-search.service';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { switchMap, map, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-referral-edit',
  templateUrl: './referral-edit.component.html',
  styleUrls: ['./referral-edit.component.css']
})
export class ReferralEditComponent implements OnInit {

  isPatientIdValidated: boolean;
  isSearchingForPatient: boolean;
  modalResult: PatientSearchResult;
  patientDetails: Patient;
  referral$: Observable<Referral | any>;
  referralCreated: Date;
  referralForm: FormGroup;
  referralId: number;

  constructor(
    private formBuilder: FormBuilder,
    private patientSearchService: PatientSearchService,
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService
  ) { }

  ngOnInit() {

    this.referral$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.referralService.getReferral(+params.get('referralId'))
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
    });

    this.patientDetails = {} as Patient;
    this.isPatientIdValidated = false;
    this.OnChanges();

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

  get alternativeIdentifier() {
    return this.referralForm.controls.alternativeIdentifier.value;
  }

  get alternativeIdentifierField() {
    return this.referralForm.controls.alternativeIdentifier;
  }

  get nhsNumber(): string {
    return this.referralForm.controls.nhsNumber.value;
  }

  get nhsNumberField() {
    return this.referralForm.controls.nhsNumber;
  }

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

  InitialiseForm(referral: Referral) {
    this.referralCreated = referral.createdAt;
    this.referralId = referral.id;
    this.alternativeIdentifierField.setValue(referral.patient.alternativeIdentifier);
    this.nhsNumberField.setValue(referral.patient.nhsNumber);
  }

  IsSearchingForPatient(): boolean {
    return this.isSearchingForPatient;
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

    // this.residentialPostcodeField.valueChanges.subscribe((val: string) => {
    //   if (this.patientDetails.residentialPostcode && this.patientDetails.residentialPostcode !== '') {
    //     this.isPatientPostcodeValidated =
    //       this.RemoveWhiteSpace(val).toUpperCase() === this.RemoveWhiteSpace(this.patientDetails.residentialPostcode).toUpperCase();
    //   }
    // });
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

}
