import { ActivatedRoute, ParamMap } from '@angular/router';
import { AddressResult } from 'src/app/interfaces/address-result';
import { AssessmentService } from 'src/app/services/assessment/assessment.service';
import { Component, OnInit, ViewChild, Renderer2 } from '@angular/core';
import { debounceTime, tap, catchError, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { DoctorDetails } from 'src/app/interfaces/doctor-details';
import { DoctorListService } from 'src/app/services/doctor-list/doctor-list.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { PostcodeRegex, PROFILE_TYPE_UNREGISTERED, SECTION12_APPROVED } from 'src/app/constants/Constants';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { RouterService } from 'src/app/services/router/router.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UnregisteredUser } from 'src/app/interfaces/unregistered-user';
import { UserDetails } from 'src/app/interfaces/user-details';
import { UserDetailsService } from 'src/app/services/user/user-details.service';
import { DoctorSearchResult } from 'src/app/interfaces/doctor-search-result';

@Component({
  selector: 'app-doctor-add',
  templateUrl: './doctor-add.component.html',
  styleUrls: ['./doctor-add.component.css']
})
export class DoctorAddComponent implements OnInit {

  addressList: AddressResult[] = [];
  assessmentId: number;
  cancelModal: NgbModalRef;
  genderTypes: NameIdList[];
  existingUserMessage: string;
  existingUserModal: NgbModalRef;
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
  selectedDoctor: UserDetails;
  unregisteredDoctorForm: FormGroup;
  unregisteredUser: UnregisteredUser;
  unregisteredUserError: string;
  unregisteredUsers: UnregisteredUser[];

  @ViewChild('cancelAllocation', null) cancelAllocationTemplate;
  @ViewChild('unregisteredUserResults', { static: true }) unregisteredUserResults;
  @ViewChild('confirmExistingUser', null) confirmExistingUser;

  constructor(
    private assessmentService: AssessmentService,
    private doctorListService: DoctorListService,
    private formBuilder: FormBuilder,
    private nameIdListService: NameIdListService,
    private modalService: NgbModal,
    private postcodeValidationService: PostcodeValidationService,
    private renderer: Renderer2,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService,
    private userDetailsService: UserDetailsService
  ) { }

