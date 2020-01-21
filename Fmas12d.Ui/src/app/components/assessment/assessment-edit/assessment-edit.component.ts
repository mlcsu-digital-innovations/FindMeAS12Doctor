import { ActivatedRoute, ParamMap } from '@angular/router';
import { AddressResult } from 'src/app/interfaces/address-result';
import { AmhpListService } from 'src/app/services/amhp-list/amhp-list.service';
import { Assessment } from 'src/app/interfaces/assessment';
import { AssessmentService } from 'src/app/services/assessment/assessment.service';
import { AssessmentUser } from 'src/app/interfaces/assessment-user';
import { Component, OnInit, Renderer2, ViewChild } from '@angular/core';
import { DatePickerFormat } from 'src/app/helpers/date-picker.validator';
import { environment } from 'src/environments/environment';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { map, switchMap, catchError, tap, distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { NgbDateStruct, NgbTimeStruct, NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of, throwError } from 'rxjs';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { Referral } from 'src/app/interfaces/referral';
import { REFERRAL_STATUS_ASSESSMENT_SCHEDULED, REFERRAL_STATUS_AWAITING_RESCHEDULING } from 'src/app/constants/Constants';
import { REFERRAL_STATUS_CLOSED } from 'src/app/constants/Constants';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { ReferralView } from 'src/app/interfaces/referral-view';
import { RouterService } from 'src/app/services/router/router.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { TypeAheadResult } from 'src/app/interfaces/typeahead-result';
import * as moment from 'moment';

@Component({
  selector: 'app-assessment-edit',
  templateUrl: './assessment-edit.component.html',
  styleUrls: ['./assessment-edit.component.css']
})
export class AssessmentEditComponent implements OnInit {

  addressList: string[] = [];
  allocatedDoctors: AssessmentUser[] = [];
  assessmentDetails: NameIdList[] = [];
  assessmentForm: FormGroup;
  assessmentId: number;
  assessmentScheduledDate: NgbDateStruct;
  assessmentScheduledTime: NgbTimeStruct;
  assessmentShouldBeCompletedByDate: NgbDateStruct;
  assessmentShouldBeCompletedByTime: NgbTimeStruct;
  cancelModal: NgbModalRef;
  defaultCompletionDate: NgbDateStruct;
  defaultCompletionTime: NgbTimeStruct;
  dropdownSettings: IDropdownSettings;
  genderSelected: number;
  genderTypes: NameIdList[];
  hasAmhpSearchFailed: boolean;
  isAllowedToRemove: boolean;
  isAmhpSearching: boolean;
  isPatientIdValidated: boolean;
  isPlannedAssessment: boolean;
  isSearchingForPostcode: boolean;
  minDate: NgbDateStruct;
  navigationPage: string;
  pageSize: number;
  referral$: Observable<Referral | any>;
  referralCreated: Date;
  referralId: number;
  removeModal: NgbModalRef;
  reselectModal: NgbModalRef;
  selectedDetails: NameIdList[] = [];
  selectedDoctors: AssessmentUser[] = [];
  specialities: NameIdList[];
  unwantedDoctorList: number[] = [];

  @ViewChild('cancelUpdate', null) cancelUpdateTemplate;
  @ViewChild('removeDoctors', null) removeDoctorTemplate;
  @ViewChild('selectDoctors', null) selectDoctorTemplate;

  constructor(
    private amhpListService: AmhpListService,
    private assessmentService: AssessmentService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private nameIdListService: NameIdListService,
    private postcodeValidationService: PostcodeValidationService,
    private referralService: ReferralService,
    private renderer: Renderer2,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService,
  ) { }

  ngOnInit() {

    this.dropdownSettings = {
      allowSearchFilter: false,
      enableCheckAll: false,
      idField: 'id',
      itemsShowLimit: 3,
      singleSelection: false,
      textField: 'name',
    };

    this.referral$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.referralService.getReferralView(+params.get('referralId'))
            .pipe(
              map(referral => {
                this.assessmentId = referral.currentAssessment.id;
                this.InitialiseForm(referral);
                return referral;
              })
            );
        }
      ),
      catchError((err) => {
        console.log(err);
        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving Referral Information'
        });

        const emptyReferral = {} as Referral;

        return of(emptyReferral);
      })
    );

    this.assessmentForm = this.formBuilder.group({
      amhp: [
        ''
      ],
      currentAssessment: [
        ''
      ],
      doctorNamesAccepted: [
        ''
      ],
      doctorNamesAllocated: [
        ''
      ],
      assessmentDetails: [
        ''
      ],
      assessmentAddress: [
        {
          value: '',
          disabled: true
        }
      ],
      meetingArrangementComment: [
        ''
      ],
      mustBeCompletedBy: [
        ''
      ],
      postCode: [
        ''
      ],
      preferredGender: [
      ],
      speciality: [
      ],
      scheduledDate: [
        this.assessmentScheduledDate,
        [
          DatePickerFormat
        ]
      ],
      scheduledTime: [this.assessmentScheduledTime],
      toBeCompletedByDate: [
        this.assessmentShouldBeCompletedByDate,
        [
          DatePickerFormat
        ]
      ],
      toBeCompletedByTime: [this.assessmentShouldBeCompletedByTime]
    });

    this.navigationPage = '/referral';
  }

  AddressSearch(): void {
    this.addressList = [];
    this.assessmentAddressField.setValue('');
    this.isSearchingForPostcode = true;
    this.assessmentAddressField.enable();

    this.FormatPostcode();

    this.postcodeValidationService.searchPostcode(this.assessmentPostcode.value)
      .subscribe(result => {
        this.addressList = result.addresses;
      }, (err) => {
        this.isSearchingForPostcode = false;
        this.toastService.displayError({
          title: 'Search Error',
          message: 'Error Retrieving Address Information'
        });
      }, () => {
        this.isSearchingForPostcode = false;
        // show an error if no matching addresses are returned
        if (this.addressList.length === 0) {
          this.assessmentPostcode.setErrors({ NoResultsReturned: true });
        }
      });
  }

  AmhpSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isAmhpSearching = true)),
      switchMap(term =>
        this.amhpListService.GetAmhpList(term).pipe(
          tap(() => (this.hasAmhpSearchFailed = false)),
          tap((results: any[]) => (this.ValidateTypeAheadResults(results, 'amhp'))),
          catchError(() => {
            this.hasAmhpSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.isAmhpSearching = false))
    )

  CancelEdit() {
    if (this.assessmentForm.dirty) {
      this.cancelModal = this.modalService.open(this.cancelUpdateTemplate, {
        size: 'lg'
      });
    } else {
      this.routerService.navigate(['/referral']);
    }
  }

  CheckAssessmentValidity() {
    if (!this.ValidateAssessment()) {
      return;
    }

    this.ConfirmUnwantedDoctors();
  }

  ClearField(fieldName: string) {
    if (this.assessmentForm.contains(fieldName)) {
      this.assessmentForm.controls[fieldName].setValue('');
      this.SetFieldFocus(`#${fieldName}`);
      this.assessmentForm.markAsDirty();
    }
  }

  ClearSelect(fieldName: string) {
    if (this.assessmentForm.contains(fieldName)) {
      this.assessmentForm.controls[fieldName].setValue([]);
      // this.assessmentForm.controls[fieldName].updateValueAndValidity();

      this.genderSelected = 0;
    }
  }

  ConfirmUnwantedDoctors() {

    this.unwantedDoctorList = [];

    this.selectedDoctors.forEach(doctor => {
      if (!doctor.selected) {
        this.unwantedDoctorList.push(doctor.doctorId);
      }
    });

    this.allocatedDoctors.forEach(doctor => {
      if (!doctor.selected) {
        this.unwantedDoctorList.push(doctor.doctorId);
      }
    });

    if (this.unwantedDoctorList.length > 0) {
      this.removeModal = this.modalService.open(this.removeDoctorTemplate, {
        size: 'lg'
      });
    } else {
      this.UpdateReferral();
    }

  }

  ConvertToDateStruct(dateValue: Date): NgbDateStruct {

    const momentDate = moment(dateValue);
    const dateStruct = {} as NgbDateStruct;
    dateStruct.day = momentDate.date();
    dateStruct.month = momentDate.month() + 1;
    dateStruct.year = momentDate.year();

    return dateStruct;
  }

  ConvertToTimeStruct(dateValue: Date): NgbTimeStruct {

    // round up to the next 5 minute interval
    const start = moment(dateValue);
    const remainder = 5 - (start.minute() % 5);
    const adjustment = remainder === 5 ? 0 : remainder;
    const momentDate = moment(start).add(adjustment, 'minutes');
    const timeStruct = {} as NgbTimeStruct;
    timeStruct.hour = momentDate.hour();
    timeStruct.minute = momentDate.minutes();
    timeStruct.second = momentDate.seconds();

    return timeStruct;
  }

  CreateDateFromPickerObjects(datePart: NgbDateStruct, timePart: NgbTimeStruct): Date {
    return new Date(
      datePart.year,
      datePart.month - 1,
      datePart.day,
      timePart.hour,
      timePart.minute,
      timePart.second,
      0
    );
  }

  CreateJsonDateFromPickerObjects(datePart: NgbDateStruct, timePart: NgbTimeStruct): string {
    return `${datePart.year}-${datePart.month}-${datePart.day}T${timePart.hour}`;
  }

  async Delay(milliseconds: number) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
  }

  DisableIfParentIsDisabled(fieldName: string): boolean {
    return this.assessmentForm.controls[fieldName].disabled;
  }

  FetchDropDownData() {
    // get the list of specialities for the dropdown
    this.nameIdListService.GetListData('speciality')
      .subscribe(specialities => {
        this.specialities = specialities;
      },
        (err) => {
          this.toastService.displayError({
            title: 'Error',
            message: 'Error Retrieving Speciality Data'
          });
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

    // get the list of risks for the dropdown
    this.nameIdListService.GetListData('assessmentdetailtype')
      .subscribe(details => {
        this.assessmentDetails = details;
      },
        (err) => {
          this.toastService.displayError({
            title: 'Error',
            message: 'Error Retrieving Assessment Risks'
          });
        });
  }

  FormatAddress(): string[] {

    const addressLines: string[] = [];
    const addressSplitByCommas = this.GetFormValue('assessmentAddress').split(',');

    // can only store 4 lines of the address
    for (let i = 0; i < 4; i++) {
      if (addressSplitByCommas.length - 1 >= i &&
        addressSplitByCommas[i] !== undefined &&
        addressSplitByCommas[i].trim() !== this.assessmentPostcode.value) {
        addressLines.push(addressSplitByCommas[i].trim());
      } else {
        addressLines.push(null);
      }
    }

    return addressLines;
  }

  FormatPostcode() {
    let postcode = this.assessmentPostcode.value.trim();
    if (postcode.indexOf(' ') === -1 && postcode.length > 3) {
      const inwardCode = postcode.substr(postcode.length - 3, 3);
      const outwardCode = postcode.substr(0, postcode.length - 3);
      postcode = `${outwardCode} ${inwardCode}`;
    }
    this.assessmentPostcode.setValue(postcode);
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  GetFormValue(fieldName: string) {
    return this.assessmentForm.get(fieldName).value;
  }

  get amhpField() {
    return this.assessmentForm.controls.amhp;
  }

  get assessmentAddressField() {
    return this.assessmentForm.controls.assessmentAddress;
  }

  get assessmentPostcode() {
    return this.assessmentForm.controls.postCode;
  }

  get preferredGenderField() {
    return this.assessmentForm.controls.preferredGender;
  }

  get scheduledDateField() {
    return this.assessmentForm.controls.scheduledDate;
  }

  get scheduledTimeField() {
    return this.assessmentForm.controls.scheduledTime;
  }

  get specialityField() {
    return this.assessmentForm.controls.speciality;
  }

  get toBeCompletedByDateField() {
    return this.assessmentForm.controls.toBeCompletedByDate;
  }

  get toBeCompletedByTimeField() {
    return this.assessmentForm.controls.toBeCompletedByTime;
  }

  InitialiseForm(referral: ReferralView) {

    this.minDate = this.ConvertToDateStruct(referral.createdAt);
    this.FetchDropDownData();

    const assessment = referral.currentAssessment;
    const amhpUser = assessment.amhpUser;

    // AMHP User - mandatory field
    const AmhpUser: TypeAheadResult = { id: amhpUser.id, resultText: amhpUser.displayName };
    this.assessmentForm.controls.amhp.setValue(AmhpUser);

    this.assessmentForm.controls.meetingArrangementComment.setValue(referral.currentAssessment.meetingArrangementComment);
    this.assessmentForm.controls.assessmentAddress.setValue(referral.currentAssessment.fullAddress);
    this.assessmentForm.controls.postCode.setValue(referral.currentAssessment.postcode);

    this.minDate = this.ConvertToDateStruct(referral.currentAssessment.mustBeCompletedBy);
    this.SetDefaultDateTimeFields(referral.currentAssessment.mustBeCompletedBy);

    this.selectedDoctors = referral.currentAssessment.doctorsSelected;
    this.allocatedDoctors = referral.currentAssessment.doctorsAllocated;

    this.preferredGenderField.setValue(referral.currentAssessment.preferredDoctorGenderType.id);
    this.specialityField.setValue(referral.currentAssessment.speciality.id);

    this.assessmentScheduledDate = this.ConvertToDateStruct(assessment.scheduledTime);
    this.scheduledDateField.setValue(this.assessmentScheduledDate);

    this.assessmentScheduledTime = this.ConvertToTimeStruct(assessment.scheduledTime);
    this.scheduledTimeField.setValue(this.assessmentScheduledTime);

    this.isPlannedAssessment = referral.currentAssessment.isPlanned;

    referral.currentAssessment.detailTypes.forEach(detailType => {
      const detail = { id: detailType.id, name: detailType.name } as NameIdList;
      this.selectedDetails.push(detail);
    });

    this.assessmentForm.controls.assessmentDetails.setValue(this.selectedDetails);

    this.isAllowedToRemove =
      referral.referralStatusId !== REFERRAL_STATUS_ASSESSMENT_SCHEDULED &&
      referral.referralStatusId !== REFERRAL_STATUS_CLOSED;

    // initialise all as selected
    this.selectedDoctors.forEach(doctor => doctor.selected = true);
    this.allocatedDoctors.forEach(doctor => doctor.selected = true);
  }

  OnCancelModalAction(action: boolean) {
    this.cancelModal.close();
    if (action) {
      this.routerService.navigate(['/referral']);
    }
  }

  OnCancelReselectAction(action: boolean) {
    this.reselectModal.close();
    if (action) {
      this.navigationPage = `/assessment/${this.assessmentId}/select-doctors`;
      this.UpdateReferral();
    } else {
      this.routerService.navigate([`/assessment/${this.assessmentId}/select-doctors`]);
    }
  }

  OnItemDeselect(item: any) {
    this.selectedDetails =
      this.selectedDetails.filter(obj => obj.id !== item.id);
  }

  OnItemSelect(item: NameIdList) {
    this.selectedDetails.push(item);
  }

  OpenLocationTab(): void {
    window.open(environment.locationEndpoint, '_blank');
  }

  OnRemoveDoctorsAction(action: boolean) {
    this.removeModal.close();
    if (action) {
      this.RemoveUnwantedDoctors();
    }
  }

  RemoveUnwantedDoctors() {
    this.assessmentService.removeDoctors(this.assessmentId, this.unwantedDoctorList).subscribe(
      result => {
        this.toastService.displaySuccess({
          message: 'Doctors Removed'
        });
        this.UpdateReferral();
      },
      error => {
        console.log(error);
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to remove doctors from assessment! Please try again in a few moments'
        });
        return throwError(error);
      }
    );

  }

  ReselectDoctors() {
    if (this.assessmentForm.dirty) {
      this.reselectModal = this.modalService.open(this.selectDoctorTemplate, {
        size: 'lg'
      });
    } else {
      this.routerService.navigate([`/assessment/${this.assessmentId}/select-doctors`]);
    }
  }

  SetDefaultDateTimeFields(defaultDatetIme: Date) {
    this.toBeCompletedByDateField.setValue(this.ConvertToDateStruct(defaultDatetIme));
    this.toBeCompletedByTimeField.setValue(this.ConvertToTimeStruct(defaultDatetIme));
    this.defaultCompletionDate = this.ConvertToDateStruct(defaultDatetIme);
    this.defaultCompletionTime = this.ConvertToTimeStruct(defaultDatetIme);
  }

  async SetFieldFocus(fieldName: string) {
    // ToDo: Find a better way to do this !
    await this.Delay(100);
    this.renderer.selectRootElement(fieldName).focus();
  }

  ToggleAllocatedUpdate(id: number) {
    const doctor = this.allocatedDoctors.find(sd => sd.id === id);
    doctor.selected = !doctor.selected;
  }

  ToggleAllocatedDoctor(index: number) {
    this.allocatedDoctors[index].selected = !this.allocatedDoctors[index].selected;
  }

  ToggleSelectedDoctor(index: number) {
    this.selectedDoctors[index].selected = !this.selectedDoctors[index].selected;
  }

  ToggleSelectedUpdate(id: number) {
    const doctor = this.selectedDoctors.find(sd => sd.id === id);
    doctor.selected = !doctor.selected;
  }

  UpdateReferral() {

    const updatedAssessment = {} as Assessment;

    updatedAssessment.id = this.assessmentId;
    updatedAssessment.postcode = this.GetFormValue('postCode');
    updatedAssessment.amhpUserId = this.GetFormValue('amhp').id;

    const specialityId = this.GetFormValue('speciality');
    updatedAssessment.specialityId = specialityId === 0 ? null : specialityId;

    const genderId = this.GetFormValue('preferredGender');
    updatedAssessment.preferredDoctorGenderTypeId = genderId === 0 ? null : genderId;

    const details: number[] = [];
    this.selectedDetails.forEach(detail => {
      details.push(detail.id);
    });
    updatedAssessment.detailTypeIds = details;

    updatedAssessment.meetingArrangementComment = this.GetFormValue('meetingArrangementComment');

    updatedAssessment.isPlanned = this.isPlannedAssessment;

    if (this.isPlannedAssessment) {
      const scheduledDate = this.GetFormValue('scheduledDate');
      const scheduledTime = this.GetFormValue('scheduledTime');
      updatedAssessment.scheduledTime = this.CreateDateFromPickerObjects(scheduledDate, scheduledTime);
    } else {
      const completedDate = this.GetFormValue('toBeCompletedByDate');
      const completedTime = this.GetFormValue('toBeCompletedByTime');
      updatedAssessment.mustBeCompletedBy = this.CreateDateFromPickerObjects(completedDate, completedTime);
    }

    const addressLines = this.FormatAddress();
    updatedAssessment.address1 = addressLines[0];
    updatedAssessment.address2 = addressLines[1];
    updatedAssessment.address3 = addressLines[2];
    updatedAssessment.address4 = addressLines[3];

    this.assessmentService.updateAssessment(updatedAssessment).subscribe(
      (result: Assessment) => {
        this.toastService.displaySuccess({
          message: 'Assessment Updated'
        });
        this.assessmentId = result.id;
        this.routerService.navigateByUrl(this.navigationPage);
      },
      error => {
        console.log(error);
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to update assessment! Please try again in a few moments'
        });
        return throwError(error);
      }
    );

  }

  ValidateAssessment(): boolean {

    let formIsValid = true;

    if (this.amhpField.value === null || this.amhpField.value === '') {
      formIsValid = false;
      this.amhpField.setErrors({ MissingAmhpUser: true });
    }

    if (this.isPlannedAssessment && this.scheduledDateField.value === null) {
      formIsValid = false;
      this.scheduledDateField.setErrors({ MissingDate: true });
    }

    if (this.isPlannedAssessment && this.scheduledTimeField.value === null) {
      formIsValid = false;
      this.scheduledDateField.setErrors({ MissingTime: true });
    }

    if (!this.isPlannedAssessment && this.toBeCompletedByDateField.value === null) {
      formIsValid = false;
      this.toBeCompletedByDateField.setErrors({ MissingDate: true });
    }

    if (!this.isPlannedAssessment && this.toBeCompletedByTimeField.value === null) {
      formIsValid = false;
      this.toBeCompletedByDateField.setErrors({ MissingTime: true });
    }

    if (this.assessmentPostcode.value === null || this.assessmentPostcode.value === '') {
      formIsValid = false;
      this.assessmentPostcode.setErrors({ MissingPostcode: true });
    }

    if (this.assessmentAddressField.value === null || this.assessmentAddressField.value === '') {
      formIsValid = false;
      this.assessmentPostcode.setErrors({ MissingAddress: true });
    }

    if (!formIsValid) {
      this.toastService.displayWarning({
        message: 'Please correct invalid fields'
      });
    }

    return formIsValid;
  }

  ValidateTypeAheadResults(results: any[], fieldName: string) {

    if (results == null) {
      this.assessmentForm.controls[fieldName].setErrors({ NoMatchingResults: true });
    }
  }
}
