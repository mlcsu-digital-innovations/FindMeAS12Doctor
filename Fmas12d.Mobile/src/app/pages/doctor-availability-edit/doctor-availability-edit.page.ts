import { AVAILABLE, KNOWN_LOCATION_OTHER_ID, KNOWN_LOCATION_OTHER_NAME } from 'src/app/constants/app.constants';
import { catchError, map } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { ContactDetailService } from 'src/app/services/contact-details/contact-detail.service';
import { DoctorAvailability } from 'src/app/models/doctor-availability.model';
import { NameId } from 'src/app/interfaces/name-id.interface';
import { NavController, ToastController } from '@ionic/angular';
import { Observable, of } from 'rxjs';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { Router, ActivatedRoute } from '@angular/router';
import { UserAvailability } from 'src/app/interfaces/user-availability.interface';
import { UserAvailabilityService } from 'src/app/services/user-availability/user-availability.service';
import * as moment from 'moment';

@Component({
  selector: 'app-doctor-availability-edit',
  templateUrl: './doctor-availability-edit.page.html',
  styleUrls: ['./doctor-availability-edit.page.scss'],
})
export class DoctorAvailabilityEditPage implements OnInit {

  public contactDetails$: Observable<NameId>;
  public doctorAvailability: DoctorAvailability = new DoctorAvailability();
  public originalDoctorAvailability: DoctorAvailability = new DoctorAvailability();
  public maxDate: string;
  public minDate: string;
  private cachedUserAvailability: UserAvailability;

  constructor(
    private contactDetailService: ContactDetailService,
    private postcodeValidationService: PostcodeValidationService,
    private navController: NavController,
    private route: ActivatedRoute,
    private router: Router,
    private toastController: ToastController,
    private userAvailabilityService: UserAvailabilityService
  ) { }

  ngOnInit() {
    const now = new Date();

    this.minDate = moment().utc().startOf('day').toISOString();
    this.maxDate =
      moment().utc().startOf('day').add(6, 'M').add(23, 'h').add(55, 'm').toISOString();

    this.cachedUserAvailability =
      this.router.getCurrentNavigation().extras.state.availability;

    this.getContactDetails();
    this.getUserAvailability();
  }

  // availabilityChange() {
  //   this.userAvailability.location.contactDetailId = undefined;
  //   this.userAvailability.location.postcode = undefined;
  //   this.userAvailability.statusId = this.available ? AVAILABLE : UNAVAILABLE;
  // }

  // datesChanged() {
  //   this.hasDateError = false;
  //   this.dateErrorText = '';

  //   if (this.userAvailability.start > this.userAvailability.end) {
  //     this.hasDateError = true;
  //     this.dateErrorText = 'Invalid start / end dates';
  //   }
  // }

  getUserAvailability(): void {
    let userAvailabilityId = +this.route.snapshot.paramMap.get("id")
    this.userAvailabilityService.get(userAvailabilityId)
      .pipe(
        catchError(err => {
          this.showErrorToast('Unable to retrieve availability');
          return of({} as UserAvailability);
        }))
      .subscribe(result => {
        this.doctorAvailability.endDateTime = result.end as Date;
        this.doctorAvailability.id = result.id;
        this.doctorAvailability.isAvailable = result.statusId === AVAILABLE;
        this.doctorAvailability.postcode = result.location.postcode;
        this.doctorAvailability.startDateTime = result.start as Date;
        if (result.location.postcode === null) {
          this.doctorAvailability.knownLocation.id = result.location.contactDetailId;
          this.doctorAvailability.knownLocation.name = result.location.contactDetailTypeName;
        } else {
          this.doctorAvailability.isPostcodeValid = true;
          this.doctorAvailability.knownLocation.id = KNOWN_LOCATION_OTHER_ID;
          this.doctorAvailability.knownLocation.name = KNOWN_LOCATION_OTHER_NAME;
        }
        Object.assign(this.originalDoctorAvailability, this.doctorAvailability);
        console.dir(this.doctorAvailability);
      });
  }

  getContactDetails(): void {
    this.contactDetails$ = this.contactDetailService
      .getContactDetailsForUser(true)
      .pipe(
        map(contactDetails => contactDetails.map(contactDetail => ({
          id: contactDetail.contactDetails[0].id,
          name: contactDetail.name
        }))),
        catchError(err => {
          this.showErrorToast('Unable to retrieve contact details for user');
          return of({} as NameId);
        })
      );
  }

  hasDataChanged(): boolean {
    return !this.doctorAvailability.compareWith(this.originalDoctorAvailability);
    // return this.userAvailability.statusId !== this.originalUserAvailability.statusId ||
    //   this.userAvailability.start !== this.originalUserAvailability.start ||
    //   this.userAvailability.end !== this.originalUserAvailability.end ||
    //   this.userAvailability.location.contactDetailId !== this.originalUserAvailability.location.contactDetailId ||
    //   this.userAvailability.location.postcode !== this.originalUserAvailability.location.postcode;
  }

  // isDataValid(): boolean {

  //   let dataValid = true;

  //   if (this.hasDateError) {
  //     dataValid = false;
  //   }

  //   if (this.available === true) {
  //     if (!this.validPostcode && this.userAvailability.location.contactDetailId === undefined) {
  //       dataValid = false;
  //     }
  //   }
  //   return dataValid;
  // }

  // postcodeChanged() {
  //   this.userAvailability.location.contactDetailId = undefined;
  //   this.validPostcode = false;
  // }

  updateAvailability() {
    let userAvailability: UserAvailability = {
      end: this.doctorAvailability.endDateTime,      
      id: this.doctorAvailability.id,
      location: this.doctorAvailability.location,
      start: this.doctorAvailability.startDateTime,
      statusId: this.doctorAvailability.statusId
    };

    this.userAvailabilityService.putUserAvailability(userAvailability)
      .subscribe(
        result => {
          this.showSuccessToast('Availability updated');
          this.navController.back();
        }, error => {
          this.showErrorToast('Unable to update availability for user');
        }
      );
  }

  showErrorToast(msg: string) {
    this.showToast(msg, 'danger', 'Error!');
  }
  showSuccessToast(msg: string) {
    this.showToast(msg, 'success', 'Success');
  }

  async showToast(msg: string, colour: string, hdr: string) {
    const toast = await this.toastController.create({
      message: msg,
      header: hdr,
      color: colour,
      duration: 2000,
      position: 'top'
    });
    await toast.present();
  }

  validatePostcode() {
    this.postcodeValidationService.getPostcodeDetails(this.doctorAvailability.postcode)
      .subscribe(
        result => {
          this.doctorAvailability.postcode = result.postcode;
          this.doctorAvailability.isPostcodeValid = true;
        }, error => {
          this.showErrorToast('Unable to validate postcode');
          this.doctorAvailability.isPostcodeValid = false;
        }
      );
  }
}