import { Component, OnInit } from '@angular/core';
import { UserAvailability } from 'src/app/interfaces/user-availability.interface';
import { UserAvailabilityService } from 'src/app/services/user-availability/user-availability.service';
import { ToastController, AlertController, IonItemSliding, LoadingController } from '@ionic/angular';
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
  private loading: HTMLIonLoadingElement;
  public unavailableList: UserAvailability[] = [];  

  constructor(
    public alertController: AlertController,
    private loadingController: LoadingController,
    private router: Router,
    private toastController: ToastController,
    private userAvailabilityService: UserAvailabilityService
  ) { }

  ngOnInit() {
  }

  closeLoading() {
    if (this.loading) {
      this.loading.dismiss();
    }
  }

  closeRefreshing($event?: any) {
    if ($event) {
      $event.target.complete();
    }
  }  

  editAvailability(item: UserAvailability, slidingItem: IonItemSliding) {
    slidingItem.close();
    const navigationExtras: NavigationExtras = {
      state: {
        availability: item
      }
    };

    this.router.navigate([`/doctor-availability-edit/${item.id}`], navigationExtras);
  }

  async deleteAvailability(item: UserAvailability, slidingItem: IonItemSliding) {
    slidingItem.close();
    const alert = await this.alertController.create({
      header: 'Confirm Deletion',
      message: 'Are you sure you want to delete this availability period?',
      buttons: [
        {
          text: 'No',
          role: 'cancel',
          cssClass: 'secondary'
        }, {
          text: 'Yes',
          cssClass: 'primary',
          handler: () => {
            this.userAvailabilityService.delete(item.id)
              .subscribe(
                result => {
                  this.showSuccessToast('Availability sucessfully deleted');
                  this.refreshList();
                }, error => {
                  this.showErrorToast('Unable to delete availability');
                }
              );
          }
        }
      ]
    });
    await alert.present();
  }

  ionViewDidEnter() {
    this.refreshList();
  }

  refreshList($event?: any) {
    this.showLoading();
    this.userAvailabilityService.getListForUser()
      .subscribe(
        (result: UserAvailability[]) => {

          if (result && result.length > 0) {
            this.availableList = result.filter(item => item.statusId === AVAILABLE);
            this.unavailableList = result.filter(item => item.statusId === UNAVAILABLE);
          } else {
            this.availableList = [];
            this.unavailableList = [];
          }
          this.closeLoading();
          this.closeRefreshing($event);
        }, error => {
          this.showErrorToast('Unable to retrieve availability details for user');
          this.closeLoading();
          this.closeRefreshing($event);
        }
      );
  }

  showErrorToast(msg: string) {
    this.showToast(msg, 'danger', 'Error!');
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines'
    });
    await this.loading.present();
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
