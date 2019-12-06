import { Component, OnInit } from '@angular/core';
import { UserAvailability } from 'src/app/interfaces/user-availability.interface';
import { stringify } from 'querystring';
import { UserAvailabilityService } from 'src/app/services/user-availability/user-availability.service';
import { ToastController } from '@ionic/angular';
import { AVAILABLE, UNAVAILABLE } from 'src/app/constants/app.constants';
import { Router, NavigationExtras } from '@angular/router';

@Component({
  selector: 'app-doctor-availability-view',
  templateUrl: './doctor-availability-view.page.html',
  styleUrls: ['./doctor-availability-view.page.scss'],
})
export class DoctorAvailabilityViewPage implements OnInit {

  public availableList: UserAvailability[] = [];
  public fullList: UserAvailability[] = [];
  public unavailableList: UserAvailability[] = [];

  constructor(
    private router: Router,
    private toastController: ToastController,
    private userAvailabilityService: UserAvailabilityService
  ) { }

  ngOnInit() {
  }

  editAvailability(item: UserAvailability) {
    const navigationExtras: NavigationExtras = {
      state: {
      availability: item
      }
    };
    this.router.navigate([`/doctor-availability-edit/${item.id}`], navigationExtras);
  }

  ionViewDidEnter() {
    this.userAvailabilityService.getListForUser()
    .subscribe(
      result => {
        this.availableList = result.filter(item => item.statusId === AVAILABLE);
        this.unavailableList = result.filter(item => item.statusId === UNAVAILABLE);
      }, error => {
        this.showErrorToast('Unable to retrieve availability details for user');
      }
    );
  }

  showErrorToast(msg: string) {
    this.showToast(msg, 'danger', 'Error!');
  }
  showSuccessToast(msg: string) {
    this.showToast(msg, 'success', 'Success');
  }

  async showToast(msg: string, colour: string, hdr: string) {
    const toast = await this.toastController.create({
      message: msg,
      header: hdr,
      color: colour,
      duration: 2000,
      position: 'top'
    });
    await toast.present();
  }

}
