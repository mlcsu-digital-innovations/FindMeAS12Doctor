import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ContactDetailProfile } from 'src/app/interfaces/contact-detail-profile';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { PostcodeRegex } from 'src/app/constants/Constants';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserProfile } from 'src/app/interfaces/user-profile';

@Component({
  selector: 'app-user-contact-detail-modal',
  templateUrl: './user-contact-detail-modal.component.html',
  styleUrls: ['./user-contact-detail-modal.component.css']
})
export class UserContactDetailModalComponent implements OnInit {
  @Output() actioned = new EventEmitter<any>();
  @Input() public contactDetail: ContactDetailProfile;
  @Input() public userProfile: UserProfile;
  
  contactDetailForm: FormGroup;
  contactDetailTypes: NameIdList[];
  isSearchingForPostcode: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private nameIdListService: NameIdListService,
    private postcodeValidationService: PostcodeValidationService,
    private toastService: ToastService
  ) { }

  ngOnInit() {    
    this.contactDetailForm = this.formBuilder.group({
      address1: [''],
      address2: [''],
      address3: [''],
      contactDetailType: ['1'],
      latitude: [''],
      longitude: [''],
      mobile: [''],
      postcode: [
        '',
        [       
          Validators.minLength(6),        
          Validators.maxLength(8),
          Validators.pattern(`${PostcodeRegex}$`)
        ]
      ],
      email: [''],
      telephone: [''],
      town: ['']
    });

    this.nameIdListService.GetListData('contactdetailtype').subscribe((result: NameIdList[]) => {
      this.contactDetailTypes = result;
    });

    if (this.contactDetail) {
      this.contactDetailTypeField.disable();
      this.InitialiseForm();
    }
    else if (this.userProfile.contactDetails && this.userProfile.contactDetails.length === 1) {
      let remainingContactDetailTypeId: number = this.contactDetailTypes.find(item => item.id != this.userProfile.contactDetails[0].contactDetailTypeId).id;
      this.contactDetailTypes = this.contactDetailTypes.filter(item => item.id == remainingContactDetailTypeId);
      this.contactDetailTypeField.setValue(remainingContactDetailTypeId);            
    }
  }

  InitialiseForm() {    
    this.address1Field.setValue(this.contactDetail.address1);
    this.address2Field.setValue(this.contactDetail.address2);
    this.address3Field.setValue(this.contactDetail.address3);
    this.contactDetailTypeField.setValue(this.contactDetail.contactDetailTypeId);
    this.latitudeField.setValue(this.contactDetail.latitude);
    this.longitudeField.setValue(this.contactDetail.longitude);
    this.mobileField.setValue(this.contactDetail.mobileNumber);
    this.postcodeField.setValue(this.contactDetail.postcode);
    this.emailField.setValue(this.contactDetail.email);
    this.telephoneField.setValue(this.contactDetail.telephoneNumber);
    this.townField.setValue(this.contactDetail.town);
  }

  get address1Field() {
    return this.contactDetailForm.controls.address1;
  }

  get address2Field() {
    return this.contactDetailForm.controls.address2;
  }

  get address3Field() {
    return this.contactDetailForm.controls.address3;
  }

  get contactDetailTypeField() {
    return this.contactDetailForm.controls.contactDetailType;
  }

  get emailField() {
    return this.contactDetailForm.controls.email;
  }

  get latitudeField() {
    return this.contactDetailForm.controls.latitude;
  }

  get longitudeField() {
    return this.contactDetailForm.controls.longitude;
  }

  get mobileField() {
    return this.contactDetailForm.controls.mobile;
  }

  get postcodeField() {
    return this.contactDetailForm.controls.postcode;
  }

  get telephoneField() {
    return this.contactDetailForm.controls.telephone;
  }

  get townField() {
    return this.contactDetailForm.controls.town;
  }

  FormatPostcode() {
    let postcode = this.postcodeField.value.trim();
    if (postcode.indexOf(' ') === -1 && postcode.length > 3) {
      const inwardCode = postcode.substr(postcode.length - 3, 3);
      const outwardCode = postcode.substr(0, postcode.length - 3);
      postcode = `${outwardCode} ${inwardCode}`;
    }
    this.postcodeField.setValue(postcode);
  }

  HasInvalidPostcode(): boolean {    
    return (
      this.postcodeField.value === '' ||
      this.postcodeField.value === null || 
      this.postcodeField.errors !== null
    );
  }

  HasValidPostcode(): boolean {
    return (
      this.postcodeField.value !== '' &&
      this.postcodeField.value !== null &&
      this.postcodeField.errors === null
    );
  }

  IsSearchingForPostcode(): boolean {
    return this.isSearchingForPostcode;
  }

  ValidatePostcode(): void {    
    this.isSearchingForPostcode = true;
    this.FormatPostcode();

    this.postcodeValidationService.validatePostcode(this.postcodeField.value)
      .subscribe((result: any) => {        
        this.isSearchingForPostcode = false;
        this.latitudeField.setValue(result.latitude);
        this.longitudeField.setValue(result.longitude);
        this.postcodeField.setErrors(null);
        this.toastService.displaySuccess({          
          message: 'Postcode is valid'
        });
      }, (err) => {
        this.isSearchingForPostcode = false;
        this.latitudeField.reset();
        this.longitudeField.reset();
        this.postcodeField.setErrors({InvalidPostcode: true});
        this.toastService.displayError({
          title: 'Search Error',
          message: 'Error Retrieving Address Information'
        });
      });
  }

  Cancel() {
    this.actioned.emit(null);
  }

  SaveContactDetail() {

    let canContinue: boolean = true;
    
    // contactDetailTypeId
    if (!(this.contactDetailTypeField.value && this.contactDetailTypeField.value > 0)) {
      canContinue = false;
      this.contactDetailTypeField.setErrors({ InvalidContactDetailType: true});
    }    

    // postcode
    if (this.postcodeField.value == 0 && !this.HasValidPostcode()) {
      canContinue = false;
      this.postcodeField.setErrors({ InvalidPostcode: true });      
    }     

    if (canContinue) {
      const contactDetail = {} as ContactDetailProfile;
      contactDetail.address1 = this.address1Field.value;
      contactDetail.address2 = this.address2Field.value;
      contactDetail.address3 = this.address3Field.value;
      contactDetail.contactDetailTypeId = this.contactDetailTypeField.value as number;
      contactDetail.mobileNumber = this.mobileField.value;
      contactDetail.name = this.contactDetailTypes.find(item => item.id == contactDetail.contactDetailTypeId).name;
      contactDetail.postcode = this.postcodeField.value;
      contactDetail.telephoneNumber = this.telephoneField.value;
      contactDetail.town = this.townField.value;
  
      if (this.contactDetail) {
        contactDetail.id = this.contactDetail.id;
      }
  
      this.actioned.emit(contactDetail);
    }
    
  }
}
