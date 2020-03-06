import { BankDetailsProfile } from 'src/app/interfaces/bank-details-profile';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ContactDetailProfile } from 'src/app/interfaces/contact-detail-profile';
import { FormGroup, FormBuilder, Validators, FormArray, ValidatorFn, AbstractControl } from '@angular/forms';
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
  readonly section12Approved: number = SECTION12_APPROVED;
  selectedContactDetail: ContactDetailProfile;
  selectedFinanceDetail: BankDetailsProfile;
  allSpecialities$: Observable<NameIdList[]>;
  specialities: NameIdList[];
  userProfile: UserProfile;
  userProfileForm: FormGroup;
  userContactDetailModal: NgbModalRef;
  userFinanceDetailModal: NgbModalRef;
  
  @ViewChild('addUserContactDetailModal', null) addUserContactDetailTemplate;
  @ViewChild('editUserContactDetailModal', null) editUserContactDetailTemplate;
  @ViewChild('deleteUserContactDetailModal', null) deleteUserContactDetailTemplate;
  @ViewChild('addUserFinanceDetailModal', null) addUserFinanceDetailTemplate;
  @ViewChild('editUserFinanceDetailModal', null) editUserFinanceDetailTemplate;
  @ViewChild('deleteUserFinanceDetailModal', null) deleteUserFinanceDetailTemplate;

  constructor(
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private nameIdListService: NameIdListService,
    private toastService: ToastService,
    public userProfileService: UserProfileService
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

    this.genderTypes$ = this.nameIdListService.GetListData('gendertype');    
    this.allSpecialities$ = this.nameIdListService.GetListData('speciality');              

    this.userProfileService.loading(true);
    this.userProfileService.GetUser().subscribe((result: UserProfile) => {      
      this.PopulateFormFields(result);
      this.userProfileService.loading(false);
    }, err => {
      this.userProfileService.loading(false);
    });
    
  }

  PopulateFormFields(userProfile: UserProfile) {    
    this.userProfile = userProfile;    
    if (this.userProfileForm) {
      this.userProfileForm.patchValue(this.userProfile);
    }       
    else {
      this.userProfileForm = this.formBuilder.group({
        displayName: [this.userProfile.displayName, Validators.required],      
        genderTypeId: [this.userProfile.genderTypeId, Validators.required],
        emailAddress: [this.userProfile.emailAddress, !this.userProfile.isDoctor 
          ? [Validators.required, Validators.email] 
          : null],
        mobileNumber: [this.userProfile.mobileNumber, this.userProfile.isAmhp ? Validators.required : null],
        telephoneNumber: this.userProfile.telephoneNumber,
        gmcNumber: [this.userProfile.gmcNumber, this.userProfile.isDoctor ? Validators.required : null],
        specialities: [this.userProfile.userSpecialities, this.userProfile.isDoctor ? Validators.required : null],
        contactDetails: [this.userProfile.contactDetails, this.userProfile.isDoctor ? this.ContactDetailsBaseRequired : null],
        bankDetails: [this.userProfile.bankDetails, this.userProfile.isDoctor ? Validators.required : null]
      });  
    }    
  }

  get controls() {
    return this.userProfileForm.controls;
  }   

  ContactDetailsBaseRequired(control: AbstractControl) {       
    if (!control.value || 
      control.value.length === 0 || 
      !control.value.find((item: ContactDetailProfile) => item.contactDetailTypeId == CONTACT_DETAIL_TYPE_BASE)) {
      return { baseRequired: true };
    }
    else {
      return null;
    }  
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

  AddFinanceDetail() {
    this.userFinanceDetailModal = this.modalService.open(
      this.addUserFinanceDetailTemplate,
      { size: 'lg' }
    );   
  }

  EditFinanceDetail(financeDetail: BankDetailsProfile) {
    this.selectedFinanceDetail = financeDetail;
    this.userFinanceDetailModal = this.modalService.open(
      this.editUserFinanceDetailTemplate,
      { size: 'lg' }
    );  
  }

  DeleteFinanceDetail(financeDetail: BankDetailsProfile) {    
    this.selectedFinanceDetail = financeDetail;
    this.deleteModal = this.modalService.open(this.deleteUserFinanceDetailTemplate, {
      size: 'lg'
    });
  }

  VerifyGMCNumber() {

  }

  UserHasAllContactDetails() {
    return this.controls.contactDetails.value.find(item => item.contactDetailTypeId === CONTACT_DETAIL_TYPE_BASE) &&
    this.controls.contactDetails.value.find(item => item.contactDetailTypeId === CONTACT_DETAIL_TYPE_HOME);
  }

  Cancel() {

  }

  Save() {   
    if (this.userProfileForm.valid) {      
      const result: UserProfile = this.GetUserProfileFromForm();      
      console.log('Updated', result);
      this.userProfile = result;    
      this.UpdateUser('User Profile', 'updated', 'update error');     
    }
    else {
      console.log('Invalid', this.userProfileForm.value);
    }
  }

  OnContactDetailModalActionAdd(userContactDetail: ContactDetailProfile) {
    this.userContactDetailModal.close();
    if (userContactDetail)
    {            
      this.userProfile.contactDetails.push(userContactDetail);  
      this.UpdateUser('Contact detail', 'added', 'add error');    
    }
    else {
      this.toastService.displayInfo({ message: "Contact Detail add has been cancelled" });      
    } 
  }

  OnContactDetailModalActionEdit(userContactDetail: ContactDetailProfile) {
    this.userContactDetailModal.close();
    if (userContactDetail)
    {                          
      let i = this.userProfile.contactDetails.findIndex(item => item.id === userContactDetail.id);
      this.userProfile.contactDetails[i] = userContactDetail;       
      this.UpdateUser('Contact Detail', 'updated', 'update error');     
    }
    else {
      this.toastService.displayInfo({ message: "Contact Detail update has been cancelled" });      
    }    
  }

  OnDeleteContactDetailAction(action: boolean) {
    this.deleteModal.close();

    if (action) {      
      this.userProfile.contactDetails = this.userProfile.contactDetails
        .filter(item => item.contactDetailTypeId !== this.selectedContactDetail.contactDetailTypeId);
      this.UpdateUser('Contact Detail', 'deleted', 'delete error'); 
    }    
    else {
      this.toastService.displayInfo({ message: "Contact Detail delete has been cancelled" }); 
    }
  }

  OnFinanceDetailModalActionAdd(userFinanceDetail: BankDetailsProfile) {
    this.userFinanceDetailModal.close();
    if (userFinanceDetail)
    { 
      this.userProfile.bankDetails.push(userFinanceDetail);         
      this.UpdateUser('Finance Detail', 'added', 'add error');         
    }
    else {
      this.toastService.displayInfo({ message: "Finance Detail add has been cancelled" });      
    } 
  }

  OnFinanceDetailModalActionEdit(userFinanceDetail: BankDetailsProfile) {
    this.userFinanceDetailModal.close();
    if (userFinanceDetail)
    {            
      let i: number = this.userProfile.bankDetails.findIndex(item => item.id === userFinanceDetail.id);
      this.userProfile.bankDetails[i] = userFinanceDetail;
      this.UpdateUser('Finance Detail', 'updated', 'update error'); 
    }
    else {
      this.toastService.displayInfo({ message: "Finance Detail update has been cancelled" });      
    }    
  }

  OnDeleteFinanceDetailAction(action: boolean) {
    this.deleteModal.close();

    if (action) {                             
      this.userProfile.bankDetails = this.userProfile.bankDetails
        .filter(item => 
          !(item.id === this.selectedFinanceDetail.id && 
            item.ccgId === this.selectedFinanceDetail.ccgId)
        );
      this.UpdateUser('Finance Detail', 'deleted', 'delete error'); 
    }    
    else {
      this.toastService.displayInfo({ message: "Finance Detail delete has been cancelled" }); 
    }
  }

  GetUserProfileFromForm(): UserProfile {
    let result = this.userProfileForm.value as UserProfile;
    result.contactDetailTypeId = this.userProfile.contactDetailTypeId;      
    result.id = this.userProfile.id;
    result.isAmhp = this.userProfile.isAmhp;
    result.isDoctor = this.userProfile.isDoctor;
    result.isFinance = this.userProfile.isFinance;    
    result.organisationName = this.userProfile.organisationName;
    result.profileTypeId = this.userProfile.profileTypeId;
    result.section12ApprovalStatusId = this.userProfile.section12ApprovalStatusId;
    result.section12ExpiryDate = this.userProfile.section12ExpiryDate;    

    return result;
  }

  private UpdateUser(elementUpdated: string, success: string, error: string): void {
    this.userProfileService.UpdateUser(this.userProfile).subscribe(result => {
      this.PopulateFormFields(result);
      this.toastService.displaySuccess({ message: `${elementUpdated} ${success}`});
    }, err => {
      this.toastService.displayError({ message: `${elementUpdated} ${error}`});
    }); 
  }
}
