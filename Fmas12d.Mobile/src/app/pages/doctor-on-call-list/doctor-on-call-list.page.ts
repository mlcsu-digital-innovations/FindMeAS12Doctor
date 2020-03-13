import { Component } from '@angular/core';
import { LoadingController } from '@ionic/angular';
import { NavigationExtras, Router } from '@angular/router';
import { OnCallDoctor } from 'src/app/interfaces/on-call-doctor.interface';
import { OnCallService } from 'src/app/services/on-call/on-call.service';

@Component({
  selector: 'app-doctor-on-call-list',
  templateUrl: './doctor-on-call-list.page.html',
  styleUrls: ['./doctor-on-call-list.page.scss'],
})
export class DoctorOnCallListPage {
  public onCallDoctorsConfirmed: OnCallDoctor[] = [];
  public onCallDoctorsUnconfirmed: OnCallDoctor[] = [];
  public onCallDoctorsRejected: OnCallDoctor[] = [];
  private loading: HTMLIonLoadingElement;
  private hasData: boolean;

  constructor(
    private onCallService: OnCallService,
    private router: Router,
    private loadingController: LoadingController) { }

  hasOncallDetails() {
    return (
      this.onCallDoctorsConfirmed.length +
      this.onCallDoctorsUnconfirmed.length +
      this.onCallDoctorsRejected.length) > 0
      && this.hasData;
  }

  ionViewDidEnter() {
    this.refreshPage();
  }

  refreshPage($event?: any) {
    this.showLoading();

    this.onCallService.getListForUser().subscribe((result: OnCallDoctor[]) => {
      this.hasData = true;
      if (result && result.length > 0) {
        this.onCallDoctorsConfirmed = result
          .filter((onCall: OnCallDoctor) => onCall.onCallIsConfirmed === true);
        this.onCallDoctorsUnconfirmed = result
          .filter((onCall: OnCallDoctor) => onCall.onCallIsConfirmed === null);
        this.onCallDoctorsRejected = result
          .filter((onCall: OnCallDoctor) => onCall.onCallIsConfirmed === false);
      }

      this.closeLoading();
      this.closeRefreshing($event);
    }, err => {
      this.closeLoading();
      this.closeRefreshing($event);
    });
  }

  confirmOrReject(onCallDoctor: OnCallDoctor) {
    const navigationExtras: NavigationExtras = {
      state: {
        onCallDoctor: onCallDoctor
      }
    };
    this.router.navigate(['/doctor-on-call-confirm-reject'], navigationExtras);
  }

  closeLoading() {
    if (this.loading) {
      setTimeout(() => { this.loading.dismiss(); }, 500);
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
