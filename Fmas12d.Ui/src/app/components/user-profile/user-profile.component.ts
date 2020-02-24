import { Component, OnInit } from '@angular/core';
import { ContactDetailProfile } from 'src/app/interfaces/contact-detail-profile';
import { FormGroup, FormBuilder } from '@angular/forms';
import { GenderType } from 'src/app/interfaces/gender-type';
import { GenderTypeService } from 'src/app/services/gender-type/gender-type.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { PROFILE_TYPE_AMHP, PROFILE_TYPE_GP, PROFILE_TYPE_PSYCHIATRIST, SECTION12_APPROVED, 
  CONTACT_DETAIL_TYPE_BASE, CONTACT_DETAIL_TYPE_HOME } from 'src/app/constants/Constants';
import { SpecialitiesService } from 'src/app/services/specialities/specialities.service';
import { Speciality } from 'src/app/interfaces/speciality';
import { UserProfile } from 'src/app/interfaces/user-profile';
import { UserProfileService } from 'src/app/services/user-profile/user-profile.service';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  dropdownSettings: IDropdownSettings;
  genderTypes: GenderType[];
  isGPOrPsychiatrist: boolean;
  selectedSpecialities: NameIdList[];
  specialities: Speciality[];
  public userProfile: UserProfile;
  userProfileForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private genderTypeService: GenderTypeService,
    private specialitiesService: SpecialitiesService,
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
      this.isGPOrPsychiatrist 
        = result.profileTypeId === PROFILE_TYPE_GP || 
          result.profileTypeId === PROFILE_TYPE_PSYCHIATRIST;
    });
    this.genderTypeService.GetGenderTypes().subscribe((result: GenderType[]) => this.genderTypes = result);
    this.specialitiesService.GetSpecialities().subscribe((result: Speciality[]) => this.specialities = result);

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
    this.specialities.forEach(specialityType => {
      if (this.userProfile.userSpecialities && this.userProfile.userSpecialities.find(item => item.specialityId === specialityType.id)) {
        const speciality = { id: specialityType.id, name: specialityType.name } as NameIdList;
        this.selectedSpecialities.push(speciality);
      }      
    });
    this.userProfileForm.controls.specialities.setValue(this.selectedSpecialities);
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
    return `${contactDetail.address1}${contactDetail.town ? ', ' + contactDetail.town : ''}, ${contactDetail.postcode}`;
  }

  public VSRNumber() {
    return this.userProfile.vsrNumber;
  }

  public EditContactDetail(contactDetail: ContactDetailProfile) {

  }

  public DeleteContactDetail(contactDetail: ContactDetailProfile) {
    this.userProfile.contactDetails = this.userProfile.contactDetails.filter(item => item.id !== contactDetail.id);
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

  AddContactDetail() {

  }

  MobileNumberIsMandatory() {
    return this.userProfile.profileTypeId === PROFILE_TYPE_AMHP;
  }

  UserHasAllContactDetails() {
    return this.userProfile.contactDetails.find(item => item.contactDetailTypeId === CONTACT_DETAIL_TYPE_BASE) &&
    this.userProfile.contactDetails.find(item => item.contactDetailTypeId === CONTACT_DETAIL_TYPE_HOME);
  }

  Cancel() {

  }

  Save() {
    
  }

}
