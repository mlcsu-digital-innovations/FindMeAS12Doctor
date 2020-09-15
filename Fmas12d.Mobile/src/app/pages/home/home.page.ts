import { AmhpAssessmentList } from 'src/app/models/amhp-assessment-list.model';
import { AmhpAssessmentRequest } from 'src/app/models/amhp-assessment-request.model';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { ASSESSMENTRESCHEDULING, ASSESSMENTSCHEDULED, AWAITINGREVIEW, DOCTORSTATUSALLOCATED, REFERRALSTATUS_NEW, AVAILABLE, UNAVAILABLE, REFERRALSTATUS_SELECTING, REFERRALSTATUS_AWAITING_RESPONSES, REFERRALSTATUS_RESPONSES_PARTIAL, REFERRALSTATUS_RESPONSES_COMPLETE, REFERRALSTATUS_AWAITING_REVIEW, REFERRALSTATUS_CLOSED, REFERRALSTATUS_OPEN } from 'src/app/constants/app.constants';
import { AuthService } from 'src/app/services/auth/auth.service';
import { BroadcastService } from '@azure/msal-angular';
import { Component, OnInit, ChangeDetectorRef, OnDestroy  } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { OnCallDoctor } from 'src/app/interfaces/on-call-doctor.interface';
import { OnCallService } from 'src/app/services/on-call/on-call.service';
import { PinDialog } from '@ionic-native/pin-dialog/ngx';
import { Platform, AlertController, LoadingController, IonItemSliding } from '@ionic/angular';
import { StorageService } from 'src/app/services/storage/storage.service';
import { Subscription, Observable } from 'rxjs';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserAvailability } from 'src/app/interfaces/user-availability.interface';
import { UserAvailabilityService } from 'src/app/services/user-availability/user-availability.service';
import { UserDetails } from 'src/app/interfaces/user-details';
import { UserDetailsService } from 'src/app/services/user-details/user-details.service';
import { version } from '../../../../package.json';
import * as moment from 'moment';

