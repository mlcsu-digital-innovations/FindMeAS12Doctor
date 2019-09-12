import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NhsNumberValidFormat } from '../helpers/nhs-number.validator';
import { PatientSearch } from '../classes/patient-search';
import { PatientSearchService } from '../services/patient-search.service';

@Component({
  selector: 'app-referral-create',
  templateUrl: './referral-create.component.html',
  styleUrls: ['./referral-create.component.css']
})
export class ReferralCreateComponent implements OnInit {
  myForm: FormGroup;
  patientForm: FormGroup;
  value = false;
  searchingForPatient: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private patientService: PatientSearchService
    ) {}

  ngOnInit() {

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
      ]
    });
  }

  get patient() {
    return this.patientForm.controls;
  }

  get nhsNumberField() {
    return this.patientForm.controls.nhsNumber;
  }

  get alternativeIdentifierField() {
    return this.patientForm.controls.alternativeIdentifier;
  }

  IsSearchingForPatient(): boolean {
    return this.searchingForPatient;
  }

  submit() {
    console.log(this.patientForm.controls.nhsNumber);
  }

  HasInvalidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' && this.nhsNumberField.errors !== null
    );
  }

  HasValidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' && this.nhsNumberField.errors == null
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
      this.nhsNumberField.value === '' || this.nhsNumberField.errors !== null;
    const alternativeIdentifierFieldInValid =
      this.alternativeIdentifierField.value === '' ||
      this.alternativeIdentifierField.errors !== null;

    return nhsNumberFieldInValid && alternativeIdentifierFieldInValid;
  }

  Delay(milliseconds: number) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
  }

  ValidatePatient(): void {

    if (this.searchingForPatient || this.HasInvalidNHSNumber() || this.HasInvalidAlternativeIdentifier() ) {
      return;
    }

    // prevent further buttons clicks and update the page
    this.searchingForPatient = true;

    const search = new PatientSearch();

    if (this.HasValidNHSNumber()) {
      search.NhsNumber = this.nhsNumberField.value;
    } else {
      search.AlternativeIdentifier = this.alternativeIdentifierField.value;
    }

    this.patientService.patientSearch(search)
      .subscribe((results) => {
        this.searchingForPatient = false;
        console.log(results);
      });
  }
}
