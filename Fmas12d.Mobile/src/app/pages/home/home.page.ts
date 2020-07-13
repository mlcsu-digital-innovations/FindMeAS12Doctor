import { AmhpAssessmentList } from 'src/app/models/amhp-assessment-list.model';
import { AmhpAssessmentRequest } from 'src/app/models/amhp-assessment-request.model';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { ASSESSMENTRESCHEDULING, ASSESSMENTSCHEDULED, AWAITINGREVIEW, DOCTORSTATUSALLOCATED, REFERRALSTATUS_NEW, AVAILABLE } from 'src/app/constants/app.constants';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { PinDialog } from '@ionic-native/pin-dialog/ngx';
import { Platform, AlertController, LoadingController, IonItemSliding } from '@ionic/angular';
import { StorageService } from 'src/app/services/storage/storage.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserAvailability } from 'src/app/interfaces/user-availability.interface';
import { UserAvailabilityService } from 'src/app/services/user-availability/user-availability.service';
import { version } from '../../../../package.json';
import { NavigationExtras, Router } from '@angular/router';
import { OnCallService } from 'src/app/services/on-call/on-call.service';
import { OnCallDoctor } from 'src/app/interfaces/on-call-doctor.interface';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {

  private hasData: boolean;
  private loading: HTMLIonLoadingElement;

  public allAssessments: AmhpAssessmentRequest[] = [];
  public appVersion: string = version;
  public assessmentListLastUpdated: Date;
  public assessmentListScheduled: AmhpAssessmentList[] = [];
  public assessmentRequests: AmhpAssessmentRequest[] = [];
  public assessmentRequestsLastUpdated: Date;
  public availableList: UserAvailability[] = [];
  public canUsePin: boolean;
  public connection: boolean;
  public isAuthenticated: boolean;
  public lastUser: string;
  public onCallDoctorsConfirmed: OnCallDoctor[] = [];
  public onCallDoctorsRejected: OnCallDoctor[] = [];
  public onCallDoctorsUnconfirmed: OnCallDoctor[] = [];
  public scheduledAssessments: AmhpAssessmentRequest[] = [];
  public unavailableList: UserAvailability[] = [];

  constructor(
    private alertController: AlertController,
    private assessmentService: AmhpAssessmentService,
    private authService: AuthService,
    private changeRef: ChangeDetectorRef,
    private loadingController: LoadingController,
    private networkService: NetworkService,
    private onCallService: OnCallService,
    private pinDialog: PinDialog,
    private platform: Platform,
    private router: Router,
    private storageService: StorageService,
    private toastService: ToastService,
    private userAvailabilityService: UserAvailabilityService
  ) {

    this.authService.authState.subscribe(authState => {
      this.isAuthenticated = authState;
    });

  }

  ionViewDidEnter() {
    this.refreshPage();
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

  refreshPage($event?: any) {
    // load data each time the page is shown
    // this may be changed if offline data is to be used
    const request = this.assessmentService.getRequests();
    this.showLoading();

    request
      .subscribe(
        result => {

          this.allAssessments = result;

          if (result && result.length > 0) {

            const scheduled = [ASSESSMENTSCHEDULED, ASSESSMENTRESCHEDULING, AWAITINGREVIEW];

            // scheduled assessments will have a referralId of 6, 7 or 8
            this.scheduledAssessments = this.allAssessments
              .filter(assessment => scheduled.includes(assessment.referralStatusId));
            // .filter(assessment => assessment.dateTime >= new Date());

            this.assessmentRequests = this.allAssessments
              .filter(assessment => !scheduled.includes(assessment.referralStatusId))
              .filter(assessment => assessment.referralStatusId !== REFERRALSTATUS_NEW);
            // .filter(assessment => assessment.dateTime >= new Date());

          } else {
            this.scheduledAssessments = [];
            this.assessmentRequests = [];
          }

          this.assessmentRequestsLastUpdated = new Date();
          this.closeLoading();
          this.closeRefreshing($event);
        }, () => {
          this.closeLoading();
          this.closeRefreshing($event);
        }
      );

    this.userAvailabilityService.getListForUser()
      .subscribe(
        (result: UserAvailability[]) => {

          this.hasData = true;

          if (result && result.length > 0) {
            this.availableList = result.filter(item => item.statusId === AVAILABLE);
            this.unavailableList = result.filter(item => item.statusId === AVAILABLE);
          } else {
            this.availableList = [];
            this.unavailableList = [];
          }
          this.closeLoading();
          this.closeRefreshing($event);
        }, error => {
          this.closeLoading();
          this.closeRefreshing($event);
        }
      );

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

  editAvailability(item: UserAvailability, slidingItem: IonItemSliding) {
    slidingItem.close();
    const navigationExtras: NavigationExtras = {
      state: {
        availability: item
      }
    };

    this.router.navigate([`/doctor-availability-edit/${item.id}`], navigationExtras);
  }

  confirmOrReject(onCallDoctor: OnCallDoctor) {
    const navigationExtras: NavigationExtras = {
      state: {
        onCallDoctor: onCallDoctor
      }
    };
    this.router.navigate(['/doctor-on-call-confirm-reject'], navigationExtras);
  }

  assessmentIsRescheduling(assessment: AmhpAssessmentRequest): boolean {
    return assessment.referralStatusId === ASSESSMENTRESCHEDULING;
  }
  assessmentIsReviewing(assessment: AmhpAssessmentRequest): boolean {
    return assessment.referralStatusId === AWAITINGREVIEW;
  }
  assessmentIsScheduled(assessment: AmhpAssessmentRequest): boolean {
    return assessment.referralStatusId === ASSESSMENTSCHEDULED;
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
          handler: () => {
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
      .then((result: { buttonIndex: number, input1: string }) => {
        if (result.buttonIndex === 1) {

          const userPin = parseInt(result.input1, 10);

          this.storageService.comparePin(userPin)
            .subscribe((match: boolean) => {
              if (match) {
                this.authService.signinSilently();
              } else {
                this.toastService.displayError({ message: 'Incorrect PIN' });
              }
            }, () => {
              this.toastService.displayError({ message: 'Error verifying PIN' });
            });
        } else {
          console.log('Cancelled');
        }
      });
  }

  doctorIsAllocated(assessment: AmhpAssessmentRequest): boolean {
    return assessment.doctorStatusId === DOCTORSTATUSALLOCATED;
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
      spinner: 'lines',
      duration: 3000
    });
    await this.loading.present();
  }
}
