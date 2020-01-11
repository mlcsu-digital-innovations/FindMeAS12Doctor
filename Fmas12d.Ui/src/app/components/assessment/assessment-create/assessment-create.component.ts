import { ActivatedRoute, ParamMap } from '@angular/router';
import { AddressResult } from 'src/app/interfaces/address-result';
import { AmhpListService } from '../../../services/amhp-list/amhp-list.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePickerFormat } from 'src/app/helpers/date-picker.validator';
import { debounceTime, distinctUntilChanged, tap, switchMap, catchError, map } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { Assessment } from 'src/app/interfaces/assessment';
import { AssessmentService } from 'src/app/services/assessment/assessment.service';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { User } from 'src/app/interfaces/user';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { NgbDateStruct, NgbTimeStruct, NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of, empty, throwError } from 'rxjs';
import { Patient } from 'src/app/interfaces/patient';
import { PostcodeRegex } from '../../../constants/Constants';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from '../../../services/referral/referral.service';
import { RouterService } from 'src/app/services/router/router.service';
import { ToastService } from '../../../services/toast/toast.service';
import { TypeAheadResult } from 'src/app/interfaces/typeahead-result';
import * as moment from 'moment';

@Component({
  selector: 'app-assessment-create',
  templateUrl: './assessment-create.component.html',
  styleUrls: ['./assessment-create.component.css']
})
export class AssessmentCreateComponent implements OnInit {

  addresses$: Observable<any>;
  addressList: string[];
  cancelModal: NgbModalRef;
  defaultCompletionDate: NgbDateStruct;
  defaultCompletionTime: NgbTimeStruct;
  dropdownSettings: IDropdownSettings;
  assessmentDetails: NameIdList[] = [];
  assessmentForm: FormGroup;
  assessmentId: number;
  assessmentPostcodeValidationMessage: string;
  assessmentShouldBeCompletedByDate: NgbDateStruct;
  assessmentShouldBeCompletedByTime: NgbTimeStruct;
  genderTypes: NameIdList[];
  hasAmhpSearchFailed: boolean;
  isAmhpSearching: boolean;
  isCreatingAssessment: boolean;
  isFormInCreatingState: boolean = true;
  isSearchingForPostcode: boolean;
  minDate: NgbDateStruct;
  referral$: Observable<Referral | any>;
  referralCreated: Date;
  referralId: number;
  selectedDate: Date;
  selectedDetails: NameIdList[] = [];
  specialities: NameIdList[];

  @ViewChild('cancelAssessment', null) cancelAssessmentTemplate;

  constructor(
    private amhpListService: AmhpListService,
    private assessmentService: AssessmentService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private nameIdListService: NameIdListService,
    private postcodeValidationService: PostcodeValidationService,
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private routerService: RouterService,
    private toastService: ToastService
  ) { }

