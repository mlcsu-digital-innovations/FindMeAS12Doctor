import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-doctor-accept',
  templateUrl: './doctor-accept.component.html',
  styleUrls: ['./doctor-accept.component.css']
})
export class DoctorAcceptComponent implements OnInit {

  selectDoctor: FormGroup;

  constructor(
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.selectDoctor = this.formBuilder.group({
      doctorSearch: []
     });
  }

}
