import { AVAILABLE, UNAVAILABLE } from 'src/app/constants/app.constants';
import { Component, OnInit } from '@angular/core';
import { ContactDetailService } from 'src/app/services/contact-details/contact-detail.service';
import { NameId } from 'src/app/interfaces/name-id.interface';
import { NavController, ToastController } from '@ionic/angular';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { Router, ActivatedRoute } from '@angular/router';
import { UserAvailability } from 'src/app/interfaces/user-availability.interface';
import { UserAvailabilityService } from 'src/app/services/user-availability/user-availability.service';

@Component({
  selector: 'app-doctor-availability-edit',
  templateUrl: './doctor-availability-edit.page.html',
  styleUrls: ['./doctor-availability-edit.page.scss'],
})
export class DoctorAvailabilityEditPage implements OnInit {

  public available = true;
  public contactDetails: NameId[] = [];
  public dateErrorText: string;
  public hasDateError: boolean;
  public maxDate: string;
  public minDate: string;
  public postcode: string;
  public userAvailability: UserAvailability;
  public validPostcode: boolean;

  constructor(
    private contactDetailService: ContactDetailService,
    private postcodeValidationService: PostcodeValidationService,
    private navController: NavController,
    private route: ActivatedRoute,
    private router: Router,
    private toastController: ToastController,
    private userAvailabilityService: UserAvailabilityService
  ) {
    this.route.queryParams.subscribe(
      params => {
        if (this.router.getCurrentNavigation().extras.state) {

          this.userAvailability = this.router.getCurrentNavigation().extras.state.availability;
          this.postcode = this.userAvailability.location.postcode;
          this.available = this.userAvailability.statusId === AVAILABLE;
        } else {
          this.showErrorToast('Unable to retrieve existing availability');
        }
      }
    );
  }

  ngOnInit() {
    const now = new Date();
    this.minDate = new Date().toISOString();
    this.maxDate = new Date(now.setMonth(now.getMonth() + 6)).toISOString();
    this.getContactDetails();
  }

  availabilityChange() {
    this.userAvailability.location.contactDetailId = undefined;
    this.userAvailability.location.postcode = undefined;
    this.userAvailability.statusId = this.available ? AVAILABLE : UNAVAILABLE;
  }

  datesChanged() {
    this.hasDateError = false;
    this.dateErrorText = '';

    if ( this.userAvailability.start > this.userAvailability.end ) {
      this.hasDateError = true;
      this.dateErrorText = 'Invalid start / end dates';
    }
  }

  isDataValid(): boolean {

    let dataValid = true;

    if (this.hasDateError) {
      dataValid = false;
    }

    if (this.available === true) {
      if ( !this.validPostcode && this.userAvailability.location.contactDetailId === undefined) {
        dataValid = false;
      }
    }
    return dataValid;
  }

  getContactDetails() {
    this.contactDetailService.getContactDetailsForUser()
      .subscribe(
        result => {
          if (result !== null) {
            this.contactDetails = [];
            result.forEach(contact => {
              this.contactDetails.push({id: contact.contactDetails[0].id, name: contact.name});
            });
          }
        }, error => {
          this.showErrorToast('Unable to retrieve contact details for user');
        }
      );
  }

  postcodeChanged() {
    this.userAvailability.location.contactDetailId = undefined;
    this.validPostcode = false;
  }

  updateAvailability() {
    this.userAvailabilityService.putUserAvailability(this.userAvailability)
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
    this.validPostcode = true;

    this.postcodeValidationService.getPostcodeDetails(this.postcode)
      .subscribe(
        result => {
          this.userAvailability.location.postcode = result.postcode;
        }, error => {
          this.showErrorToast('Unable to validate postcode');
          this.validPostcode = false;
        }
      );
  }
}