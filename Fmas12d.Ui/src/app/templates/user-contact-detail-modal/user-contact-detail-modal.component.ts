import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ContactDetailProfile } from 'src/app/interfaces/contact-detail-profile';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
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
      contactDetailTypeId: [
        '1',
        Validators.required
      ],
      emailAddress: ['', Validators.email],
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
      telephoneNumber: [''],
      town: ['']
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
        }
        else if (this.contactDetails && this.contactDetails.length === 1) {
          let remainingContactDetailTypeId: number = this.contactDetailTypes
            .find(item => item.id != this.contactDetails[0].contactDetailTypeId).id;
          this.contactDetailTypes = this.contactDetailTypes
            .filter(item => item.id == remainingContactDetailTypeId);
          this.controls.contactDetailTypeId.setValue(remainingContactDetailTypeId);            
        }
      });
  }

  get controls() {
    return this.contactDetailForm.controls;
  }  

  ClearCoordinates() {
    this.controls.latitude.setValue(null);
    this.controls.longitude.setValue(null);
    if (this.controls.postcode.value.length > 0) {
      this.controls.postcode.setErrors({ InvalidPostcode: true });
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

  ValidatePostcode(): void {    
    this.isSearchingForPostcode = true;
    this.FormatPostcode();

    this.postcodeValidationService.validatePostcode(this.controls.postcode.value)
      .subscribe((postcodeDetails: any) => {        
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
      });
  }

  Cancel() {
    this.actioned.emit(null);
  }

  SaveContactDetail() {
    if (this.contactDetailForm.valid) {
      this.controls.contactDetailTypeId.enable();
      this.controls.latitude.enable();
      this.controls.longitude.enable();
      const contactDetail = this.contactDetailForm.value as ContactDetailProfile;
      contactDetail.id = this.contactDetail ? this.contactDetail.id : 0;
      contactDetail.contactDetailTypeName = this.contactDetailTypes
        .find(item => item.id == contactDetail.contactDetailTypeId).name;
      this.actioned.emit(contactDetail);
    }        
  }
}
