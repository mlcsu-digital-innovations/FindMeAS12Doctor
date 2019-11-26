import { Component, OnInit, ViewChild, Renderer2 } from '@angular/core';
import { debounceTime, tap, catchError, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { DoctorListService } from 'src/app/services/doctor-list/doctor-list.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserDetails } from 'src/app/interfaces/user-details';
import { UserDetailsService } from 'src/app/services/user/user-details.service';
import { RouterService } from 'src/app/services/router/router.service';
import { UnregisteredUserService } from 'src/app/services/unregistered-user/unregistered-user.service';
import { UnregisteredUser } from 'src/app/interfaces/unregistered-user';
import { PostcodeRegex } from 'src/app/constants/Constants';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { AddressResult } from 'src/app/interfaces/address-result';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';

@Component({
  selector: 'app-doctor-add',
  templateUrl: './doctor-add.component.html',
  styleUrls: ['./doctor-add.component.css']
})
export class DoctorAddComponent implements OnInit {

  addressList: AddressResult[] = [];
  cancelModal: NgbModalRef;
  genderTypes: NameIdList[];
  hasDoctorSearchFailed: boolean;
  hasRegisteredDoctorDetails: boolean;
  hasUnregisteredUser: boolean;
  isRegisteredDoctorSearching: boolean;
  isRegisteredDoctorValidated: boolean;
  isPostcodeSearching: boolean;
  isUnregisteredSearchComplete: boolean;
  multipleUsersModal: NgbModalRef;
  registeredDoctorDetails: UserDetails;
  registeredDoctorForm: FormGroup;
  unregisteredDoctorForm: FormGroup;
  unregisteredUser: UnregisteredUser;
  unregisteredUsers: UnregisteredUser[];

  @ViewChild('cancelAllocation', null) cancelAllocationTemplate;
  @ViewChild('unregisteredUserResults', { static: true }) unregisteredUserResults;

  constructor(
    private doctorListService: DoctorListService,
    private formBuilder: FormBuilder,
    private nameIdListService: NameIdListService,
    private modalService: NgbModal,
    private postcodeValidationService: PostcodeValidationService,
    private renderer: Renderer2,
    private routerService: RouterService,
    private toastService: ToastService,
    private unregisteredUserService: UnregisteredUserService,
    private userDetailsService: UserDetailsService
  ) { }

  ngOnInit() {
    this.registeredDoctorForm = this.formBuilder.group({
      registeredName: ['']
    });

    this.unregisteredDoctorForm = this.formBuilder.group({
      unregisteredName: [''],
      unregisteredGmcNumber: ['',
      [
        Validators.minLength(7),
        Validators.maxLength(7),
        Validators.pattern('^\d{7}$')
      ]],
      postcode: ['',
        [
          Validators.minLength(6),
          Validators.maxLength(8),
          Validators.pattern(`${PostcodeRegex}$`)
        ]
      ],
      contact: [''],
      organisation: [''],
      previousAssessments: [{disabled: true}],
      address: [''],
      gender: null,
      isSection12: false,
      unregisteredUserAddress: ['']
    });

    // get the list of genders for the dropdown
    this.nameIdListService.GetListData('gendertype')
      .subscribe(genders => {
        this.genderTypes = genders;
      },
        (err) => {
          this.toastService.displayError({
            title: 'Error',
            message: 'Error Retrieving Gender Data'
          });
        });
  }

  AddressSearch(): void {
    this.addressList = [];
    this.isPostcodeSearching = true;

    this.postcodeValidationService.searchPostcode(this.unregisteredPostcodeField.value)
      .subscribe(address => {
        this.addressList.push(address);
      }, (err) => {
        this.isPostcodeSearching = false;
        console.log(this.addressList);
        this.toastService.displayError({
          title: 'Search Error',
          message: 'Error Retrieving Address Information'
        });
      }, () => {
        this.isPostcodeSearching = false;
        // show an error if no matching addresses are returned
        if (this.addressList.length === 0) {
          this.unregisteredPostcodeField.setErrors({ NoResultsReturned: true });
        }
      });
  }

  AllocateRegisteredDoctor() {
    // ToDo: use a service to allocate the registered doctor
  }

  Cancel() {
    // if either form has been changed then ask the user for confirmation
    if (this.registeredDoctorForm.dirty) {
      this.cancelModal = this.modalService.open(this.cancelAllocationTemplate, {
        size: 'lg'
      });
    } else {
      this.routerService.navigatePrevious();
    }
  }


  ClearField(fieldName: string) {
    if (this.unregisteredDoctorForm.contains(fieldName)) {
      this.unregisteredDoctorForm.controls[fieldName].setValue('');
      this.SetFieldFocus(`#${fieldName}`);
      this.unregisteredDoctorForm.markAsDirty();
    }
  }

  async Delay(milliseconds: number) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
  }

  DisableFieldsForUnregisteredUser() {
    this.unregisteredAssessmentsField.disable();
    this.unregisteredContactField.disable();
    this.unregisteredGenderField.disable();
    this.unregisteredOrganisationField.disable();
    this.unregisteredPostcodeField.disable();
    this.unregisteredSection12Field.disable();
  }

  DisableIfParentIsDisabled(fieldName: string): boolean {
    return this.unregisteredDoctorForm.controls[fieldName].disabled;
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  get registeredDoctorField() {
    return this.registeredDoctorForm.controls.registeredName;
  }

  get unregisteredAssessmentsField() {
    return this.unregisteredDoctorForm.controls.previousAssessments;
  }

  get unregisteredContactField() {
    return this.unregisteredDoctorForm.controls.contact;
  }

  get unregisteredDoctorField() {
    return this.unregisteredDoctorForm.controls.unregisteredName;
  }

  get unregisteredGenderField() {
    return this.unregisteredDoctorForm.controls.gender;
  }

  get unregisteredGmcNumberField() {
    return this.unregisteredDoctorForm.controls.unregisteredGmcNumber;
  }

  get unregisteredOrganisationField() {
    return this.unregisteredDoctorForm.controls.organisation;
  }

  get unregisteredPostcodeField() {
    return this.unregisteredDoctorForm.controls.postcode;
  }

  get unregisteredSection12Field() {
    return this.unregisteredDoctorForm.controls.isSection12;
  }

  get unregisteredUserField() {
    return this.unregisteredDoctorForm.controls.unregisteredName;
  }

  HasDoctorBeenSelected() {
    return (typeof this.registeredDoctorField.value) === 'object';
  }

  HasInvalidPostcode(): boolean {
    return (
      this.unregisteredPostcodeField.value !== '' &&
      this.unregisteredPostcodeField.errors !== null
    );
  }

  HasValidPostcode(): boolean {
    return (
      this.unregisteredPostcodeField.value !== '' &&
      this.unregisteredPostcodeField.errors == null
    );
  }

  OnCancelModalAction(action: boolean) {
    this.cancelModal.close();
    if (action) {
      this.routerService.navigatePrevious();
    }
  }

  OnCancelUnregisteredUser() {
    this.isUnregisteredSearchComplete = false;
    this.multipleUsersModal.close();
  }

  OnSelectUnregisteredUser(user: UnregisteredUser) {
    this.unregisteredUser = user;
    this.hasUnregisteredUser = true;
    this.DisableFieldsForUnregisteredUser();
    this.SetFieldValues();
    this.multipleUsersModal.close();
  }

  RegisteredDoctorSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isRegisteredDoctorSearching = true)),
      switchMap(term =>
        this.doctorListService.GetDoctorList(term).pipe(
          tap(() => (this.hasDoctorSearchFailed = false)),
          catchError(() => {
            this.hasDoctorSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.isRegisteredDoctorSearching = false))
    )

  ResetAllFields() {
    this.unregisteredAssessmentsField.enable();
    this.unregisteredAssessmentsField.setValue('');
    this.unregisteredContactField.enable();
    this.unregisteredContactField.setValue('');
    this.unregisteredGenderField.enable();
    this.unregisteredGenderField.setValue('');
    this.unregisteredOrganisationField.enable();
    this.unregisteredOrganisationField.setValue('');
    this.unregisteredPostcodeField.enable();
    this.unregisteredPostcodeField.setValue('');
    this.unregisteredSection12Field.enable();
    this.unregisteredSection12Field.setValue(false);
  }

  SearchUnregisteredDoctor() {

    if (this.unregisteredUserField.value === '' && this.unregisteredGmcNumberField.value === '') {
      return;
    }

    this.ResetAllFields();

    this.unregisteredUserService.SearchUnregisteredUsers(this.unregisteredUserField.value, +this.unregisteredGmcNumberField.value)
    .subscribe((unregisteredUsers: UnregisteredUser[]) => {
      this.isUnregisteredSearchComplete = true;

      switch (unregisteredUsers.length) {
        case 0:
          this.toastService.displayInfo({
            title: 'Information',
            message: 'No existing unregistered users found'
          });
          this.hasUnregisteredUser = false;
          this.unregisteredUser = {} as UnregisteredUser;
          break;
        case 1:
          this.unregisteredUser = unregisteredUsers[0];
          this.hasUnregisteredUser = true;

          this.DisableFieldsForUnregisteredUser();
          this.SetFieldValues();

          this.toastService.displayInfo({
            title: 'Information',
            message: 'An existing unregistered user has been found'
          });
          break;
        default:
          this.unregisteredUsers = unregisteredUsers;
          this.multipleUsersModal = this.modalService.open(
            this.unregisteredUserResults,
            { size: 'lg' }
          );
          this.hasUnregisteredUser = false;
          this.unregisteredUser = {} as UnregisteredUser;
      }
    },
      (err) => {
        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving User Details'
        });
      });
  }

  async SetFieldFocus(fieldName: string) {
    // ToDo: Find a better way to do this !
    await this.Delay(100);
    this.renderer.selectRootElement(fieldName).focus();
  }

  SetFieldValues() {

    this.unregisteredDoctorField.setValue(this.unregisteredUser.displayName);
    this.unregisteredGmcNumberField.setValue(this.unregisteredUser.gmcNumber);

    this.unregisteredGenderField.setValue(this.unregisteredUser.genderId);
    this.unregisteredPostcodeField.setValue(this.unregisteredUser.postcode);
    this.unregisteredDoctorField.setValue(this.unregisteredUser.displayName);
  }

  ValidateRegisteredDoctor() {
    this.userDetailsService.GetUserDetails(this.registeredDoctorField.value.id)
    .subscribe(userDetails => {
      this.registeredDoctorDetails = userDetails;
      this.hasRegisteredDoctorDetails = true;
    },
      (err) => {
        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving User Details'
        });
      });
  }
}
