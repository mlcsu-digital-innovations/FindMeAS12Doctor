import { BankDetailsProfile } from 'src/app/interfaces/bank-details-profile';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ContactDetailProfile } from 'src/app/interfaces/contact-detail-profile';
import { FormGroup, FormBuilder, Validators, FormArray, ValidatorFn, AbstractControl } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { RouterService } from 'src/app/services/router/router.service';
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
  cancelModal: NgbModalRef;
  deleteModal: NgbModalRef;
  dropdownSettings: IDropdownSettings;
  genderTypes$: Observable<NameIdList[]>;
  saveModal: NgbModalRef;
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
  @ViewChild('saveUserProfileModal', null) saveUserProfileTemplate;
  @ViewChild('cancelUserProfileModal', null) cancelUserProfileTemplate;

  constructor(
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private nameIdListService: NameIdListService,
    private routerService: RouterService,
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
    this.userProfileService.GetUser().subscribe((userProfile: UserProfile) => {
      this.PopulateFormFields(userProfile);
      this.userProfileService.loading(false);
    }, error => {
      if (error.error && error.error.title) {
        this.toastService.displayError(
          {
            title: 'User Profile load error',
            message: error.error.title
          }
        );
      }
      this.toastService.displayError({message: 'User Profile load error'});
      this.userProfileService.loading(false);
    });

  }

  PopulateFormFields(userProfile: UserProfile) {
    this.userProfile = userProfile;
    if (this.userProfileForm) {
      this.userProfileForm.patchValue(this.userProfile);
    } else {
      this.userProfileForm = this.formBuilder.group({
        displayName: [this.userProfile.displayName, Validators.required],
        genderTypeId: [this.userProfile.genderTypeId, Validators.required],
        emailAddress: [this.userProfile.emailAddress, !this.userProfile.isDoctor
          ? [Validators.required, Validators.email]
          : null],
        mobileNumber: [
          this.userProfile.mobileNumber,
          this.userProfile.isAmhp
          ? [
             Validators.required,
             Validators.pattern(/^[0-9\s]*$/)
            ]
          : [
            Validators.pattern(/^[0-9\s]*$/)
            ]
        ],
        telephoneNumber: [
          this.userProfile.telephoneNumber,
          [
            Validators.pattern(/^[0-9\s]*$/)
          ]
        ],
        gmcNumber: [
          this.userProfile.gmcNumber, this.userProfile.isDoctor
          ? [
            Validators.required,
            Validators.pattern(/^\d{7}$/)
          ]
          : null
        ],
        specialities: [this.userProfile.userSpecialities,
          this.userProfile.isDoctor ?
          Validators.required : null
        ],
        contactDetails: [
          this.userProfile.contactDetails,
          this.userProfile.isDoctor ?
          this.ContactDetailsBaseRequired : null
        ],
        bankDetails: [
          this.userProfile.bankDetails,
          this.userProfile.isDoctor ? Validators.required : null
        ]
      });

    }
  }

  get controls() {
    return this.userProfileForm.controls;
  }

  ContactDetailsBaseRequired(control: AbstractControl) {
    if (!control.value || control.value.length === 0 ||
      !control.value.find((item: ContactDetailProfile) =>
        item.contactDetailTypeId === CONTACT_DETAIL_TYPE_BASE)
      ) {
        return { baseRequired: true
      };
    } else {
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
    return this.controls.contactDetails.value
      .find(item => item.contactDetailTypeId === CONTACT_DETAIL_TYPE_BASE) &&
    this.controls.contactDetails.value
      .find(item => item.contactDetailTypeId === CONTACT_DETAIL_TYPE_HOME);
  }

  Cancel(): void {
    if (this.userProfileForm.dirty) {
      this.cancelModal = this.modalService.open(this.cancelUserProfileTemplate, {
        size: 'lg'
      });
    } else {
      // TODO replace with switch statement
      this.routerService.navigate(['/referral']);
    }
  }

  Save(): void {
    if (this.userProfileForm.valid) {
      this.saveModal = this.modalService.open(this.saveUserProfileTemplate, {
        size: 'lg'
      });
    }
  }

  OnCancelModalAction(action: boolean) {
    this.cancelModal.close();

    if (action) {
      this.toastService.displayInfo({ message: 'User Update has been cancelled' });
      this.routerService.navigate(['/referral']);
    }
  }

  OnContactDetailModalActionAdd(userContactDetail: ContactDetailProfile) {
    this.userContactDetailModal.close();
    if (userContactDetail) {
      this.controls.contactDetails.value.push(userContactDetail);
      this.controls.contactDetails.updateValueAndValidity();
      this.toastService.displaySuccess({ message: `Contact Detail added`});
    } else {
      this.toastService.displayInfo({ message: 'Contact Detail add has been cancelled' });
    }
  }

  OnContactDetailModalActionEdit(userContactDetail: ContactDetailProfile) {
    this.userContactDetailModal.close();
    if (userContactDetail) {
      const formContactDetails: ContactDetailProfile[] = this.controls.contactDetails.value;
      const updatedContactDetailIndex: number = formContactDetails
        .findIndex(item => item.contactDetailTypeId === userContactDetail.contactDetailTypeId);
      if (updatedContactDetailIndex > -1) {
        formContactDetails[updatedContactDetailIndex] = userContactDetail;
      }

      this.controls.contactDetails.setValue(formContactDetails);
      this.toastService.displaySuccess({ message: `Contact Detail updated`});
    } else {
      this.toastService.displayInfo({ message: 'Contact Detail update has been cancelled' });
    }
  }

  OnDeleteContactDetailAction(action: boolean) {
    this.deleteModal.close();

    if (action) {
      this.controls.contactDetails.setValue(this.controls.contactDetails.value
        .filter(item =>
          item.contactDetailTypeId !== this.selectedContactDetail.contactDetailTypeId
        )
      );
      this.toastService.displaySuccess({ message: `Contact Detail deleted`});
    } else {
      this.toastService.displayInfo({ message: 'Contact Detail delete has been cancelled' });
    }
  }

  OnFinanceDetailModalActionAdd(userFinanceDetail: BankDetailsProfile) {
    this.userFinanceDetailModal.close();
    if (userFinanceDetail) {
      this.controls.bankDetails.value.push(userFinanceDetail);
      this.toastService.displaySuccess({ message: `Finance Detail added`});
    } else {
      this.toastService.displayInfo({ message: 'Finance Detail add has been cancelled' });
    }
  }

  OnFinanceDetailModalActionEdit(userFinanceDetail: BankDetailsProfile) {
    this.userFinanceDetailModal.close();
    if (userFinanceDetail) {
      const formBankDetails: BankDetailsProfile[] = this.controls.bankDetails.value;
      const updatedBankDetailIndex: number = formBankDetails
        .findIndex(item => item.ccgId === userFinanceDetail.ccgId);
      if (updatedBankDetailIndex > -1) {
        formBankDetails[updatedBankDetailIndex] = userFinanceDetail;
      }
      this.toastService.displaySuccess({ message: `Finance Detail updated`});
    } else {
      this.toastService.displayInfo({ message: 'Finance Detail update has been cancelled' });
    }
  }

  OnDeleteFinanceDetailAction(action: boolean) {
    this.deleteModal.close();

    if (action) {
      this.controls.bankDetails.setValue(this.controls.bankDetails.value
        .filter(item =>
          !(item.id === this.selectedFinanceDetail.id &&
            item.ccgId === this.selectedFinanceDetail.ccgId)
        ));
      this.toastService.displaySuccess({ message: `Finance Detail deleted`});
    } else {
      this.toastService.displayInfo({ message: 'Finance Detail delete has been cancelled' });
    }
  }

  OnSaveUserProfileAction(action: boolean) {
    this.saveModal.close();

    if (action) {
      const userProfile: UserProfile = this.GetUserProfileFromForm();
      this.userProfileService.loading(true);
      this.userProfileService.UpdateUser(userProfile).subscribe(() => {
        this.toastService.displaySuccess({ message: 'User Updated'});
        this.userProfileService.loading(false);
        // TODO replace with switch statement
        this.routerService.navigate(['/referral']);
      }, error => {
        if (error.error && error.error.title) {
          this.toastService.displayError(
            {
              title: 'User Update error',
              message: error.error.title
            }
          );
        } else {
          this.toastService.displayError({message: 'User Update error'});
        }
        this.userProfileService.loading(false);
      });
    } else {
      this.toastService.displayInfo({ message: 'User Update has been cancelled' });
    }
  }

  GetUserProfileFromForm(): UserProfile {
    const userProfile = this.userProfileForm.value as UserProfile;
    userProfile.userSpecialities = this.controls.specialities.value;
    userProfile.contactDetailTypeId = this.userProfile.contactDetailTypeId;
    userProfile.id = this.userProfile.id;
    userProfile.isAdmin = this.userProfile.isAdmin;
    userProfile.isAmhp = this.userProfile.isAmhp;
    userProfile.isDoctor = this.userProfile.isDoctor;
    userProfile.isFinance = this.userProfile.isFinance;
    userProfile.organisationName = this.userProfile.organisationName;
    userProfile.profileTypeId = this.userProfile.profileTypeId;
    userProfile.section12ApprovalStatusId = this.userProfile.section12ApprovalStatusId;
    userProfile.section12ExpiryDate = this.userProfile.section12ExpiryDate;

    return userProfile;
  }
}
