import { AuthService } from './services/auth/auth.service';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { Component, OnInit } from '@angular/core';
import { FCM } from '@ionic-native/fcm/ngx';
import { MsalService as CordovaMsalService } from './services/msal/msal.service';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { OfflineManagerService } from 'src/app/services/offline-manager/offline-manager.service';
import { Platform, NavController, AlertController, LoadingController } from '@ionic/angular';
import { Router } from '@angular/router';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { StorageService } from './services/storage/storage.service';
import { ToastService } from './services/toast/toast.service';
import { UserDetails } from './interfaces/user-details';
import { UserDetailsService } from './services/user-details/user-details.service';
import * as jwt_decode from 'jwt-decode';
import { MsalResult } from './interfaces/msal-result.interface';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent implements OnInit {

  isAuthenticated: boolean;
  user = {} as UserDetails;
  userName: string;

  private loading: HTMLIonLoadingElement;

  constructor(
    private alertController: AlertController,
    private authService: AuthService,
    private broadcastService: BroadcastService,
    private cordovaMsalService: CordovaMsalService,
    private fcm: FCM,
    private loadingController: LoadingController,
    private networkService: NetworkService,
    private offlineManager: OfflineManagerService,
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private storageService: StorageService,
    private toastService: ToastService,
    private userDetailsService: UserDetailsService
  ) {
  }

  ngOnInit() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();

      if (this.platform.is('cordova')) {
        this.showLoading();
        this.cordovaMsalService.msalInit()
        .subscribe(success => {
          this.hasPin()
          .subscribe(pin => {
            console.log('pin: ', pin);
            if (!pin) {
              // Sign in silently if the user hasn't set a PIN
              this.authService.signinSilently();
            }
            this.closeLoading();
          });
        }, (err) => {
          console.log('Error initialising MSAL', err);
          this.closeLoading();
        });
      }

      this.authService.authState.subscribe(authState => {
        this.isAuthenticated = authState;
      });

      this.fcm.onTokenRefresh().subscribe(
        token => {
          // update the users table with the new token
          console.log('Token Refresh', token);
          this.refreshFcmToken(token);
        }
      );

      this.fcm.onNotification().subscribe(
        data => {
          if (data.wasTapped) {
            // app is currently in background
            this.presentAlertConfirm(data.notificationTitle, data.notificationMessage);
          } else {
            // app is being used
            this.presentAlertConfirm(data.notificationTitle, data.notificationMessage);
          }
        }
      );

      this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {
        if (status === ConnectionStatus.Online) {
          this.offlineManager.checkForEvents().subscribe();
        }
      });

      this.broadcastService.subscribe('msal:loginFailure', (payload) => {
        // TODO: Process the login failure
        console.log('msal:loginFailure', payload);
      });


      this.broadcastService.subscribe('msal:silentLoginSuccess', (payload) => {
        this.authService.authState.next(true);
        this.storageService.storeAccessToken(this.convertToken(payload));
        this.setUserDetails(payload);
        this.fcm.getToken().then(token => {
          this.refreshFcmToken(token);
        });
      });

      this.broadcastService.subscribe('msal:loginSuccess', (payload) => {

        this.toastService.displaySuccess({message: 'Signed in'});

        this.authService.authState.next(true);
        this.storageService.storeAccessToken(this.convertToken(payload));
        this.setUserDetails(payload);
        this.fcm.getToken().then(token => {
          this.refreshFcmToken(token);
        });
      });

      this.broadcastService.subscribe('msal:refreshToken', (payload) => {
        this.cordovaMsalService.refreshTokenSilently();
      });

      this.broadcastService.subscribe('msal:tokenRefresh', (payload) => {
        this.storageService.storeAccessToken(this.convertToken(payload));
      });

      this.broadcastService.subscribe('msal:acquireTokenSuccess', (payload) => {
        // TODO: Process the acquire token success
        console.log('msal:acquireTokenSuccess');
      });

      this.broadcastService.subscribe('msal:acquireTokenFailure', (payload) => {
        // TODO: Process the acquire token failure
        console.log('msal:acquireTokenFailure');
      });

      this.broadcastService.subscribe('msal:notAuthorized', (payload) => {
        console.log('msal:notAuthorized', payload);
      });
    });

    this.storageService.getAccessToken().subscribe(token => {
      if (token) {
        this.setUserDetails(token);
      }
    }, error => {
      this.toastService.displayError({message: error});
    });
  }

  private hasPin(): Observable<any> {

    const checkPin = new Observable((observer) => {

      this.storageService.getPin()
        .subscribe(pin => {
          observer.next(pin !== null);
          observer.complete();
        });
    });
    return checkPin;
  }

  private isAuthenticationResult(object: any): object is MsalResult {
    return typeof object === 'string' ? false : '_token' in object;
  }

  private convertToken(jwt: any): string {

    let token: string;

    if (this.isAuthenticationResult(jwt)) {
      token = jwt._token;
    } else {
      token = jwt;
    }

    return token;
  }

  public logOff(): void {
    this.authService.logoutUsingMsal();
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines',
      duration: 3000
    });
    await this.loading.present();
  }

  closeLoading() {
    if (this.loading) {
      setTimeout(() => { this.loading.dismiss(); }, 500);
    }
  }

  private async presentAlertConfirm(title: string, message: string) {

    const alert = await this.alertController.create({
      header: title,
      message,
      buttons: [
         {
          text: 'Ok',
          handler: () => {
            console.log('Confirm Okay');
          }
        }
      ]
    });

    await alert.present();
  }

  private refreshFcmToken(token: string): void {
    console.log('Refreshing FCM token', token);
    if (token !== null && token !== '') {
      this.userDetailsService.refreshFcmToken(token)
      .subscribe();
    }
  }

  private setUserDetails(token: string): void {
    const details = jwt_decode(token);

    if (details.name) {
      this.userName = details.name;
    }

    if (details.oid) {
      this.userDetailsService.getUserDetails(details.oid)
      .subscribe(user => {
          this.user = user;
      });

      // Set a dummy PIN for testing.
      this.storageService.storePin(details.oid, 1234).subscribe(() => {});
    }
  }
}
