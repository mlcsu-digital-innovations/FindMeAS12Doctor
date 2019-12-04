import { Component, OnInit } from '@angular/core';
import { DoctorAvailability } from 'src/app/interfaces/doctor-availability.interface';
import { stringify } from 'querystring';

@Component({
  selector: 'app-doctor-availability-view',
  templateUrl: './doctor-availability-view.page.html',
  styleUrls: ['./doctor-availability-view.page.scss'],
})
export class DoctorAvailabilityViewPage implements OnInit {

  public availableList: DoctorAvailability[] = [];
  public fullList: DoctorAvailability[] = [];
  public unavailableList: DoctorAvailability[] = [];

  constructor() { }

  ngOnInit() {
    this.fullList.push({id: 1, startDateTime: new Date(), endDateTime: new Date(), locationDetails: 'Home', isAvailable: false});
    this.fullList.push({id: 2, startDateTime: new Date(), endDateTime: new Date(), locationDetails: 'Work', isAvailable: true});
    this.fullList.push({id: 3, startDateTime: new Date(), endDateTime: new Date(), locationDetails: 'Home', isAvailable: true});

    this.availableList = this.fullList.filter(item => item.isAvailable === true);
    this.unavailableList = this.fullList.filter(item => item.isAvailable === false);

  }

}
