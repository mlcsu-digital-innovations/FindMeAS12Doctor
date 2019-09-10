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
      nhsNumber: ['', [Validators.maxLength(10), Validators.pattern('^[1-9]\\d{9}$'), NhsNumberValidFormat]]
    });
  }

  get patient() {
    return this.patientForm.controls;
  }

  submit() {
    console.log(this.patientForm.controls.nhsNumber);
  }

  HasInvalidNHSNumber(): boolean {
    const control = this.patientForm.controls.nhsNumber;
    return control.value !== '' && control.errors !== null;
  }

  HasValidNHSNumber(): boolean {
    const control = this.patientForm.controls.nhsNumber;
    return control.value !== '' && control.errors == null;
  }

}
