import { Component, OnInit } from '@angular/core';
import { IonItemSliding, LoadingController } from '@ionic/angular';
import { NavigationExtras, Router } from '@angular/router';
import { OnCallDoctor } from 'src/app/interfaces/on-call-doctor.interface';
import { OnCallService } from 'src/app/services/on-call/on-call.service';

@Component({
  selector: 'app-doctor-on-call-list',
  templateUrl: './doctor-on-call-list.page.html',
  styleUrls: ['./doctor-on-call-list.page.scss'],
})
export class DoctorOnCallListPage implements OnInit {
  public onCallDoctorsConfirmed: OnCallDoctor[];
  public onCallDoctorsUnconfirmed: OnCallDoctor[];
  public onCallDoctorsRejected: OnCallDoctor[];
  private loading: HTMLIonLoadingElement;

  constructor(
    private onCallService: OnCallService, 
    private router: Router,
    private loadingController: LoadingController) { }

  ngOnInit() { }

  ionViewWillEnter() {
    this.refreshPage();
  }

  refreshPage($event?: any) {
    this.onCallService.getListForUser().subscribe((result: OnCallDoctor[])  => {      
      this.onCallDoctorsConfirmed = result
        .filter((onCall: OnCallDoctor) => onCall.onCallIsConfirmed === true);
      this.onCallDoctorsUnconfirmed = result
        .filter((onCall: OnCallDoctor) => onCall.onCallIsConfirmed === null);
      this.onCallDoctorsRejected = result
        .filter((onCall: OnCallDoctor) => onCall.onCallIsConfirmed === false);    
      this.closeLoading();
      this.closeRefreshing($event);
    }, err => {      
      this.closeLoading();
      this.closeRefreshing($event);
    });
  }

  confirmOrReject(onCallDoctor: OnCallDoctor, slidingItem: IonItemSliding) {
    slidingItem.close();
    const navigationExtras: NavigationExtras = {
      state: {
        onCallDoctor: onCallDoctor
      }
    };

    this.router.navigate(['/doctor-on-call-confirm-reject'], navigationExtras);
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

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines'
    });
    await this.loading.present();
  }
}
