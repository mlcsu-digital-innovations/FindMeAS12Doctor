import { Component, OnInit } from '@angular/core';
import { ContactDetailService } from 'src/app/services/contact-details/contact-detail.service';
import { NameId } from 'src/app/interfaces/name-id.interface';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { Router } from '@angular/router';
import { ToastController, NavController } from '@ionic/angular';
import { UserAvailability, Location } from 'src/app/interfaces/user-availability.interface';
import { UserAvailabilityService } from 'src/app/services/user-availability/user-availability.service';

@Component({
  selector: 'app-doctor-availability-add',
  templateUrl: './doctor-availability-add.page.html',
  styleUrls: ['./doctor-availability-add.page.scss'],
})
export class DoctorAvailabilityAddPage implements OnInit {

  public available = true;
  public contactDetails: NameId[] = [];
  public dateErrorText: string;
  public endDateTime: string;
  public hasDateError: boolean;
  public maxDate: string;
  public minDate: string;
  public validPostcode: boolean;
  public userAvailability: UserAvailability;

  constructor(
    private contactDetailService: ContactDetailService,
    private postcodeValidationService: PostcodeValidationService,
    private navController: NavController,
    private router: Router,
    private toastController: ToastController,
    private userAvailabilityService: UserAvailabilityService
  ) { }

  ngOnInit() {

    this.userAvailability = {} as UserAvailability;
    this.userAvailability.location = {} as Location;

    let now = new Date();
    this.minDate = new Date().toISOString();
    this.maxDate = new Date(now.setMonth(now.getMonth() + 6)).toISOString();

    now = new Date();
    this.userAvailability.start = now.toISOString();
    this.userAvailability.end = new Date(now.setHours(now.getHours() + 8)).toISOString();

    this.getContactDetails();
  }

  availabilityChange() {
    this.userAvailability.location.contactDetailId = undefined;
    this.userAvailability.postcode = undefined;
    this.userAvailability.isAvailable = this.available;
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

  saveAvailability() {

    this.userAvailabilityService.postUserAvailability(this.userAvailability)
    .subscribe(
      result => {
        this.showSuccessToast('Availability saved');
        this.navController.back();
      }, error => {
        this.showErrorToast(`Unable to save availability for user. ${error.error.title}` );
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
    console.log('validating postcode');
    this.validPostcode = true;

    this.postcodeValidationService.getPostcodeDetails(this.userAvailability.location.postcode)
      .subscribe(
        result => {
          console.log(result);
        }, error => {
          this.showErrorToast('Unable to validate postcode');
          this.validPostcode = false;
        }
      );

  }

}
