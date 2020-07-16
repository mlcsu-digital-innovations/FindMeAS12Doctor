import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { PinDialog } from '@ionic-native/pin-dialog/ngx';
import { Platform, AlertController, ToastController } from '@ionic/angular';
import { StorageService } from 'src/app/services/storage/storage.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { version } from '../../../../package.json';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {

  public appVersion: string = version;
  public canUsePin: boolean;
  public connection: boolean;
  public isAuthenticated: boolean;
  public lastUser: string;

  constructor(
    private alertController: AlertController,
    private authService: AuthService,
    private changeRef: ChangeDetectorRef,
    private networkService: NetworkService,
    private pinDialog: PinDialog,
    private platform: Platform,
    private storageService: StorageService,
    private toastService: ToastService
    ) {

      this.authService.authState.subscribe(authState => {
        this.isAuthenticated = authState;
      });

     }

  ngOnInit() {
    this.connection = this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Online;

    this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {
      this.connection = status === ConnectionStatus.Online;
      this.changeRef.detectChanges();
    });

    this.storageService.getUserNameFromToken()
    .subscribe(userName => {
      this.lastUser = userName;
    }, () => {
      this.lastUser = '';
    });

    this.platform.ready().then(() => {

      if (this.platform.is('cordova') && !this.isAuthenticated) {

        this.canUsePin = false;

        // Check to see if we have a stored pin for the logged in user.
        this.storageService.hasPin()
          .subscribe(pin => {

            // We have a stored PIN, can we sign in silently ?
            if (pin === true) {
              this.authService.canSignInSilently()
                .subscribe(silentSignin => {
                  this.canUsePin = silentSignin;
                });
            }
          });
      }
    });
  }

  public logIn(): void {

    if (this.platform.is('cordova')) {
      this.authService.loginCordovaMsal();
    } else {
      this.authService.loginAzureMsal();
    }
  }

  public async switchUser() {
    const alert = await this.alertController.create({
      cssClass: 'my-custom-class',
      header: 'Switch user ?',
      message: `${this.lastUser} will be signed out`,
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: (blah) => {
            console.log('Confirm Cancel: blah');
          }
        }, {
          text: 'Okay',
          handler: () => {
            this.canUsePin = false;
            this.isAuthenticated = false;
            this.authService.logoutCordovaMsal();
          }
        }
      ]
    });

    await alert.present();
  }

  public enterPin(): void {
    this.pinDialog.prompt('Enter PIN', 'Unlock', ['OK', 'Cancel'])
    .then((result: {buttonIndex: number, input1: string}) => {
      if (result.buttonIndex === 1) {

        this.storageService.comparePin(result.input1)
        .subscribe((match: boolean) => {
          if (match) {
            this.authService.signinSilently();
          } else {
            this.toastService.displayError({message: 'Incorrect PIN'});
          }
        }, err => {
          this.toastService.displayError({message: 'Error verifying PIN'});
        });
      } else {
        console.log('Cancelled');
      }
    });
  }
}
