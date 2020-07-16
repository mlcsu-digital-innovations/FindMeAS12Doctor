import { Component, OnInit } from '@angular/core';
import { LoadingController, AlertController } from '@ionic/angular';
import { StorageService } from 'src/app/services/storage/storage.service';
import { PinDialog } from '@ionic-native/pin-dialog/ngx';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.page.html',
  styleUrls: ['./user-profile.page.scss'],
})
export class UserProfilePage implements OnInit {

  public listLastUpdated: Date;
  private loading: HTMLIonLoadingElement;
  public currentUserName: string;
  public hasPin: boolean;

  private pin1: string;
  private pin2: string;

  constructor(
    private alertController: AlertController,
    private loadingController: LoadingController,
    private pinDialog: PinDialog,
    private storageService: StorageService,
    private toastService: ToastService
  ) { }

  ngOnInit() {

    this.storageService.getUserNameFromToken()
    .subscribe(userName => {
      this.currentUserName = userName;
    }, () => {
      this.currentUserName = '';
    });

    this.storageService.hasPin()
    .subscribe(pin => {
      this.hasPin = pin;
    }, err => console.log('pin error'));
  }

  setPin(): void {
    this.storageService.setPin(this.pin1)
    .subscribe(() => {
      this.toastService.displaySuccess({message: 'PIN set'});
    }, err => {
      this.toastService.displayError({message: 'Error saving PIN'});
    });
  }

  async pinChanged() {

    if (!this.hasPin) {
      const alert = await this.alertController.create({
        cssClass: 'my-custom-class',
        header: 'Confirm',
        message: 'This will remove the existing PIN for this user',
        buttons: [
          {
            text: 'Cancel',
            role: 'cancel',
            cssClass: 'secondary',
            handler: (blah) => {
              this.hasPin = true;
            }
          }, {
            text: 'Okay',
            handler: () => {
              this.storageService.clearPin()
              .subscribe(() => {
                this.toastService.displaySuccess({message: 'PIN cleared'});
              }, err => {
                this.toastService.displayError({message: 'Error clearing PIN'});
              });
            }
          }
        ]
      });
      await alert.present();
    }
  }

  public enterPin(): void {
    this.pinDialog.prompt('PIN', 'Set PIN', ['OK', 'Cancel'])
    .then((result: {buttonIndex: number, input1: string}) => {
      if (result.buttonIndex === 1) {
        this.pin1 = result.input1.trim();

        if (this.pin1.length >= 4) {
          this.confirmPin();
        } else {
          this.toastService.displayError({message: 'PIN must have at least 4 digits'});
          this.pin1 = '';
          this.pin2 = '';
        }
      } else {
        console.log('Cancelled');
      }
    });
  }

  public confirmPin(): void {
    this.pinDialog.prompt('PIN', 'Confirm PIN', ['OK', 'Cancel'])
    .then((result: {buttonIndex: number, input1: string}) => {
      if (result.buttonIndex === 1) {
        this.pin2 = result.input1.trim();

        if (this.pin1.valueOf() === this.pin2.valueOf()) {
          this.setPin();
        } else {
          this.toastService.displayError({message: 'PINs do not match'});
          this.pin1 = '';
          this.pin2 = '';
        }
      } else {
        console.log('Cancelled');
      }
    });
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines'
    });
    await this.loading.present();
  }
}
