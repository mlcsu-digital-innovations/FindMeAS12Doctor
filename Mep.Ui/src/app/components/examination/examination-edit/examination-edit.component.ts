import { ActivatedRoute, ParamMap } from '@angular/router';
import { AmhpListService } from 'src/app/services/amhp-list/amhp-list.service';
import { Component, OnInit, Renderer2 } from '@angular/core';
import { DatePickerFormat } from 'src/app/helpers/date-picker.validator';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { map, switchMap, catchError, tap, distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { NgbDateStruct, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { ReferralView } from 'src/app/interfaces/referral-view';
import { RouterService } from 'src/app/services/router/router.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { TypeAheadResult } from 'src/app/interfaces/typeahead-result';
import * as moment from 'moment';

@Component({
  selector: 'app-examination-edit',
  templateUrl: './examination-edit.component.html',
  styleUrls: ['./examination-edit.component.css']
})
export class ExaminationEditComponent implements OnInit {

  allocatedDoctors: string[] = [];
  defaultCompletionDate: NgbDateStruct;
  defaultCompletionTime: NgbTimeStruct;
  dropdownSettings: IDropdownSettings;
  examinationForm: FormGroup;
  examinationDetails: NameIdList[] = [];
  examinationShouldBeCompletedByDate: NgbDateStruct;
  examinationShouldBeCompletedByTime: NgbTimeStruct;
  genderSelected: number;
  genderTypes: NameIdList[];
  hasAmhpSearchFailed: boolean;
  isAmhpSearching: boolean;
  isPatientIdValidated: boolean;
  isSearchingForPostcode: boolean;
  minDate: NgbDateStruct;
  pageSize: number;
  referral$: Observable<Referral | any>;
  referralCreated: Date;
  referralId: number;
  selectedDetails: NameIdList[] = [];
  selectedDoctors: string[] = [];
  specialities: NameIdList[];

  constructor(
    private amhpListService: AmhpListService,
    private formBuilder: FormBuilder,
    private nameIdListService: NameIdListService,
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
                console.log(referral);
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
    this.nameIdListService.GetListData('examinationdetailtype')
      .subscribe(details => {
        this.examinationDetails = details;
      },
      (err) => {
        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving Examination Risks'
      });
    });

    this.examinationForm = this.formBuilder.group({
      amhp: [
        ''
      ],
      currentExamination: [
        ''
      ],
      doctorNamesAccepted: [
        ''
      ],
      doctorNamesAllocated: [
        ''
      ],
      examinationDetails: [
        ''
      ],
      fullAddress: [
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
        ''
      ],
      toBeCompletedByDate: [
        this.examinationShouldBeCompletedByDate,
        [
          DatePickerFormat
        ]
      ],
      toBeCompletedByTime: [this.examinationShouldBeCompletedByTime]
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

  ClearField(fieldName: string) {
    if (this.examinationForm.contains(fieldName)) {
      this.examinationForm.controls[fieldName].setValue('');
      this.SetFieldFocus(`#${fieldName}`);
    }
  }

  ClearSelect(fieldName: string) {
    if (this.examinationForm.contains(fieldName)) {
      this.examinationForm.controls[fieldName].setValue([]);
      // this.examinationForm.controls[fieldName].updateValueAndValidity();

      this.genderSelected = 0;
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

  async Delay(milliseconds: number) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
  }

  DisableIfParentIsDisabled(fieldName: string): boolean {
    return this.examinationForm.controls[fieldName].disabled;
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  get examinationPostcode() {
    return this.examinationForm.controls.postCode;
  }

  get toBeCompletedByDateField() {
    return this.examinationForm.controls.toBeCompletedByDate;
  }

  get toBeCompletedByTimeField() {
    return this.examinationForm.controls.toBeCompletedByTime;
  }

  InitialiseForm(referral: ReferralView) {

    const examination = referral.currentExamination;

    // AMHP User - mandatory field
    const AmhpUser: TypeAheadResult = {id: 1, resultText: examination.amhpUserName };
    this.examinationForm.controls.amhp.setValue(AmhpUser);

    this.examinationForm.controls.meetingArrangementComment.setValue(referral.currentExamination.meetingArrangementComment);
    this.examinationForm.controls.fullAddress.setValue(referral.currentExamination.fullAddress);
    this.examinationForm.controls.postCode.setValue(referral.currentExamination.postcode);

    this.minDate = this.ConvertToDateStruct(referral.currentExamination.mustBeCompletedBy);
    this.SetDefaultDateTimeFields(referral.currentExamination.mustBeCompletedBy);

    this.selectedDoctors = ['Doctor Smith', 'Doctor Jones'];
    this.allocatedDoctors= ['Doctor Livingstone'];

  }

  OnItemDeselect(item: any) {
    this.selectedDetails =
      this.selectedDetails.filter(obj => obj.id !== item.id);
  }

  OnItemSelect(item: NameIdList) {
    this.selectedDetails.push(item);
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

}
