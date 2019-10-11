import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { AddressResult } from 'src/app/interfaces/address-result';
import { AmhpListService } from '../../../services/amhp-list/amhp-list.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePickerFormat } from 'src/app/helpers/date-picker.validator';
import { debounceTime, distinctUntilChanged, tap, switchMap, catchError, map } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { LeadAmhpUser } from 'src/app/interfaces/user';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { NgbDateStruct, NgbTimeStruct, NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { Patient } from 'src/app/interfaces/patient';
import { PostcodeRegex } from '../../../constants/Constants';
import { PostcodeValidationService } from 'src/app/services/postcode-validation/postcode-validation.service';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from '../../../services/referral/referral.service';
import { ToastService } from '../../../services/toast/toast.service';
import { TypeAheadResult } from 'src/app/interfaces/typeahead-result';

@Component({
  selector: 'app-examination-create',
  templateUrl: './examination-create.component.html',
  styleUrls: ['./examination-create.component.css']
})
export class ExaminationCreateComponent implements OnInit {

  addresses$: Observable<any>;
  addressList: AddressResult[];
  cancelModal: NgbModalRef;
  defaultCompletionDate: NgbDateStruct;
  defaultCompletionTime: NgbTimeStruct;
  dropdownSettings: IDropdownSettings;
  examinationDetails: NameIdList[] = [];
  examinationForm: FormGroup;
  examinationPostcodeValidationMessage: string;
  examinationShouldBeCompletedByDate: NgbDateStruct;
  examinationShouldBeCompletedByTime: NgbTimeStruct;
  genderTypes: NameIdList[];
  hasAmhpSearchFailed: boolean;
  isAmhpSearching: boolean;
  isSearchingForPostcode: boolean;
  minDate: NgbDateStruct;
  referral$: Observable<Referral | any>;
  referralCreated: Date;
  selectedDetails: NameIdList[] = [];
  specialities: NameIdList[];

  @ViewChild('cancelExamination', null) cancelExaminationTemplate;

  constructor(
    private amhpListService: AmhpListService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private nameIdListService: NameIdListService,
    private postcodeValidationService: PostcodeValidationService,
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private router: Router,
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
          return this.referralService.getReferral(+params.get('referralId'))
            .pipe(
              map(referral => {
                this.SetAmhpField(referral.leadAmhpUser.id, referral.leadAmhpUser.displayName);
                this.minDate = referral.referralCreatedAtAsDatePicker;
                this.referralCreated =
                  this.CreateDateFromPickerObjects(referral.referralCreatedAtAsDatePicker, referral.referralCreatedAtAsTimePicker);
                this.toBeCompletedByDateField.setValue(referral.defaultToBeCompletedByDate);
                this.toBeCompletedByTimeField.setValue(referral.defaultToBeCompletedByTime);

                this.defaultCompletionDate = referral.defaultToBeCompletedByDate;
                this.defaultCompletionTime = referral.defaultToBeCompletedByTime;

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
        emptyReferral.leadAmhpUser = {} as LeadAmhpUser;

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

    // dummy data - replace with service call once api updated
    this.examinationDetails.push({ id: 1, name: 'Big dog in garden' });
    this.examinationDetails.push({ id: 2, name: 'Parking is difficult' });
    this.examinationDetails.push({ id: 3, name: 'Aggressive neighbour' });

    // get the list of risks for the dropdown
    // this.nameIdListService.GetListData('examinationDetail')
    //   .subscribe(details => {
    //     this.examinationDetails = details;
    //     console.log(this.examinationDetails);
    //   },
    //   (err) => {
    //     this.toastService.displayError({
    //       title: 'Error',
    //       message: 'Error Retrieving Examination Risks'
    //   });
    // });

    this.examinationForm = this.formBuilder.group({
      plannedExamination: false,
      amhp: [''],
      examinationPostcode: [
        '',
        [
          Validators.minLength(6),
          Validators.maxLength(8),
          Validators.pattern(`${PostcodeRegex}$`)
        ]
      ],
      examinationAddress: [''],
      additionalDetails: ['',
        [
          Validators.maxLength(2000)
        ]
      ],
      speciality: [''],
      preferredGender: [''],
      examinationDetails: [''],
      scheduledDate: [''],
      scheduledTime: [''],
      toBeCompletedByDate: [
        this.examinationShouldBeCompletedByDate,
        [
          DatePickerFormat
        ]
      ],
      toBeCompletedByTime: [this.examinationShouldBeCompletedByTime]
    });
  }

  AddressSearch(): void {
    this.addressList = [];
    this.examinationAddressField.setValue('');
    this.isSearchingForPostcode = true;

    this.postcodeValidationService.searchPostcode(this.examinationPostcodeField.value)
      .subscribe(address => {
        this.addressList.push(address);
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
          this.examinationPostcodeField.setErrors({ NoResultsReturned: true });
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

  CancelExamination() {

    if (this.examinationForm.dirty) {
      this.cancelModal = this.modalService.open(this.cancelExaminationTemplate, {
        size: 'lg'
      });
    } else {
      this.router.navigate(['/referral']);
    }
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

  get additionalDetailsField() {
    return this.examinationForm.controls.additionalDetails;
  }

  get amhpField() {
    return this.examinationForm.controls.amhp;
  }

  get amhpUser(): TypeAheadResult {
    return this.examinationForm.controls.amhp.value;
  }

  get examination() {
    return this.examinationForm.controls;
  }

  get examinationAddressField() {
    return this.examinationForm.controls.examinationAddress;
  }

  get examinationPostcodeField() {
    return this.examinationForm.controls.examinationPostcode;
  }

  get plannedExaminationField() {
    return this.examinationForm.controls.plannedExamination;
  }

  get toBeCompletedByDateField() {
    return this.examinationForm.controls.toBeCompletedByDate;
  }

  get toBeCompletedByTimeField() {
    return this.examinationForm.controls.toBeCompletedByTime;
  }

  get scheduledDateField() {
    return this.examinationForm.controls.scheduledDate;
  }

  get scheduledTimeField() {
    return this.examinationForm.controls.scheduledTime;
  }

  HasValidAddress(): boolean {
    return this.examinationAddressField.value !== '';
  }

  HasValidAmhp(): boolean {
    return this.amhpUser.id !== undefined;
  }

  HasValidPostcode(): boolean {
    return (
      this.examinationPostcodeField.value !== '' &&
      this.examinationPostcodeField.errors == null
    );
  }

  IsExaminationBeforeReferralCreationDate(dateField: AbstractControl, timeField: AbstractControl ): boolean {

    const examinationDateIsBeforeReferralCreatedAt =
      (this.CreateDateFromPickerObjects(dateField.value, timeField.value)) < this.referralCreated;


    if (examinationDateIsBeforeReferralCreatedAt) {
      dateField.setErrors({InvalidExaminationDate: true});
    } else {
      dateField.setErrors({InvalidExaminationDate: false});
    }

    return examinationDateIsBeforeReferralCreatedAt;
  }

  HasInvalidPostcode(): boolean {
    return (
      this.examinationPostcodeField.value !== '' &&
      this.examinationPostcodeField.errors !== null
    );
  }

  IsSearchingForPostcode(): boolean {
    return this.isSearchingForPostcode;
  }

  OnCancelModalAction(action: boolean) {

    this.cancelModal.close();

    if (action) {
      this.router.navigate(['/referral']);
    }
  }

  OnItemDeselect(item: any) {
    this.selectedDetails =
      this.selectedDetails.filter(obj => obj.id !== item.id);
  }

  OnItemSelect(item: NameIdList) {
    this.selectedDetails.push(item);
  }

  RoundToNearestFiveMinutes(minute: number): number {
    return Math.ceil(minute / 5) * 5;
  }

  SaveExamination() {
    let canContinue = true;

    // check AMHP
    if (!this.HasValidAmhp()) {
      this.amhpField.setErrors({ InvalidAmhp: true });
      canContinue = false;
    }

    // check postcode
    if (!this.HasValidPostcode()) {
      this.examinationPostcodeField.setErrors({ MissingPostcode: true });
      canContinue = false;
    }

    // check address
    if (!this.HasValidAddress()) {
      this.examinationAddressField.setErrors({ InvalidAddress: true });
      canContinue = false;
    }


    if (this.plannedExaminationField.value === true ) {
      // check the scheduled examination date
      canContinue =
        this.IsExaminationBeforeReferralCreationDate(
          this.scheduledDateField,
          this.scheduledTimeField
        ) ? false : canContinue;

    } else {
      // check the to be completed by date
      canContinue =
        this.IsExaminationBeforeReferralCreationDate(
          this.toBeCompletedByDateField,
          this.toBeCompletedByTimeField
        ) ? false : canContinue;
    }

    console.log('can continue = ' + canContinue);
  }

  SetAmhpField(id: number | null, text: string | null) {
    const amhp = {} as TypeAheadResult;

    amhp.id = id;
    amhp.resultText = text;

    this.amhpField.setValue(amhp);
  }

  TogglePlannedExamination(event: any) {

    if (this.examinationForm.controls.plannedExamination.value === true) {
      // planned examination
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

  OpenLocationTab(): void {
    window.open(environment.locationEndpoint, '_blank');
  }
}
