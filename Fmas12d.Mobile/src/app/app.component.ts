import { AmhpAssessmentRequest } from 'src/app/models/amhp-assessment-request.model';
import { AmhpAssessmentService } from './services/amhp-assessment/amhp-assessment.service';
import { AssessmentClaimService } from './services/assessment-claims/assessment-claims.service';
import { AuthService } from './services/auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { FCM } from '@ionic-native/fcm/ngx';
import { MsalResult } from './interfaces/msal-result.interface';
import { MsalService as CordovaMsalService } from './services/msal/msal.service';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { Observable } from 'rxjs';
import { OfflineManagerService } from 'src/app/services/offline-manager/offline-manager.service';
import { OnCallService } from './services/on-call/on-call.service';
import { Platform, AlertController, LoadingController } from '@ionic/angular';
import { Router } from '@angular/router';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { StorageService } from './services/storage/storage.service';
import { take } from 'rxjs/operators';
import { ToastService } from './services/toast/toast.service';
import { UserDetails } from './interfaces/user-details';
import { UserDetailsService } from './services/user-details/user-details.service';
import * as jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})

export class AppComponent implements OnInit {

  user = {} as UserDetails;
  userName: string;

  private loading: HTMLIonLoadingElement;

  public allAssessments: AmhpAssessmentRequest[] = [];
  public amhpAssessmentsRequiringAction: number;
  public assessmentsRequiringAction: number;
  public claimsRequiringAction: number;
  public isAuthenticated: boolean;
  public unconfirmedOnCallCount: number;

  private runningOnDevice = false;

  constructor(
    private alertController: AlertController,
    private assessmentClaimService: AssessmentClaimService,
    private assessmentService: AmhpAssessmentService,
    private authService: AuthService,
    private broadcastService: BroadcastService,
    private cordovaMsalService: CordovaMsalService,
    private fcm: FCM,
    private loadingController: LoadingController,
    private networkService: NetworkService,
    private offlineManager: OfflineManagerService,
    private oncallService: OnCallService,
    private platform: Platform,
    private router: Router,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private storageService: StorageService,
    private toastService: ToastService,
    private userDetailsService: UserDetailsService
  ) {

    this.assessmentService.assessmentCount
      .subscribe(count => {
        this.assessmentsRequiringAction = count;
      });

    this.assessmentService.scheduledAssessmentCount
      .subscribe(count => {
        this.amhpAssessmentsRequiringAction = count;
      });

    this.assessmentClaimService.claimsCount
      .subscribe(count => {
        this.claimsRequiringAction = count;
      });

    this.oncallService.unconfirmedOnCall
      .subscribe(count => {
        this.unconfirmedOnCallCount = count;
      });
  }

  refreshBadges() {

    this.assessmentService.getRequests()
      .pipe(take(1))
      .subscribe(
        () => {}
      );

    this.assessmentClaimService.getList()
      .pipe(take(1))
      .subscribe(
        () => {}
      );

    this.oncallService.getListForUser()
      .pipe(take(1))
      .subscribe(
        () => {}
      );
  }

  ngOnInit() {

    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();

      this.refreshBadges();

      if (this.platform.is('cordova')) {
        this.runningOnDevice = true;
        this.showLoading();
        this.cordovaMsalService.msalInit()
        .subscribe(success => {
          this.closeLoading();
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
          this.refreshBadges();

          this.broadcastService.broadcast('NotificationReceived', null);

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

      this.broadcastService.subscribe('msal:loginFailure', () => {
        // TODO: Process the login failure
        console.log('msal:loginFailure');
      });


      this.broadcastService.subscribe('msal:silentLoginSuccess', (payload) => {
        this.toastService.displaySuccess({message: 'Signed in'});
        this.refreshBadges();
        this.changeAuthState(true);
        this.storageService.storeAccessToken(this.convertToken(payload));
        this.setUserDetails(payload);
        this.fcm.getToken().then(token => {
          this.refreshFcmToken(token);
        });
      });

      this.broadcastService.subscribe('msal:loginSuccess', (payload) => {

        this.toastService.displaySuccess({message: 'Signed in'});
        this.refreshBadges();
        this.changeAuthState(true);
        this.storageService.storeAccessToken(this.convertToken(payload));
        this.setUserDetails(payload);
        this.fcm.getToken().then(token => {
          this.refreshFcmToken(token);
        });
      });

      this.broadcastService.subscribe('msal:logoutSuccess', () => {
        this.router.navigate(['/home'])
        .then(r => {
          this.changeAuthState(false);
        });
      });

      this.broadcastService.subscribe('msal:logoutFailure', () => {
        console.log('signed out (failed)');
      });

      this.broadcastService.subscribe('msal:refreshToken', (payload) => {
        this.cordovaMsalService.refreshTokenSilently();
      });

      this.broadcastService.subscribe('msal:tokenRefresh', (payload) => {
        this.storageService.storeAccessToken(this.convertToken(payload));
      });

      this.broadcastService.subscribe('msal:acquireTokenFailure', () => {
        // TODO: Process the acquire token failure
        console.log('msal:acquireTokenFailure');
      });

      this.broadcastService.subscribe('msal:notAuthorized', (payload) => {
        console.log('msal:notAuthorized', payload);
        this.closeLoading();
      });
    });

    this.storageService.getAccessToken().subscribe(token => {
      if (token) {
        this.setUserDetails(token);
      }
    }, error => {
      this.toastService.displayError({ message: error });
    });
  }

  private changeAuthState(authState: boolean) {
    if (!this.runningOnDevice) {
      this.authService.authState.next(authState);
    }
  }

  private hasPin(): Observable<any> {

    const checkPin = new Observable((observer) => {

      this.storageService.hasPin()
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

  public async confirmSignOut() {
    const alert = await this.alertController.create({
      cssClass: 'my-custom-class',
      header: 'Sign Out ?',
      message: `Please confirm sign out`,
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: (blah) => {
            console.log('Confirm Cancel: blah');
          }
        }, {
          text: 'Ok',
          handler: () => {
            this.isAuthenticated = false;
            this.logOff();
          }
        }
      ]
    });

    await alert.present();
  }

  async showLoading() {
    if (!this.loading) {
      this.loading = await this.loadingController.create({
        message: 'Please wait',
        spinner: 'lines',
        duration: 3000
      });
      await this.loading.present();
    }
  }

  closeLoading() {
    setTimeout(() => {
      if (this.loading) {
        this.loading.dismiss();
      }
    }, 1000);
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
