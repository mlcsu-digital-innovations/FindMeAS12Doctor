import { Component, OnInit } from '@angular/core';
import { UserAvailability } from 'src/app/interfaces/user-availability.interface';
import { stringify } from 'querystring';

@Component({
  selector: 'app-doctor-availability-view',
  templateUrl: './doctor-availability-view.page.html',
  styleUrls: ['./doctor-availability-view.page.scss'],
})
export class DoctorAvailabilityViewPage implements OnInit {

  public availableList: UserAvailability[] = [];
  public fullList: UserAvailability[] = [];
  public unavailableList: UserAvailability[] = [];

  constructor() { }

  ngOnInit() {
    // this.fullList.push({id: 1, startDateTime: new Date(), endDateTime: new Date(), locationDetails: 'Home', isAvailable: false});
    // this.fullList.push({id: 2, startDateTime: new Date(), endDateTime: new Date(), locationDetails: 'Work', isAvailable: true});
    // this.fullList.push({id: 3, startDateTime: new Date(), endDateTime: new Date(), locationDetails: 'Home', isAvailable: true});

    this.availableList = this.fullList.filter(item => item.isAvailable === true);
    this.unavailableList = this.fullList.filter(item => item.isAvailable === false);

  }

}
