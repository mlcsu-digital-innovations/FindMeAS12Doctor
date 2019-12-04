import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-doctor-availability-add',
  templateUrl: './doctor-availability-add.page.html',
  styleUrls: ['./doctor-availability-add.page.scss'],
})
export class DoctorAvailabilityAddPage implements OnInit {

  public available = true;
  public startDate: Date;
  public startTime: Date;
  public endDateTime: Date;
  public customPickerOptions: any;

  constructor() { }

  ngOnInit() {
    this.customPickerOptions = {

    }
  }

}
