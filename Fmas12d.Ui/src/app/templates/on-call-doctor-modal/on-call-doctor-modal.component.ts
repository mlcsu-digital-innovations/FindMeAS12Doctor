import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ContactDetail } from 'src/app/interfaces/contact-detail';
import { ContactDetailType } from 'src/app/interfaces/contact-detail-type';
import { ContactDetailTypeService }
  from 'src/app/services/contact-detail-type/contact-detail-type.service';
import { DatePickerFormat } from 'src/app/helpers/date-picker.validator';
import { DoctorListService } from 'src/app/services/doctor-list/doctor-list.service';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { OnCallDoctor } from 'src/app/interfaces/on-call-doctor';
import { OnCallDoctorList } from 'src/app/interfaces/on-call-doctor-list';
import { PostcodeRegex } from 'src/app/constants/Constants';
import { PostcodeValidationService }
  from 'src/app/services/postcode-validation/postcode-validation.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UserDetails } from 'src/app/interfaces/user-details';
import { UserDetailsService } from 'src/app/services/user/user-details.service';
import { debounceTime, distinctUntilChanged, tap, switchMap, catchError } from 'rxjs/operators';
import * as moment from 'moment';

@Component({
  selector: 'app-on-call-doctor-modal',
  templateUrl: './on-call-doctor-modal.component.html',
  styleUrls: ['./on-call-doctor-modal.component.css']
})
export class OnCallDoctorModalComponent implements OnInit {
  @Output() actioned = new EventEmitter<any>();
  @Input() public onCallDoctor: OnCallDoctorList;

  contactDetails: ContactDetail[];
  doctorGmcNumber: number;
  doctorId: number;
  doctorName: string;
  doctorIsValid?: boolean;
  endDate: NgbDateStruct;
  endTime: NgbTimeStruct;
  isRegisteredDoctorSearching: boolean;
  isSearchingForPostcode: boolean;
  hasDoctorSearchFailed: boolean;
  onCallDoctorExists: boolean;
  onCallDoctorForm: FormGroup;
  startDate: NgbDateStruct;
  startTime: NgbTimeStruct;

  constructor(
    private contactDetailTypeService: ContactDetailTypeService,
    private doctorListService: DoctorListService,
    private formBuilder: FormBuilder,
    private postcodeValidationService: PostcodeValidationService,
    private toastService: ToastService,
    private userDetailsService: UserDetailsService,
  ) { }