  ngOnInit() {

    this.route.paramMap.subscribe(
      (paramMap: ParamMap) => {
        this.assessmentId = +paramMap.get('assessmentId');
      });

    this.registeredDoctorForm = this.formBuilder.group({
      registeredName: ['']
    });

    this.unregisteredDoctorForm = this.formBuilder.group({
      unregisteredSearch: [''],
      unregisteredName: [''],
      unregisteredGmcNumber: ['',
        [
          Validators.minLength(7),
          Validators.maxLength(7),
          Validators.pattern('^\d{7}$')
        ]],
      contact: [''],
      gender: null,
      isSection12: [{ value: false, disabled: true }]
    });

    // get the list of genders for the dropdown
    this.nameIdListService.GetListData('gendertype')
      .subscribe(genders => {
        this.genderTypes = genders;
      },
        (err) => {
          this.toastService.displayError({
            title: 'Error',
            message: 'Error retrieving gender data'
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
        this.toastService.displayError({
          title: 'Search Error',
          message: 'Error retrieving address information'
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
    this.assessmentService
      .allocateDoctorDirectly(this.assessmentId, this.registeredDoctorDetails.id)
      .subscribe(userDetails => {
        this.toastService.displaySuccess({
          title: 'Success',
          message: 'Doctor allocated'
        });
        this.routerService.navigatePrevious();
      },
        (err) => {
          let msg = 'Error allocating doctor to assessment';

          if (err.error.errors && err.error.errors.UserId !== undefined) {
            msg = 'Doctor is already allocated to this assessment';
          }

          // specific error message for missing contact details
          if (String(err.error).includes('ContactDetailBaseMissingForUserException')) {
            msg = 'Unable to find contact details for doctor. Doctor cannot be allocated.';
          }

          this.toastService.displayError({
            title: 'Error',
            message: msg
          });
        });
  }

  AllocateUnregisteredDoctor() {

    if (this.selectedDoctor && !this.selectedDoctor.fromS12LiveRegister) {
      // have an existing unregistered user
      this.assessmentService
        .allocateDoctorDirectly(this.assessmentId, this.selectedDoctor.id)
        .subscribe(userDetails => {
          this.toastService.displaySuccess({
            title: 'Success',
            message: 'Doctor allocated'
          });
          this.routerService.navigatePrevious();
        },
          (err) => {
            console.log(err);
            const msg =
              err.error.errors !== undefined &&
              err.error.errors.UserId !== undefined
                ? 'Doctor is already allocated to this assessment'
                : 'Unable to save doctor details';

            this.toastService.displayError({
              title: 'Error',
              message: msg
            });
          });
    } else {
      // create a new user and send it to the api
      const newUser = {} as UserDetails;
      newUser.displayName = this.unregisteredDoctorField.value;
      newUser.gmcNumber = +this.unregisteredGmcNumberField.value;
      newUser.genderTypeId =
        +this.unregisteredGenderField.value === 0 ? null : +this.unregisteredGenderField.value;
      newUser.contactDetailBase = { telephoneNumber: this.unregisteredContactField.value };

      let canCreateNewUser = true;

      if (newUser.contactDetailBase.telephoneNumber === ''
          || newUser.contactDetailBase.telephoneNumber === null) {
        this.unregisteredUserError = '* Telephone Number must be supplied';
        canCreateNewUser = false;
      }

      if (newUser.gmcNumber.toString().length !== 7) {
        this.unregisteredUserError = '* GMC Number format incorrect';
        canCreateNewUser = false;
      }

      if (newUser.displayName === '') {
        this.unregisteredUserError = '* Doctor Name must be supplied';
        canCreateNewUser = false;
      }

      if (canCreateNewUser) {

        // check that the GMC number is unique
        this.doctorListService.GetDoctorList(newUser.gmcNumber.toString(), true)
          .subscribe((userList: DoctorSearchResult[]) => {
            if (userList === null) {
              userList = [];
            }

            // Filter out the results from the S12 Live Register.
            userList = userList.filter(user => user.fromSection12LiveRegister === false);

            if (userList.length > 0) {
              this.toastService.displayError({
                title: 'Error',
                message: 'GMC Number used by existing user'
              });
            } else {
              this.assessmentService
                .allocateNewUnregisteredDoctor(this.assessmentId, newUser)
                .subscribe(
                  userDetails => {
                    this.toastService.displaySuccess({
                      title: 'Success',
                      message: 'Doctor allocated'
                    });
                    this.routerService.navigatePrevious();
                  },
                  err => {
                    const msg =
                      err.error.errors.UserId !== undefined
                        ? 'Doctor is already allocated to this assessment'
                        : err.error.title;

                    this.toastService.displayError({
                      title: 'Error',
                      message: msg
                    });
                  }
                );
            }
          },
            err => {
              this.toastService.displayError({
                title: 'Error',
                message: 'Error checking GMC number'
              });
            }
          );
      }
    }
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

  ClearUnregisteredDoctor() {
    this.unregisteredDoctorForm.reset();
    this.unregisteredDoctorField.enable();
    this.unregisteredGmcNumberField.enable();
    this.unregisteredDoctorField.setValue('');
    this.unregisteredGmcNumberField.setValue('');

    this.Delay(1000);
    this.SetFieldFocus('#unregisteredName');
  }

  async Delay(milliseconds: number) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
  }

  DisableFieldsForUnregisteredUser() {
    this.unregisteredContactField.disable();
    this.unregisteredGenderField.disable();
  }

  DisableIfParentIsDisabled(fieldName: string): boolean {
    return this.unregisteredDoctorForm.controls[fieldName].disabled;
  }

  ProcessDoctorDetails(doctorDetails: UserDetails, isS12: boolean) {

    if (doctorDetails === null) {
      this.toastService.displayError({
        title: 'Error',
        message: 'Unable to retrieve doctor details'
      });
    } else {

      // if the doctor is registered then inform the user
      if (doctorDetails.profileTypeId !== PROFILE_TYPE_UNREGISTERED) {
        this.registeredDoctorDetails = doctorDetails;
        this.isUnregisteredSearchComplete = false;
        this.existingUserMessage =
          `Allocate existing doctor '${doctorDetails.displayName} GMC# ${doctorDetails.gmcNumber}'
       to this assessment ?`;

        this.existingUserModal = this.modalService.open(this.confirmExistingUser, {
          size: 'lg'
        });

      } else {
        this.selectedDoctor = doctorDetails;
        this.selectedDoctor.fromS12LiveRegister = isS12;
        this.unregisteredDoctorField.setValue(doctorDetails.displayName);
        this.unregisteredGmcNumberField.setValue(doctorDetails.gmcNumber);
        this.unregisteredGenderField.setValue(doctorDetails.genderTypeId);

        if (doctorDetails.section12ApprovalStatusId === SECTION12_APPROVED) {
          this.unregisteredSection12Field.setValue(true);
        }

        this.unregisteredContactField.setValue(doctorDetails.contactDetailBase.telephoneNumber);

        this.unregisteredDoctorField.disable();
        this.unregisteredGmcNumberField.disable();
        this.hasUnregisteredUser = true;

        const existingDoctorMessage = isS12
          ? 'Details retrieved from S12 live register'
          : 'An existing unregistered doctor has been found';

        this.toastService.displayInfo({
          title: 'Information',
          message: existingDoctorMessage
        });
      }
    }
  }

  FetchS12DoctorDetails(userId: number) {

    this.userDetailsService.GetS12DoctorDetails(userId)
      .subscribe((doctorDetails: UserDetails) => {
          this.ProcessDoctorDetails(doctorDetails, true);
      },
        (err) => {
          this.toastService.displayError({
            title: 'Error',
            message: 'Error retrieving doctor details'
          });
        });
  }

  FetchDoctorDetails(userId: number) {

    this.userDetailsService.GetDoctorDetails(userId)
      .subscribe((doctorDetails: UserDetails) => {
          this.ProcessDoctorDetails(doctorDetails, false);
      },
        (err) => {
          this.toastService.displayError({
            title: 'Error',
            message: 'Error retrieving doctor details'
          });
        });
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

  get unregisteredSearchField() {
    return this.unregisteredDoctorForm.controls.unregisteredSearch;
  }

  get unregisteredUserField() {
    return this.unregisteredDoctorForm.controls.unregisteredName;
  }

  HasDoctorBeenSelected() {
    return (typeof this.registeredDoctorField.value) === 'object';
  }

  HasIncompleteUser(): boolean {

    return !(this.unregisteredDoctorField.value !== '' &&
      this.unregisteredGmcNumberField.value !== '');
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

  OnCancelExistingModalAction(action: boolean) {
    this.existingUserModal.close();
    if (action) {
      // add the already registered doctor to the assessment
      this.AllocateRegisteredDoctor();
    }
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

    if (user.fromSection12LiveRegister) {
      this.FetchS12DoctorDetails(user.id);
    } else {
      this.FetchDoctorDetails(user.id);
    }
    this.multipleUsersModal.close();
  }

  RegisteredDoctorSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isRegisteredDoctorSearching = true)),
      switchMap(term =>
        this.doctorListService.GetDoctorList(term, false).pipe(
          tap(() => (this.hasDoctorSearchFailed = false)),
          tap((results: any[]) => (this.ValidateTypeAheadResults(results, 'registeredName'))),
          catchError(() => {
            this.hasDoctorSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.isRegisteredDoctorSearching = false))
    )

  ResetAllFields() {
    this.unregisteredContactField.enable();
    this.unregisteredContactField.setValue('');
    this.unregisteredGenderField.enable();
    this.unregisteredGenderField.setValue('');
  }

  SearchUnregisteredDoctor() {

    if (this.unregisteredDoctorField.value === '' && this.unregisteredGmcNumberField.value === '') {
      return;
    }
    this.ResetAllFields();

    const searchTerm = this.unregisteredGmcNumberField.value === ''
      ? this.unregisteredDoctorField.value
      : this.unregisteredGmcNumberField.value;

    this.doctorListService.GetDoctorList(searchTerm, true)
      .subscribe((userList: DoctorSearchResult[]) => {
        this.isUnregisteredSearchComplete = true;

        if (userList === null) {
          userList = [];
        }

        this.unregisteredUser = {} as UnregisteredUser;

        switch (userList.length) {
          case 0:
            this.toastService.displayInfo({
              title: 'Information',
              message: 'No existing unregistered doctors found'
            });
            this.hasUnregisteredUser = false;

            // prevent fields from being changed after searching
            if (this.unregisteredDoctorField.value !== '') {
              this.unregisteredDoctorField.disable();
            }
            if (this.unregisteredGmcNumberField.value !== '') {
              this.unregisteredGmcNumberField.disable();
            }

            break;
          case 1:
            this.FetchS12DoctorDetails(userList[0].id);
            break;
          default:

            this.unregisteredUser = {} as UnregisteredUser;
            this.unregisteredUsers = [];

            userList.forEach(user => {
              const tempUser = {} as UnregisteredUser;
              tempUser.id = user.id;
              tempUser.fromSection12LiveRegister = user.fromSection12LiveRegister;
              const index = user.resultText.indexOf('-');

              if (index > 0) {
                tempUser.displayName = user.resultText.substr(0, index - 1);
                tempUser.gmcNumber = +user.resultText.substr(index + 2, user.resultText.length - index);
                this.unregisteredUsers.push(tempUser);
              }
            });

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
            message: 'Error retrieving doctor details'
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
    this.userDetailsService.GetDoctorDetails(this.registeredDoctorField.value.id)
      .subscribe((doctorDetails: UserDetails) => {

        if (doctorDetails === null) {
          this.toastService.displayError({
            title: 'Error',
            message: 'Unable to retrieve doctor details'
          });
        } else {
          this.registeredDoctorDetails = doctorDetails;
          this.hasRegisteredDoctorDetails = true;
        }
      },
        (err) => {
          this.toastService.displayError({
            title: 'Error',
            message: 'Error retrieving doctor details'
          });
        });
  }
  ValidateTypeAheadResults(results: any[], fieldName: string) {

    if (results == null) {
      this.registeredDoctorForm.controls[fieldName].setErrors({ NoMatchingResults: true });
    }
  }
}
