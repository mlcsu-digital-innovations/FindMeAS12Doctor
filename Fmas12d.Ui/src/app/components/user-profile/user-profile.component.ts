import { Component, OnInit, ViewChild } from '@angular/core';
import { ContactDetailProfile } from 'src/app/interfaces/contact-detail-profile';
import { FormGroup, FormBuilder } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { SECTION12_APPROVED, CONTACT_DETAIL_TYPE_BASE, CONTACT_DETAIL_TYPE_HOME } from 'src/app/constants/Constants';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserProfile } from 'src/app/interfaces/user-profile';
import { UserProfileService } from 'src/app/services/user-profile/user-profile.service';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  deleteModal: NgbModalRef;
  dropdownSettings: IDropdownSettings;
  genderTypes$: Observable<NameIdList[]>
  selectedContactDetail: ContactDetailProfile;
  selectedSpecialities: NameIdList[];
  specialities: NameIdList[];
  userProfile: UserProfile;
  userProfileForm: FormGroup;
  userContactDetailModal: NgbModalRef;
  
  @ViewChild('addUserContactDetailModal', null) addUserContactDetailTemplate;
  @ViewChild('editUserContactDetailModal', null) editUserContactDetailTemplate;
  @ViewChild('deleteUserContactDetailModal', null) deleteUserContactDetailTemplate;

  constructor(
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private nameIdListService: NameIdListService,
    private toastService: ToastService,
    private userProfileService: UserProfileService
  ) { }
  
  ngOnInit() {
    this.dropdownSettings = {
      allowSearchFilter: false,
      enableCheckAll: false,
      idField: 'id',
      itemsShowLimit: 5,
      singleSelection: false,
      textField: 'name',
    };

    this.userProfileService.GetUser().subscribe((result: UserProfile) => {
      this.userProfile = result;     
    });
    this.genderTypes$ = this.nameIdListService.GetListData('gendertype');
    

    this.userProfileForm = this.formBuilder.group({
      displayName: this.userProfile.displayName,      
      genderTypeId: this.userProfile.genderTypeId,
      emailAddress: this.userProfile.emailAddress,
      mobileNumber: this.userProfile.mobileNumber,
      telephoneNumber: this.userProfile.telephoneNumber,
      gmcNumber: this.userProfile.gmcNumber,
      specialities: ['']
    });
    
    this.selectedSpecialities = [];
    this.nameIdListService.GetListData('speciality').subscribe((result: NameIdList[]) => {
      this.specialities = result;
      this.specialities.forEach((specialityType: NameIdList) => {
        if (this.userProfile.userSpecialities && this.userProfile.userSpecialities.find(item => item.specialityId === specialityType.id)) {
          const speciality = { id: specialityType.id, name: specialityType.name } as NameIdList;
          this.selectedSpecialities.push(speciality);
        }      
      });
      this.userProfileForm.controls.specialities.setValue(this.selectedSpecialities);
    });    
  }

  get displayNameField() {
    return this.userProfileForm.controls.displayName;
  }

  get emailAddressField() {
    return this.userProfileForm.controls.emailAddress;
  }

  get genderTypeIdField() {
    return this.userProfileForm.controls.genderTypeId;
  }

  get mobileNumberField() {
    return this.userProfileForm.controls.mobileNumber;
  }

  get telephoneNumberField() {
    return this.userProfileForm.controls.telephoneNumber;
  }

  public S12Status() {
    return this.userProfile.section12ApprovalStatusId === SECTION12_APPROVED 
      ? 'Approved' 
      : 'Not Approved';
  }

  public FullAddress(contactDetail: ContactDetailProfile) {
    return `${contactDetail.address1 ? contactDetail.address1 : ''}${contactDetail.town ? ', ' + contactDetail.town : ''}`;
  }  

  AddContactDetail() {
    this.userContactDetailModal = this.modalService.open(
      this.addUserContactDetailTemplate,
      { size: 'lg' }
    );   
  }

  EditContactDetail(contactDetail: ContactDetailProfile) {
    this.selectedContactDetail = contactDetail;
    this.userContactDetailModal = this.modalService.open(
      this.editUserContactDetailTemplate,
      { size: 'lg' }
    );  
  }

  DeleteContactDetail(contactDetail: ContactDetailProfile) {    
    this.selectedContactDetail = contactDetail;
    this.deleteModal = this.modalService.open(this.deleteUserContactDetailTemplate, {
      size: 'lg'
    });
  }

  VerifyGMCNumber() {

  }

  OnItemDeselect(item: any) {
    this.selectedSpecialities =
      this.selectedSpecialities.filter(obj => obj.id !== item.id);
  }

  OnItemSelect(item: NameIdList) {
    this.selectedSpecialities.push(item);
  }  

  MobileNumberIsMandatory() {
    return this.userProfile.isAmhp;
  }

  UserHasAllContactDetails() {
    return this.userProfile.contactDetails.find(item => item.contactDetailTypeId === CONTACT_DETAIL_TYPE_BASE) &&
    this.userProfile.contactDetails.find(item => item.contactDetailTypeId === CONTACT_DETAIL_TYPE_HOME);
  }

  Cancel() {

  }

  Save() {
    let canContinue: boolean = true;

    // check displayname
    if (!this.displayNameField.value) {
      this.displayNameField.setErrors({ InvalidDisplayName: true });
      canContinue = false;
    }

    // check gender
    if (!this.genderTypeIdField.value) {
      this.genderTypeIdField.setErrors({ InvalidGenderType: true });
      canContinue = false;
    }

    if (this.userProfile.isDoctor) {
      // check specialities
      if (this.selectedSpecialities.length === 0) {        
        canContinue = false;
      }

      // check GMC number

      // check contact details
      if (!this.userProfile.contactDetails || this.userProfile.contactDetails.length === 0 || 
         (this.userProfile.contactDetails.length === 1 &&
          this.userProfile.contactDetails[0].contactDetailTypeId !== CONTACT_DETAIL_TYPE_BASE)) {
        canContinue = false;
      }
    }
    else {
      // check email
      if (!this.emailAddressField.value) {
        this.emailAddressField.setErrors({ InvalidEmailAddress: true });
        canContinue = false;
      }

      if (this.userProfile.isAmhp) {
        if (!this.mobileNumberField.value) {
          this.mobileNumberField.setErrors({ InvalidMobileNumber: true });
          canContinue = false;
        }
      }
    }
  }

  OnModalActionAdd(userContactDetail: ContactDetailProfile) {
    this.userContactDetailModal.close();
    if (userContactDetail)
    { 
      this.userProfile.contactDetails.push(userContactDetail);
      this.toastService.displaySuccess({ message: "Contact Detail added" });
    }
    else {
      this.toastService.displayInfo({ message: "Contact Detail add has been cancelled" });      
    } 
  }

  OnModalActionEdit(userContactDetail: ContactDetailProfile) {
    this.userContactDetailModal.close();
    if (userContactDetail)
    {      
      this.toastService.displaySuccess({ message: "Contact Detail updated" });
      let i: number = this.userProfile.contactDetails.findIndex(item => item.id === userContactDetail.id);
      this.userProfile.contactDetails[i] = userContactDetail;
    }
    else {
      this.toastService.displayInfo({ message: "Contact Detail update has been cancelled" });      
    }    
  }

  OnDeleteContactDetailAction(action: boolean) {
    this.deleteModal.close();

    if (action) {      
      this.toastService.displaySuccess({ message: "Contact Detail deleted" });                   
      this.userProfile.contactDetails = this.userProfile.contactDetails
        .filter(item => item.contactDetailTypeId !== this.selectedContactDetail.contactDetailTypeId);
    }    
    else {
      this.toastService.displayInfo({ message: "Contact Detail delete has been cancelled" }); 
    }
  }

}
