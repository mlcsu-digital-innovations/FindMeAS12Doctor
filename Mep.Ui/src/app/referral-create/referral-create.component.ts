import { Component, OnInit } from '@angular/core';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { NhsNumberValidFormat } from '../helpers/nhs-number.validator';

@Component({
  selector: 'app-referral-create',
  templateUrl: './referral-create.component.html',
  styleUrls: ['./referral-create.component.css']
})
export class ReferralCreateComponent implements OnInit {

  myForm: FormGroup;
  patientForm: FormGroup;
  value = false;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.patientForm = this.formBuilder.group({
      nhsNumber: ['', [Validators.maxLength(10), Validators.pattern('^[1-9]\\d{9}$'), NhsNumberValidFormat]],
      alternativeIdentifier: ['', [Validators.maxLength(200), Validators.pattern('.*[0-9].*')]]
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

  nhsNumberChanged(): void {
    console.log('TaDa !!');
  }

  submit() {
    console.log(this.patientForm.controls.nhsNumber);
  }

  HasInvalidNHSNumber(): boolean {
    return this.nhsNumberField.value !== '' && this.nhsNumberField.errors !== null;
  }

  HasValidNHSNumber(): boolean {
    return this.nhsNumberField.value !== '' && this.nhsNumberField.errors == null;
  }

  HasInvalidAlternativeIdentifier(): boolean {
    return this.alternativeIdentifierField.value !== '' && this.alternativeIdentifierField.errors !== null;
  }

  HasValidAlternativeIdentifier(): boolean {
    return this.alternativeIdentifierField.value !== '' && this.alternativeIdentifierField.errors == null;
  }

  DisableIfFieldHasValue(fieldName: string): boolean {
    return this.patientForm.controls[fieldName].value !== '';
  }
}
