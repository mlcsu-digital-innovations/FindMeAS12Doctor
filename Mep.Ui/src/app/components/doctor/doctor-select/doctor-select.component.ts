import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-doctor-select',
  templateUrl: './doctor-select.component.html',
  styleUrls: ['./doctor-select.component.css']
})
export class DoctorSelectComponent implements OnInit {

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