  ngOnInit() {

    this.addressList = [];

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
          return this.referralService.getReferralSummary(+params.get('referralId'))
            .pipe(
              map(referral => {
                this.SetAmhpField(referral.leadAmhpUser.id, referral.leadAmhpUser.displayName);

                this.referralCreated = referral.createdAt;
                this.referralId = +params.get('referralId');

                this.minDate = this.ConvertToDateStruct(referral.createdAt);
                this.SetDefaultDateTimeFields(referral.defaultToBeCompletedBy);

                return referral;
              })
            );
        }
      ),
      catchError((err) => {

        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving Referral Information'
        });

        const emptyReferral = {} as Referral;
        emptyReferral.patient = {} as Patient;
        emptyReferral.leadAmhpUser = {} as User;

        return of(emptyReferral);
      })
    );

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

    this.assessmentForm = this.formBuilder.group({
      plannedAssessment: false,
      amhp: [''],
      assessmentPostcode: [
        '',
        [
          Validators.minLength(6),
          Validators.maxLength(8),
          Validators.pattern(`${PostcodeRegex}$`)
        ]
      ],
      assessmentAddress: [''],
      additionalDetails: ['',
        [
          Validators.maxLength(2000)
        ]
      ],
      speciality: [''],
      preferredGender: [''],
      assessmentDetails: [''],
      scheduledDate: [''],
      scheduledTime: [''],
      toBeCompletedByDate: [
        this.assessmentShouldBeCompletedByDate,
        [
          DatePickerFormat
        ]
      ],
      toBeCompletedByTime: [this.assessmentShouldBeCompletedByTime]
    });
  }

  AddressSearch(): void {
    this.addressList = [];
    this.assessmentAddressField.setValue('');
    this.isSearchingForPostcode = true;

    this.FormatPostcode();

    this.postcodeValidationService.searchPostcode(this.assessmentPostcodeField.value)
      .subscribe(address => {
        this.addressList = address.addresses;
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
          this.assessmentPostcodeField.setErrors({ NoResultsReturned: true });
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
          catchError(() => {
            this.hasAmhpSearchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.isAmhpSearching = false))
    )

  CancelAssessment() {

    if (this.assessmentForm.dirty) {
      this.cancelModal = this.modalService.open(this.cancelAssessmentTemplate, {
        size: 'lg'
      });
    } else {
      this.DisplayCancelAssessmentCreationToast();
      this.routerService.navigate(['/referral']);
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
    const momentDate = moment(start).add(remainder, 'minutes');
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

  DisableForm() {
    this.amhpField.disable();
    this.assessmentPostcodeField.disable();
    this.assessmentAddressField.disable();
    this.additionalDetailsField.disable();
    this.specialityField.disable();
    this.preferredDoctorGenderField.disable();
    this.assessmentDetailsField.disable();
    this.plannedAssessmentField.disable();

    if (this.plannedAssessmentField.value === true) {
      this.scheduledDateField.disable();
      this.scheduledTimeField.disable();
    } else {
      this.toBeCompletedByDateField.disable();
      this.toBeCompletedByTimeField.disable();
    }
  }  

  EditAssessment() {
    this.routerService.navigate([`/assessment/edit/${this.assessmentId}`]);
  }

  FormatAddress(): string[] {

    const addressLines: string[] = [];
    const addressSplitByCommas = this.assessmentAddressField.value.split(',');

    // can only store 4 lines of the address
    for (let i = 0; i < 4; i++) {
      if (addressSplitByCommas.length - 1 >= i &&
          addressSplitByCommas[i].trim() !== this.assessmentPostcodeField.value) {
            addressLines.push(addressSplitByCommas[i].trim());
      } else {
        addressLines.push(null);
      }
    }

    return addressLines;
  }

  FormatPostcode() {
    let postcode = this.assessmentPostcodeField.value.trim();
    if (postcode.indexOf(' ') === -1 && postcode.length > 3) {
      const inwardCode = postcode.substr(postcode.length - 3, 3);
      const outwardCode = postcode.substr(0, postcode.length - 3);
      postcode = `${outwardCode} ${inwardCode}`;
    }
    this.assessmentPostcodeField.setValue(postcode);
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  get additionalDetailsField() {
    return this.assessmentForm.controls.additionalDetails;
  }

  get amhpField() {
    return this.assessmentForm.controls.amhp;
  }

  get amhpUser(): TypeAheadResult {
    return this.assessmentForm.controls.amhp.value;
  }

  get assessment() {
    return this.assessmentForm.controls;
  }

  get assessmentAddressField() {
    return this.assessmentForm.controls.assessmentAddress;
  }

  get assessmentDetailsField() {
    return this.assessmentForm.controls.assessmentDetails;
  }

  get assessmentPostcodeField() {
    return this.assessmentForm.controls.assessmentPostcode;
  }

  get plannedAssessmentField() {
    return this.assessmentForm.controls.plannedAssessment;
  }

  get preferredDoctorGenderField() {
    return this.assessmentForm.controls.preferredGender;
  }

  get toBeCompletedByDateField() {
    return this.assessmentForm.controls.toBeCompletedByDate;
  }

  get toBeCompletedByTimeField() {
    return this.assessmentForm.controls.toBeCompletedByTime;
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

  HasValidAddress(): boolean {
    return this.assessmentAddressField.value !== '';
  }

  HasValidAmhp(): boolean {
    return this.amhpUser.id !== undefined;
  }

  HasValidPostcode(): boolean {
    return (
      this.assessmentPostcodeField.value !== '' &&
      this.assessmentPostcodeField.errors == null
    );
  }

  IsAssessmentBeforeReferralCreationDate(dateField: AbstractControl, timeField: AbstractControl ): boolean {

    const compareDate = this.CreateDateFromPickerObjects(dateField.value, timeField.value);

    const assessmentDateIsBeforeReferralCreatedAt =
      moment(compareDate).isBefore(moment(this.referralCreated));

    if (assessmentDateIsBeforeReferralCreatedAt) {
      dateField.setErrors({InvalidAssessmentDate: true});
    } else {
      dateField.setErrors({InvalidAssessmentDate: false});
    }

    return assessmentDateIsBeforeReferralCreatedAt;
  }

  IsFormInCreatingState(): boolean {
    return this.isFormInCreatingState;
  }

  HasInvalidPostcode(): boolean {
    return (
      this.assessmentPostcodeField.value !== '' &&
      this.assessmentPostcodeField.errors !== null
    );
  }

  IsSearchingForPostcode(): boolean {
    return this.isSearchingForPostcode;
  }

  OnCancelModalAction(action: boolean) {

    this.cancelModal.close();

    if (action) {
      this.DisplayCancelAssessmentCreationToast();
      this.routerService.navigate(['/referral']);
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

  PostAssessment(assessment: Assessment) {
    this.isCreatingAssessment = true;
    this.assessmentService.createAssessment(assessment).subscribe(
      (result: Assessment) => {
        this.toastService.displaySuccess({
          message: 'Assessment Created'
        });
        this.isCreatingAssessment = false;
        // update the page - edit mode
        this.isFormInCreatingState = false;
        this.assessmentId = result.id;
        this.DisableForm();
      },
      error => {
        this.toastService.displayError({
          title: 'Server Error',
          message: 'Unable to create new assessment! Please try again in a few moments'
        });
        this.isCreatingAssessment = false;
        return throwError(error);
      }
    );
  }

  private DisplayCancelAssessmentCreationToast() {
    this.toastService.displayInfo({
      message: "Assessment creation has been cancelled"
    });
  }

  ReferralListNavigation() {
    this.routerService.navigate(['/referral/list']);
  }

  RoundToNearestFiveMinutes(minute: number): number {
    return Math.ceil(minute / 5) * 5;
  }

  SaveAssessment() {
    let canContinue = true;

    // check AMHP
    if (!this.HasValidAmhp()) {
      this.amhpField.setErrors({ InvalidAmhp: true });
      canContinue = false;
    }

    // check postcode
    if (!this.HasValidPostcode()) {
      this.assessmentPostcodeField.setErrors({ MissingPostcode: true });
      canContinue = false;
    }

    // check address
    if (!this.HasValidAddress()) {
      this.assessmentAddressField.setErrors({ InvalidAddress: true });
      canContinue = false;
    }

    if (this.plannedAssessmentField.value === true ) {
      // check the scheduled assessment date
      canContinue =
        this.IsAssessmentBeforeReferralCreationDate(
          this.scheduledDateField,
          this.scheduledTimeField
        ) ? false : canContinue;

    } else {
      // check the to be completed by date
      canContinue =
        this.IsAssessmentBeforeReferralCreationDate(
          this.toBeCompletedByDateField,
          this.toBeCompletedByTimeField
        ) ? false : canContinue;
    }

    if (!canContinue) {
      return;
    }

    // create an assessment object
    const assessment = {} as Assessment;

    assessment.amhpUserId = this.amhpUser.id;
    assessment.postcode = this.assessmentPostcodeField.value;
    assessment.isPlanned = this.plannedAssessmentField.value;
    assessment.meetingArrangementComment = this.additionalDetailsField.value;
    assessment.referralId = this.referralId;
    assessment.preferredDoctorGenderTypeId = this.preferredDoctorGenderField.value;
    assessment.specialityId = this.specialityField.value;

    assessment.mustBeCompletedBy =
      this.plannedAssessmentField.value ?
        null :
        this.CreateDateFromPickerObjects(this.toBeCompletedByDateField.value, this.toBeCompletedByTimeField.value);

    assessment.scheduledTime =
      this.plannedAssessmentField.value ?
        this.CreateDateFromPickerObjects(this.scheduledDateField.value, this.scheduledTimeField.value) :
        null;

    const addressLines: string[] = this.FormatAddress();
    assessment.address1 = addressLines[0];
    assessment.address2 = addressLines[1];
    assessment.address3 = addressLines[2];
    assessment.address4 = addressLines[3];

    assessment.detailTypeIds = [];

    this.selectedDetails.forEach(detail => {
      assessment.detailTypeIds.push(detail.id);
    });
    this.PostAssessment(assessment);
  }

  SelectDoctor() {
    this.routerService.navigate([`/assessment/${this.assessmentId}/select-doctors`]);
  }

  SetAmhpField(id: number | null, text: string | null) {
    const amhp = {} as TypeAheadResult;

    amhp.id = id;
    amhp.resultText = text;

    this.amhpField.setValue(amhp);
  }

  SetDefaultDateTimeFields(defaultDatetIme: Date) {
    this.toBeCompletedByDateField.setValue(this.ConvertToDateStruct(defaultDatetIme));
    this.toBeCompletedByTimeField.setValue(this.ConvertToTimeStruct(defaultDatetIme));
    this.defaultCompletionDate = this.ConvertToDateStruct(defaultDatetIme);
    this.defaultCompletionTime = this.ConvertToTimeStruct(defaultDatetIme);
  }

  TogglePlannedAssessment(event: any) {

    if (this.assessmentForm.controls.plannedAssessment.value === true) {
      // planned assessment
      const now = new Date();
      const scheduledDate = {
        year: now.getFullYear(),
        month: now.getMonth() + 1,
        day: now.getDate() + 1
      };

      const scheduledTime = {
        hour: 12,
        minute: 0,
        second: 0
      };

      this.scheduledDateField.setValue(scheduledDate);
      this.scheduledTimeField.setValue(scheduledTime);
      this.scheduledDateField.setErrors(null);

    } else {
      this.toBeCompletedByDateField.setValue(this.defaultCompletionDate);
      this.toBeCompletedByTimeField.setValue(this.defaultCompletionTime);
      this.toBeCompletedByDateField.setErrors(null);
    }
  }
}