import 'rxjs/add/observable/zip';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit, OnDestroy {

  private loading: HTMLIonLoadingElement;
  private networkSubscription: Subscription;
  private refreshingData: boolean;
  private spinnerVisible: boolean;
  private subscriptions: Subscription[] = [];
  private tasks$ = [];

  public allAssessments: AmhpAssessmentRequest[] = [];
  public appVersion: string = version;
  public assessmentListLastUpdated: Date;
  public assessmentListScheduled: AmhpAssessmentList[] = [];
  public assessmentRequests: AmhpAssessmentRequest[] = null;
  public assessmentRequestsLastUpdated: Date;
  public availabilityList: UserAvailability[] = null;
  public availableList: UserAvailability[] = [];
  public canUsePin: boolean;
  public connection: boolean;
  public currentAvailability: UserAvailability;
  public currentOnCall: OnCallDoctor;
  public currentUser = {isDoctor: false, isAmhp: false} as UserDetails;
  public isAuthenticated: boolean;
  public nextAvailability: UserAvailability;
  public nextOnCall: OnCallDoctor;
  public onCallDoctorsConfirmed: OnCallDoctor[] = null;
  public onCallDoctorsRejected: OnCallDoctor[] = [];
  public onCallDoctorsUnconfirmedCount: number;
  public scheduledAssessments: AmhpAssessmentRequest[] = null;
  public showAvailability: boolean;
  public showOnCall: boolean;
  public unavailableList: UserAvailability[] = [];
  public unscheduledAssessments: AmhpAssessmentRequest[] = null;

  constructor(
    private alertController: AlertController,
    private assessmentService: AmhpAssessmentService,
    private authService: AuthService,
    private broadcastService: BroadcastService,
    private changeRef: ChangeDetectorRef,
    private loadingController: LoadingController,
    private networkService: NetworkService,
    private onCallService: OnCallService,
    private pinDialog: PinDialog,
    private platform: Platform,
    private router: Router,
    private storageService: StorageService,
    private toastService: ToastService,
    private userAvailabilityService: UserAvailabilityService,
    private userDetailsService: UserDetailsService
  ) {

      this.authService.authState.subscribe(authState => {
        this.isAuthenticated = authState;
        if (this.isAuthenticated) {
          setTimeout(() => {
            this.refreshPage();
          }, 750);
        }
      });

      this.userDetailsService.currentUser.pipe(take(1)).subscribe(user => {
        this.currentUser = user;
      });

      this.broadcastService.subscribe('NotificationReceived', () => {
        this.refreshPage();
      });
  }

  ionViewWillEnter() {
    if (this.isAuthenticated) {
        this.refreshPage();
    }
    this.checkForPin();
  }

  ngOnInit() {

    this.connection = this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Online;

    this.networkSubscription = this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {
      this.connection = status === ConnectionStatus.Online;
      this.changeRef.detectChanges();
    });

    this.subscriptions.push(this.networkSubscription);
  }

  ngOnDestroy() {
    this.subscriptions.forEach((subscription) => {
      subscription.unsubscribe();
    });
  }

  private checkForPin() {
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

  getStatusIcon(request: AmhpAssessmentRequest): string {
    let icon = 'help-circle';

    switch (request.doctorHasAccepted) {
      case undefined:
        icon = 'help-circle';
        break;
      case true:
        icon = 'checkmark-circle';
        break;
      case false:
        icon = 'close-circle';
        break;
    }
    return icon;
  }

  getStatusColour(request: AmhpAssessmentRequest): string {
    let colour = 'success';

    switch (request.doctorHasAccepted) {
      case null:
        colour = 'warning';
        break;
      case true:
        colour = 'success';
        break;
      case false:
        colour = 'danger';
        break;
    }
    return colour;
  }

  refreshPage($event?: any) {
    // load data each time the page is shown
    // this may be changed if offline data is to be used

    if (this.refreshingData) {
      return;
    } else {
      this.refreshingData = true;
    }

    this.tasks$ = [];
    this.tasks$.push(this.assessmentService.getRequests());
    this.tasks$.push(this.userAvailabilityService.getListForUser());
    this.tasks$.push(this.onCallService.getListForUser());

    Observable.zip(...this.tasks$)
      .subscribe((result) => {
        this.refreshingData = false;
        this.displayRequests(result[0] as AmhpAssessmentRequest[]);
        this.displayAvailability(result[1] as UserAvailability[]);
        this.displayOncall(result[2] as OnCallDoctor[]);
      }, err => {
        this.scheduledAssessments = [];
        this.currentOnCall = null;
        this.currentAvailability = null;
        this.nextAvailability = null;
        this.availabilityList = [];
        this.unscheduledAssessments = [];
        this.refreshingData = false;
        this.toastService.displayError({message: 'Unable to retrieve data'});
      });
  }

  displayRequests(result: AmhpAssessmentRequest[]) {

    if (result && result.length > 0) {

      const currentDate = moment().startOf('day');
      const scheduled = [ASSESSMENTSCHEDULED, ASSESSMENTRESCHEDULING, AWAITINGREVIEW];
      const unscheduled = [
        REFERRALSTATUS_SELECTING,
        REFERRALSTATUS_AWAITING_RESPONSES,
        REFERRALSTATUS_RESPONSES_PARTIAL,
        REFERRALSTATUS_RESPONSES_COMPLETE
      ];
      const completed = [
        REFERRALSTATUS_AWAITING_REVIEW,
        REFERRALSTATUS_CLOSED,
        REFERRALSTATUS_OPEN
      ];

      // only interested in todays / future assessments
      this.allAssessments = result
        .filter(assessment => moment(assessment.dateTime).isAfter(currentDate));

      // scheduled assessments will have a referralId of 6, 7 or 8
      this.scheduledAssessments = this.allAssessments
        .filter(assessment => scheduled.includes(assessment.referralStatusId));

      // amhps can view unscheduled assessments but not assessment requests
      if (this.currentUser.isAmhp) {
        this.unscheduledAssessments = this.allAssessments
          .filter(assessment => unscheduled.includes(assessment.referralStatusId));
      } else {
        this.assessmentRequests = this.allAssessments
          .filter(assessment => !scheduled.includes(assessment.referralStatusId))
          .filter(assessment => assessment.referralStatusId !== REFERRALSTATUS_NEW)
          .filter(assessment => !completed.includes(assessment.referralStatusId));
        }
      } else {
          this.scheduledAssessments = [];
          this.assessmentRequests = [];
      }

    this.assessmentRequestsLastUpdated = new Date();
  }

  displayAvailability(result: UserAvailability[]) {

    if (result && result.length > 0) {

      this.showAvailability = true;
      this.availabilityList = result.sort(this.compareAvailability);

      const availabilityStatus = [AVAILABLE, UNAVAILABLE];
      const currentDate = moment();

      this.currentAvailability =
        this.availabilityList.filter(av => availabilityStatus.includes(av.statusId))
          .find(av => (
            moment(av.start).isBefore(currentDate)) && (moment(av.end).isAfter(currentDate)
          ));

      this.nextAvailability =
         this.availabilityList.filter(av => av.statusId === AVAILABLE).find(av =>
           (moment(av.start).isAfter(currentDate)));
      } else {
        this.availabilityList = [];
      }

    if (!this.currentAvailability && !this.nextAvailability) {
      this.availabilityList = [];
    }

  }

  displayOncall(result: OnCallDoctor[]) {

    if (result && result.length > 0) {
      const currentDate = moment();

      this.onCallDoctorsConfirmed = result
        .filter((onCall: OnCallDoctor) => onCall.onCallIsConfirmed === true);

      this.onCallDoctorsConfirmed.sort(this.compareOnCall);

      this.showOnCall = this.onCallDoctorsConfirmed.length > 0;

      this.currentOnCall = this.onCallDoctorsConfirmed
        .find(ocd =>
          (moment(ocd.start).isBefore(currentDate)) && (moment(ocd.end).isAfter(currentDate)));

      this.nextOnCall = this.onCallDoctorsConfirmed
        .find(ocd => moment(ocd.start).isAfter(currentDate));

      this.onCallDoctorsUnconfirmedCount = result
        .filter((onCall: OnCallDoctor) => onCall.onCallIsConfirmed === null).length;
    } else {
      this.onCallDoctorsConfirmed = [];
    }
  }

  compareAvailability = (a: UserAvailability, b: UserAvailability) => {
    if (a.start < b.start) {
      return -1;
    }
    if (a.start > b.start) {
      return 1;
    }
    return 0;
  }

  compareOnCall = (a: OnCallDoctor, b: OnCallDoctor) => {
    if (a.start < b.start) {
      return -1;
    }
    if (a.start > b.start) {
      return 1;
    }
    return 0;
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
        onCallDoctor
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
      message: `${this.currentUser.displayName} will be signed out`,
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

  async showLoading() {

    if (!this.spinnerVisible) {
      this.loading = await this.loadingController.create({
        message: 'Please wait',
        spinner: 'lines'
      });
      this.spinnerVisible = true;
      await this.loading.present();
    }
  }

  closeLoading() {

    setTimeout(() => {
      if (this.loading) {
        this.loading.dismiss().then(() => this.spinnerVisible = false);
      }
    }, 1500);
  }

  closeRefreshing($event?: any) {
    if ($event) {
      $event.target.complete();
    }
  }

  public enterPin(): void {
    this.pinDialog.prompt('Enter PIN', 'Unlock', ['OK', 'Cancel'])
      .then((result: { buttonIndex: number, input1: string }) => {
        if (result.buttonIndex === 1) {

        this.storageService.comparePin(result.input1)
        .subscribe((match: boolean) => {
          if (match) {
            this.authService.signinSilently();
            this.refreshPage();
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
