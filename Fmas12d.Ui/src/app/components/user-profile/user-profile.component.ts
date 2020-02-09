import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { UserProfile } from 'src/app/interfaces/user-profile';
import { UserProfileService } from 'src/app/services/user-profile/user-profile.service';
import { GenderType } from 'src/app/interfaces/gender-type';
import { GenderTypeService } from 'src/app/services/gender-type/gender-type.service';
import { PROFILE_TYPE_GP, PROFILE_TYPE_PSYCHIATRIST } from 'src/app/constants/Constants';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  genderTypes: GenderType[];
  isAMHPProfile: boolean;
  userProfile: UserProfile;
  userProfileForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private genderTypeService: GenderTypeService,
    private userProfileService: UserProfileService
  ) { }
  
  ngOnInit() {
    this.userProfileService.GetUser().subscribe((result: UserProfile) => {
      this.userProfile = result;      
      this.isAMHPProfile 
        = result.profileTypeId !== PROFILE_TYPE_GP && 
          result.profileTypeId !== PROFILE_TYPE_PSYCHIATRIST;
    });
    this.genderTypeService.GetGenderTypes().subscribe((result: GenderType[]) => this.genderTypes = result);

    this.userProfileForm = this.formBuilder.group({
      displayName: this.userProfile.displayName,      
      genderTypeId: this.userProfile.genderTypeId,
      emailAddress: this.userProfile.emailAddress,
      mobileNumber: this.userProfile.mobileNumber,
      telephoneNumber: this.userProfile.telephoneNumber
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

  Cancel() {

  }

  Save() {
    
  }

}