  ngOnInit() {
    this.onCallDoctorExists = false;
    this.contactDetails = [];
    this.onCallDoctorForm = this.formBuilder.group({
      endDate: [
        this.endDate,
        [
          DatePickerFormat
        ]
      ],
      endTime: [this.endTime],
      doctorSearch: [''],
      startDate: [
        this.startDate,
        [
          DatePickerFormat
        ]
      ],
      startTime: [this.startTime],
      contactDetail: null,
      locationPostcode: [
        '',
        [
          Validators.minLength(6),
          Validators.maxLength(8),
          Validators.pattern(`${PostcodeRegex}$`)
        ]
      ],
    });

    if (this.onCallDoctor) {
      this.onCallDoctorExists = true;
      this.InitialiseForm();
    }
    else {
      let currentDateTime: Date = new Date();
      this.startDateField.setValue(this.ConvertToDateStruct(currentDateTime));
      this.startTimeField.setValue(this.ConvertToTimeStruct(currentDateTime));
      this.endDateField.setValue(this.ConvertToDateStruct(currentDateTime));
      this.endTimeField.setValue(this.ConvertToTimeStruct(currentDateTime));
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

    const momentDate = moment(dateValue);
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

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  getContactDetails(userId: number) {
    this.contactDetailTypeService.GetContactDetailTypes(userId)
      .subscribe(
        (result: ContactDetailType[]) => {
          if (result !== null) {
            this.contactDetails = [];
            this.contactDetails
              .push({ id: null, name: "Please Select Location", telephoneNumber: null });
            result.forEach(contact => {
              this.contactDetails
                .push({ id: contact.contactDetails[0].id, name: contact.name, telephoneNumber: null });
            });
            this.contactDetails.push({ id: 0, name: "Other", telephoneNumber: null });
          }
        }, error => {
          this.toastService.displayError({
            title: 'Server Error',
            message: 'Unable to retrieve contact detail types'
          });
        }
      );
  }

  get doctorSearchField() {
    return this.onCallDoctorForm.controls.doctorSearch;
  }

  get locationField() {
    return this.onCallDoctorForm.controls.contactDetail;
  }

  get locationPostcodeField() {
    return this.onCallDoctorForm.controls.locationPostcode;
  }

  get startDateField() {
    return this.onCallDoctorForm.controls.startDate;
  }

  get startTimeField() {
    return this.onCallDoctorForm.controls.startTime;
  }

  get endDateField() {
    return this.onCallDoctorForm.controls.endDate;
  }

  get endTimeField() {
    return this.onCallDoctorForm.controls.endTime;
  }

  FormatPostcode() {
    let postcode = this.locationPostcodeField.value.trim();
    if (postcode.indexOf(' ') === -1 && postcode.length > 3) {
      const inwardCode = postcode.substr(postcode.length - 3, 3);
      const outwardCode = postcode.substr(0, postcode.length - 3);
      postcode = `${outwardCode} ${inwardCode}`;
    }
    this.locationPostcodeField.setValue(postcode);
  }

  InitialiseForm() {
    this.doctorName = this.onCallDoctor.userName;
    this.doctorGmcNumber = this.onCallDoctor.gmcNumber;
    this.doctorId = this.onCallDoctor.userId;
    this.doctorSearchField.setValue(`${this.doctorName} - ${this.doctorGmcNumber}`);
    this.doctorIsValid = null;
    this.startDateField.setValue(this.ConvertToDateStruct(this.onCallDoctor.start));
    this.startTimeField.setValue(this.ConvertToTimeStruct(this.onCallDoctor.start));
    this.endDateField.setValue(this.ConvertToDateStruct(this.onCallDoctor.end));
    this.endTimeField.setValue(this.ConvertToTimeStruct(this.onCallDoctor.end));

    this.getContactDetails(this.onCallDoctor.userId);
    if (this.onCallDoctor.location.contactDetailId) {
      this.locationField.setValue(this.onCallDoctor.location.contactDetailId);
    }
    else {
      this.locationField.setValue(0);
      this.locationPostcodeField.setValue(this.onCallDoctor.location.postcode);
    }
  }

  IsSearchingForPostcode(): boolean {
    return this.isSearchingForPostcode;
  }

  IsEndDateBeforeStartDate(endDateField: AbstractControl, endTimeField: AbstractControl,
    startDateField: AbstractControl, startTimeField: AbstractControl
  ): boolean {

    const endDate = this.CreateDateFromPickerObjects(endDateField.value, endTimeField.value);
    const startDate = this.CreateDateFromPickerObjects(startDateField.value, startDateField.value);

    const endDateIsBeforeStartDate =
      moment(endDate).isBefore(moment(startDate));

    if (endDateIsBeforeStartDate) {
      endDateField.setErrors({ InvalidEndDate: true });
    } else {
      endDateField.setErrors({ InvalidEndDate: false });
    }

    return endDateIsBeforeStartDate;
  }

  HasDoctorBeenSelected() {
    return (typeof this.doctorSearchField.value) === 'object';
  }

  HasInvalidPostcode(): boolean {
    return (
      this.locationPostcodeField.value === '' ||
      this.locationPostcodeField.value === null ||
      this.locationPostcodeField.errors !== null
    );
  }

  HasValidPostcode(): boolean {
    return (
      this.locationPostcodeField.value !== '' &&
      this.locationPostcodeField.value !== null &&
      this.locationPostcodeField.errors === null
    );
  }

  RegisteredDoctorSearch = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.isRegisteredDoctorSearching = true)),
      switchMap(term =>
        this.doctorListService.GetDoctorList(term, false).pipe(
          tap(() => (this.hasDoctorSearchFailed = false)),
          catchError(() => {
            this.hasDoctorSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.isRegisteredDoctorSearching = false))
    )

  SaveOnCallDoctor() {
    let canContinue: boolean = true;

    // check doctor 
    if (!this.doctorName || !this.doctorGmcNumber) {
      canContinue = false;
      this.doctorSearchField.setErrors({ InvalidDoctor: true });
    }

    // check start date
    if (!this.startDateField.value) {
      canContinue = false;
      this.startDateField.setErrors({ DatePickerFormat: true });
    }

    // check end date
    if (!this.endDateField.value) {
      canContinue = false;
      this.endDateField.setErrors({ DatePickerFormat: true });
    }

    // check end date is after start date
    if (this.startDateField.value && this.startTimeField &&
      this.endDateField.value && this.endTimeField) {
      if (this.IsEndDateBeforeStartDate(
        this.endDateField,
        this.endTimeField,
        this.startDateField,
        this.startTimeField
      )) {
        canContinue = false;
        this.endDateField.setErrors({ InvalidEndDate: true });
      }
    }

    // check location
    if (this.locationField.value === null || this.locationField.value === undefined) {
      canContinue = false;
      this.locationField.setErrors({ NoLocationSelected: true });
    }

    // check postcode if location is Other
    if (this.locationField.value == 0 && !this.HasValidPostcode()) {
      canContinue = false;
      this.locationPostcodeField.setErrors({ InvalidPostcode: true });
    }

    if (canContinue) {
      // create onCallDoctorList object
      const onCallDoctor = {} as OnCallDoctor;

      if (this.onCallDoctor) {
        onCallDoctor.id = this.onCallDoctor.id;
      }

      onCallDoctor.userId = this.doctorId;
      onCallDoctor.start =
        this.CreateDateFromPickerObjects(this.startDateField.value, this.startTimeField.value);
      onCallDoctor.end =
        this.CreateDateFromPickerObjects(this.endDateField.value, this.endTimeField.value);

      let contactDetailId: number = this.locationField.value;

      if (contactDetailId > 0) {

        onCallDoctor.contactDetailId = contactDetailId;
      }
      else {
        onCallDoctor.postcode = this.locationPostcodeField.value;
      }

      this.actioned.emit(onCallDoctor);
    }

  }

  ShowPostcodeField(): boolean {
    return this.onCallDoctorForm.controls.location.value > 0;
  }

  Cancel() {
    this.actioned.emit(null);
  }

  ValidatePostcode(): void {
    this.isSearchingForPostcode = true;
    this.FormatPostcode();

    this.postcodeValidationService.validatePostcode(this.locationPostcodeField.value)
      .subscribe(result => {
        this.isSearchingForPostcode = false;
        this.locationPostcodeField.setErrors(null);
        this.toastService.displaySuccess({
          message: 'Postcode is valid'
        });
      }, (err) => {
        this.isSearchingForPostcode = false;
        this.locationPostcodeField.setErrors({ InvalidPostcode: true });
        this.toastService.displayError({
          title: 'Search Error',
          message: 'Error Retrieving Address Information'
        });
      });
  }

  ValidateRegisteredDoctor() {
    this.userDetailsService.GetDoctorDetails(this.doctorSearchField.value.id)
      .subscribe((doctorDetails: UserDetails) => {

        if (doctorDetails === null) {
          this.toastService.displayError({
            title: 'Error',
            message: 'Unable to retrieve doctor details'
          });
          this.doctorIsValid = false;
        } else {
          this.doctorGmcNumber = doctorDetails.gmcNumber;
          this.doctorId = doctorDetails.id;
          this.doctorName = doctorDetails.displayName;
          this.doctorIsValid = true;
          this.locationField.setValue(null);
          this.getContactDetails(doctorDetails.id);
        }
      },
        (err) => {
          this.toastService.displayError({
            title: 'Error',
            message: 'Error Retrieving User Details'
          });
          this.doctorIsValid = false;
        });
  }
}
