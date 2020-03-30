import { AuthService } from './services/auth/auth.service';
import { BroadcastService } from '@azure/msal-angular';
import { Component, OnInit } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { OfflineManagerService } from 'src/app/services/offline-manager/offline-manager.service';
import { Platform, NavController, AlertController } from '@ionic/angular';
import { Router } from '@angular/router';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { StorageService } from './services/storage/storage.service';
import { ToastService } from './services/toast/toast.service';
import { UserDetails } from './interfaces/user-details';
import { UserDetailsService } from './services/user-details/user-details.service';
import * as jwt_decode from 'jwt-decode';
import { FCM } from '@ionic-native/fcm/ngx';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent implements OnInit {

  userName: string;
  user = {} as UserDetails;
  userIsLoggedIn: boolean;

  constructor(
    private alertController: AlertController,
    private authService: AuthService,
    private broadcastService: BroadcastService,
    private fcm: FCM,
    private navController: NavController,
    private networkService: NetworkService,
    private offlineManager: OfflineManagerService,
    private platform: Platform,
    private router: Router,
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
        this.userIsLoggedIn = false;
      });

      this.broadcastService.subscribe('msal:loginSuccess', (payload) => {
        this.userIsLoggedIn = true;
        this.storageService.storeAccessToken(payload.token);
        this.setUserDetails(payload.token);
        this.fcm.getToken().then(token => {
          this.refreshFcmToken(token);
        });
      });

      this.broadcastService.subscribe('msal:acquireTokenSuccess', (payload) => {
        // TODO: Process the acquire token success
      });

      this.broadcastService.subscribe('msal:acquireTokenFailure', (payload) => {
        // TODO: Process the acquire token failure
      });

      this.broadcastService.subscribe('msadal:loginSuccess', (payload) => {
        this.storageService.storeAccessToken(payload.accessToken);
        this.userIsLoggedIn = true;

        this.toastService.displaySuccess(
          {
            message: 'Signed In'
          }
        );

        this.setUserDetails(payload.accessToken);
        this.fcm.getToken().then(token => {
          this.refreshFcmToken(token);
        });
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

  public logOff(): void {
    if (this.platform.is('cordova')) {
      this.authService.logoutMsAdal();

      if (this.router.url === '/home') {
        this.authService.loginMsAdal();
      } else {
        this.navController.navigateRoot('home');
      }
    } else {
      this.authService.logoutMsal();
    }
    this.userIsLoggedIn = false;

    this.toastService.displayMessage(
      {
        message: 'You have been signed out'
      }
    );
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
    }
  }
}
