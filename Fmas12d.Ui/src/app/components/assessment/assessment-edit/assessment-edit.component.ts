import { ActivatedRoute, ParamMap } from '@angular/router';
import { AmhpListService } from 'src/app/services/amhp-list/amhp-list.service';
import { Component, OnInit, Renderer2, ViewChild } from '@angular/core';
import { DatePickerFormat } from 'src/app/helpers/date-picker.validator';
import { environment } from 'src/environments/environment';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { map, switchMap, catchError, tap, distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { NameIdList } from 'src/app/interfaces/name-id-list';
import { NameIdListService } from 'src/app/services/name-id-list/name-id-list.service';
import { NgbDateStruct, NgbTimeStruct, NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { Referral } from 'src/app/interfaces/referral';
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

  allocatedDoctors: string[] = [];
  cancelModal: NgbModalRef;
  defaultCompletionDate: NgbDateStruct;
  defaultCompletionTime: NgbTimeStruct;
  dropdownSettings: IDropdownSettings;
  assessmentForm: FormGroup;
  assessmentDetails: NameIdList[] = [];
  assessmentShouldBeCompletedByDate: NgbDateStruct;
  assessmentShouldBeCompletedByTime: NgbTimeStruct;
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

  @ViewChild('cancelUpdate', null) cancelUpdateTemplate;

  constructor(
    private amhpListService: AmhpListService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
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
        this.assessmentShouldBeCompletedByDate,
        [
          DatePickerFormat
        ]
      ],
      toBeCompletedByTime: [this.assessmentShouldBeCompletedByTime]
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

  CancelEdit() {
    if (this.assessmentForm.dirty) {
      this.cancelModal = this.modalService.open(this.cancelUpdateTemplate, {
        size: 'lg'
      });
    } else {
      this.routerService.navigate(['/referral']);
    }
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
    return this.assessmentForm.controls[fieldName].disabled;
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  get assessmentPostcode() {
    return this.assessmentForm.controls.postCode;
  }

  get toBeCompletedByDateField() {
    return this.assessmentForm.controls.toBeCompletedByDate;
  }

  get toBeCompletedByTimeField() {
    return this.assessmentForm.controls.toBeCompletedByTime;
  }

  InitialiseForm(referral: ReferralView) {

    const assessment = referral.currentAssessment;

    // AMHP User - mandatory field
    const AmhpUser: TypeAheadResult = {id: 1, resultText: assessment.amhpUserName };
    this.assessmentForm.controls.amhp.setValue(AmhpUser);

    this.assessmentForm.controls.meetingArrangementComment.setValue(referral.currentAssessment.meetingArrangementComment);
    this.assessmentForm.controls.fullAddress.setValue(referral.currentAssessment.fullAddress);
    this.assessmentForm.controls.postCode.setValue(referral.currentAssessment.postcode);

    this.minDate = this.ConvertToDateStruct(referral.currentAssessment.mustBeCompletedBy);
    this.SetDefaultDateTimeFields(referral.currentAssessment.mustBeCompletedBy);

    this.selectedDoctors = ['Doctor Smith', 'Doctor Jones'];
    this.allocatedDoctors = ['Doctor Livingstone'];

  }

  OnCancelModalAction(action: boolean) {
    this.cancelModal.close();
    if (action) {
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

  UpdateReferral() {

  }

}
