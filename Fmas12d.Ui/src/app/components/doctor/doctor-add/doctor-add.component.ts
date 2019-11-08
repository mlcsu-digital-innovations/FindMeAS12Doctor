import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-doctor-add',
  templateUrl: './doctor-add.component.html',
  styleUrls: ['../doctor-styles.css']
})
export class DoctorAddComponent implements OnInit {

  registeredDoctorForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.registeredDoctorForm = this.formBuilder.group({
      gmcNumber: ['',
      [
        Validators.pattern('^[1-9]\\d{9}$')
      ]]
    });
  }

}
