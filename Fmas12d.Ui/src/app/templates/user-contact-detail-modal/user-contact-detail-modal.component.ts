import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ContactDetailProfile } from 'src/app/interfaces/contact-detail-profile';
import { environment } from 'src/environments/environment';
import { FormGroup, FormBuilder, Validators, FormControl, AbstractControl } from '@angular/forms';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { PostcodeRegex } from 'src/app/constants/Constants';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-user-contact-detail-modal',
  templateUrl: './user-contact-detail-modal.component.html',
  styleUrls: ['./user-contact-detail-modal.component.css']
})
export class UserContactDetailModalComponent implements OnInit {
  @Output() actioned = new EventEmitter<ContactDetailProfile>();
  @Input() public contactDetail: ContactDetailProfile;
  @Input() contactDetails: ContactDetailProfile[];

  addressList: string[];
  contactDetailForm: FormGroup;
  contactDetailTypes: NameIdList[];
  isSearchingForPostcode: boolean;
  submitted: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private nameIdListService: NameIdListService,
    private postcodeValidationService: PostcodeValidationService,
    private toastService: ToastService
  ) { }

  ngOnInit() {
    this.addressList = [];

    this.contactDetailForm = this.formBuilder.group({
      address: ['', [Validators.required, this.AddressSelected]],
      contactDetailTypeId: [
        '1',
        Validators.required
      ],
      emailAddress: ['', [Validators.required, Validators.email]],
      latitude: new FormControl({ value: '', disabled: true}),
      longitude: new FormControl({ value: '', disabled: true}),
      mobileNumber: [''],
      postcode: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(8),
          Validators.pattern(`${PostcodeRegex}$`)
        ]
      ],
      telephoneNumber: ['']
    });

    this.nameIdListService.GetListData('contactdetailtype')
      .subscribe((contactDetailTypeList: NameIdList[]) => {
        this.contactDetailTypes = contactDetailTypeList;
        if (this.contactDetail) {
          this.controls.contactDetailTypeId.disable();

          if (this.contactDetail.id && this.contactDetails) {
            this.contactDetail = this.contactDetails
              .find(item => item.contactDetailTypeId === this.contactDetail.contactDetailTypeId);
          }

          this.contactDetailForm.patchValue(this.contactDetail);
          const currentAddress: string = `${this.contactDetail.address1 ? this.contactDetail.address1 : ''}${this.contactDetail.address1 && this.contactDetail.address2 ? ', ' : ''}${this.contactDetail.address2 ? this.contactDetail.address2 : ''}${this.contactDetail.address2 && this.contactDetail.address3 ? ', ' : ''}${this.contactDetail.address3 ? this.contactDetail.address3 : ''}`
          if (currentAddress.length > 0) {
            this.addressList.push(currentAddress);
            this.controls.address.setValue(currentAddress);
            this.controls.address.disable();
          }
        } else if (this.contactDetails && this.contactDetails.length === 1) {
          const remainingContactDetailTypeId: number = this.contactDetailTypes
            .find(item => item.id !== this.contactDetails[0].contactDetailTypeId).id;
          this.contactDetailTypes = this.contactDetailTypes
            .filter(item => item.id === remainingContactDetailTypeId);
          this.controls.contactDetailTypeId.setValue(remainingContactDetailTypeId);
        }
      });
  }

  get controls() {
    return this.contactDetailForm.controls;
  }

  ClearAddress() {
    this.controls.latitude.setValue(null);
    this.controls.longitude.setValue(null);
    if (this.controls.postcode.value.length > 0) {
      this.controls.postcode.setErrors({ NoAddresses: true });
    }
  }

  FormatAddress(): string[] {
    const addressLines: string[] = [];
    const addressSplitByCommas = this.controls.address.value.split(',');

    for (let i = 0; i < 4; i++) {
      if (addressSplitByCommas.length - 1 >= i &&
          addressSplitByCommas[i].trim() !== this.controls.address.value) {
            addressLines.push(addressSplitByCommas[i].trim());
      } else {
        addressLines.push(null);
      }
    }

    return addressLines;
  }

  AddressSelected(control: AbstractControl) {
    if (!control.value || control.value === 'Please Select Address') {
      return { SelectAddress: true };
    } else {
      return null;
    }
  }

  FormatPostcode() {
    let postcode = this.controls.postcode.value.trim();
    if (postcode.indexOf(' ') === -1 && postcode.length > 3) {
      const inwardCode = postcode.substr(postcode.length - 3, 3);
      const outwardCode = postcode.substr(0, postcode.length - 3);
      postcode = `${outwardCode} ${inwardCode}`;
    }
    this.controls.postcode.setValue(postcode);
  }

  AddressSearch(): void {
    this.addressList = ['Please Select Address'];
    this.controls.address.setValue('Please Select Address');
    this.isSearchingForPostcode = true;
    this.controls.address.disable();
    this.FormatPostcode();

    this.postcodeValidationService.searchPostcode(this.controls.postcode.value)
      .subscribe((postcodeDetails: any) => {
        this.addressList = this.addressList.concat(postcodeDetails.addresses);
        this.isSearchingForPostcode = false;
        this.controls.latitude.setValue(postcodeDetails.latitude);
        this.controls.longitude.setValue(postcodeDetails.longitude);
        this.controls.postcode.setErrors(null);
        this.toastService.displaySuccess({
          message: 'Postcode is valid'
        });
      }, (error) => {
        this.isSearchingForPostcode = false;
        this.controls.latitude.reset();
        this.controls.longitude.reset();
        this.controls.postcode.setErrors({InvalidPostcode: true});
        this.toastService.displayError({
          title: 'Search Error',
          message: 'Error Retrieving Address Information'
        });
      }, () => {
        this.isSearchingForPostcode = false;
        if (this.addressList.length > 0) {
          this.controls.address.enable();
        } else {
          this.controls.postcode.setErrors({ NoResultsReturned: true });
        }
      }
    );
  }

  OpenLocationTab(): void {
    window.open(environment.locationEndpoint, '_blank');
  }

  Cancel() {
    this.actioned.emit(null);
  }

  SaveContactDetail() {
    this.submitted = true;

    if (this.contactDetailForm.valid) {
      this.controls.contactDetailTypeId.enable();
      this.controls.latitude.enable();
      this.controls.longitude.enable();
      let contactDetail: ContactDetailProfile = {} as ContactDetailProfile;
      let formattedAddress = this.FormatAddress();
      contactDetail.address1 = formattedAddress[0];
      contactDetail.address2 = formattedAddress[1];
      contactDetail.address3 = formattedAddress[2];
      contactDetail.contactDetailTypeId = this.controls.contactDetailTypeId.value;
      contactDetail.emailAddress = this.controls.emailAddress.value;
      contactDetail.latitude = this.controls.latitude.value;
      contactDetail.longitude = this.controls.longitude.value;
      contactDetail.mobileNumber = this.controls.mobileNumber.value;
      contactDetail.postcode = this.controls.postcode.value;
      contactDetail.telephoneNumber = this.controls.telephoneNumber.value;
      contactDetail.town = formattedAddress[3];
      contactDetail.id = this.contactDetail ? this.contactDetail.id : 0;
      contactDetail.contactDetailTypeName = this.contactDetailTypes
        .find(item => item.id === contactDetail.contactDetailTypeId).name;
      this.actioned.emit(contactDetail);
    }
  }
}
