import { ActivatedRoute, Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { Component, OnInit } from '@angular/core';
import { OnCallDoctor } from 'src/app/interfaces/on-call-doctor.interface';
import { OnCallService } from 'src/app/services/on-call/on-call.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-doctor-on-call-confirm-reject',
  templateUrl: './doctor-on-call-confirm-reject.page.html',
  styleUrls: ['./doctor-on-call-confirm-reject.page.scss'],
})
export class DoctorOnCallConfirmRejectPage implements OnInit {
  public isConfirmed: boolean;
  public onCallDoctor: OnCallDoctor;
  public reasonAndDetails: string;

  constructor(
    private alertCtrl: AlertController,
    private onCallService: OnCallService,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService
  ) {
    this.isConfirmed = true;
    this.route.queryParams.subscribe(
      params => {
        if (this.router.getCurrentNavigation().extras.state) {
          this.onCallDoctor = this.router.getCurrentNavigation().extras.state.onCallDoctor;
        } else {
          this.onCallDoctor = {} as OnCallDoctor;
        }
      }
    );
  }

  ngOnInit() {
  }

  async saveDialog() {
    const alert = await this.alertCtrl.create({
      header: `${this.isConfirmed ? 'Confirm' : 'Reject'} On Call Doctor?`,
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel'
        },
        {
          text: 'Ok',
          handler: () => {
            this.save();
          }
        }
      ]
    });

    await alert.present();
  }

  save() {
    if (this.isConfirmed) {
      this.onCallService.confirm(this.onCallDoctor.id).subscribe(result => {
        this.toastService.displaySuccess({message: 'On Call Doctor Confirmed'});
        this.router.navigateByUrl('doctor-on-call-list');
      });
    } else {
      this.onCallService.reject(this.onCallDoctor.id, { reason: this.reasonAndDetails })
        .subscribe(result => {
          this.toastService.displaySuccess({message: 'On Call Doctor Rejected'});
          this.router.navigateByUrl('doctor-on-call-list');
      });
    }
  }
}
