import { Component, OnInit } from '@angular/core';

import { FormGroup, FormBuilder } from '@angular/forms';

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
    this.myForm = this.formBuilder.group({
      mySwitch: [true]
    });

    this.patientForm = this.formBuilder.group({
    });
  }

  submit() {
    alert(`Value: ${this.myForm.controls.mySwitch.value}`);
    console.log(`Value: ${this.myForm.controls.mySwitchA.value}`);
  }

}
