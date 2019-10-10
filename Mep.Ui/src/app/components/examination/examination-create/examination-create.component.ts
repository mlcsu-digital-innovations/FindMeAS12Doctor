import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { AddressResult } from 'src/app/interfaces/address-result';
import { AmhpListService } from '../../../services/amhp-list/amhp-list.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePickerFormat } from 'src/app/helpers/date-picker.validator';
import { debounceTime, distinctUntilChanged, tap, switchMap, catchError, map } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
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
  ) {}

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
              this.toBeCompletedByDateField.setValue(referral.defaultToBeCompletedByDate);
              this.toBeCompletedByTimeField.setValue(referral.defaultToBeCompletedByTime);

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
    this.examinationDetails.push({id: 1, name: 'Big dog in garden'});
    this.examinationDetails.push({id: 2, name: 'Parking is difficult'});
    this.examinationDetails.push({id: 3, name: 'Aggressive neighbour'});

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

      this.examinationForm.controls.scheduledDate.setValue(scheduledDate);
      this.examinationForm.controls.scheduledTime.setValue(scheduledTime);
    }

  }

  onItemSelect(item: NameIdList) {
    this.selectedDetails.push(item);
  }

  onItemDeselect(item: any) {
    this.selectedDetails =
      this.selectedDetails.filter(obj => obj.id !== item.id);
  }

  CancelExamination() {
    console.log('Cancelling examination');
    console.log(this.examinationForm.dirty);

    if (this.examinationForm.dirty) {
      this.cancelModal = this.modalService.open(this.cancelExaminationTemplate, {
        size: 'lg'
      });
    } else {
      this.router.navigate(['/referral']);
    }
  }

  onCancelModalAction(action: boolean) {

    this.cancelModal.close();

    if (action) {
      this.router.navigate(['/referral']);
    }
  }

  RoundToNearestFiveMinutes(minute: number): number {
    return Math.ceil(minute / 5) * 5;
  }

  HasValidPostcode(): boolean {
    return (
      this.examinationPostcodeField.value !== '' &&
      this.examinationPostcodeField.errors == null
    );
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

  get toBeCompletedByDateField() {

    // console.log(this.examinationForm.controls.toBeCompletedByDate.errors);


    return this.examinationForm.controls.toBeCompletedByDate;
  }

  get toBeCompletedByTimeField() {
    return this.examinationForm.controls.toBeCompletedByTime;
  }

  get additionalDetailsField() {
    return this.examinationForm.controls.additionalDetails;
  }

  get amhpField() {
    return this.examinationForm.controls.amhp;
  }

  get examination() {
    return this.examinationForm.controls;
  }

  get examinationPostcodeField() {
    return this.examinationForm.controls.examinationPostcode;
  }

  get examinationAddressField() {
    return this.examinationForm.controls.examinationAddress;
  }

  SetAmhpField(id: number | null, text: string | null) {
    const amhp = {} as TypeAheadResult;

    amhp.id = id;
    amhp.resultText = text;

    this.amhpField.setValue(amhp);
  }

  FormatTypeAheadResults(value: any): string {
    return value.resultText || '';
  }

  amhpSearch = (text$: Observable<string>) =>
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
          this.examinationPostcodeField.setErrors({NoResultsReturned: true});
        }
      });
   }

   OpenLocationTab(): void {
      window.open(environment.locationEndpoint, '_blank');
   }
}
