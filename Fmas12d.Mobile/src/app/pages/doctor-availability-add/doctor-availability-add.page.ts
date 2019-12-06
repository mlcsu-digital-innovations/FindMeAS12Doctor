import { Component, OnInit } from '@angular/core';
import { ContactDetailService } from 'src/app/services/contact-details/contact-detail.service';
import { NavController, ToastController } from '@ionic/angular';
import { ActivatedRoute, Router } from '@angular/router';
import { NameId } from 'src/app/interfaces/name-id.interface';

@Component({
  selector: 'app-doctor-availability-add',
  templateUrl: './doctor-availability-add.page.html',
  styleUrls: ['./doctor-availability-add.page.scss'],
})
export class DoctorAvailabilityAddPage implements OnInit {

  public available = true;
  public endDateTime: string;
  public minDate: string;
  public maxDate: string;
  public contactDetails: NameId[] = [];
  public intendedLocationId: number;
  public postcode: string;
  public startDateTime: string;
  public hasDateError: boolean;
  public dateErrorText: string;
  public validPostcode: boolean;

  constructor(
    private contactDetailService: ContactDetailService,
    // private navController: NavController,
    // private route: ActivatedRoute,
    // private router: Router,
    private toastController: ToastController
  ) { }

  ngOnInit() {
    let now = new Date();
    this.minDate = new Date().toISOString();
    this.maxDate = new Date(now.setMonth(now.getMonth() + 6)).toISOString();

    now = new Date();
    this.startDateTime = new Date().toISOString();
    this.endDateTime = new Date(now.setHours(now.getHours() + 8)).toISOString();

    this.getContactDetails();
  }

  locationChanged() {
    console.log('contact changed');
  }

  datesChanged() {
    this.hasDateError = false;
    this.dateErrorText = '';

    if ( this.startDateTime > this.endDateTime ) {
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
      if ( !this.validPostcode || this.intendedLocationId === 0) {
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
              this.contactDetails.push({id: contact.id, name: contact.name});
            });
          }
        }, error => {
          this.showErrorToast('Unable to retrieve contact details for user');
        }
      );
  }

  saveAvailability() {
    console.log('saving');
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
  }

}
