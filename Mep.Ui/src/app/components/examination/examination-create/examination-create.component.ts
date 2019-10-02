import { ActivatedRoute, ParamMap } from '@angular/router';
import { AmhpListService } from '../../../services/amhp-list/amhp-list.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { debounceTime, distinctUntilChanged, tap, switchMap, catchError, map } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LeadAmhpUser } from 'src/app/interfaces/user';
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
  dangerMessage: string;
  errMessage: string;
  examinationForm: FormGroup;
  examinationPostcodeValidationMessage: string;
  hasAmhpSearchFailed: boolean;
  isAmhpSearching: boolean;
  isSearchingForPostcode: boolean;
  referral$: Observable<Referral | any>;

  @ViewChild('dangerToast', null) dangerTemplate;

  constructor(
    private amhpListService: AmhpListService,
    private formBuilder: FormBuilder,
    private postcodeValidationService: PostcodeValidationService,
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private toastService: ToastService
  ) {}

  ngOnInit() {

    this.referral$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.referralService.getReferral(+params.get('referralId'))
          .pipe(
            map(referral => {
              this.SetAmhpField(referral.leadAmhpUser.id, referral.leadAmhpUser.displayName);
              return referral;
            })
          );
        }
      ),
      catchError((err) => {
        this.errMessage = err.error;
        this.dangerMessage = `Error retrieving referral information.`;
        this.toastService.show(this.dangerTemplate, {
          classname: 'bg-danger text-light',
          delay: 10000
        });

        const emptyReferral = {} as Referral;
        emptyReferral.patient = {} as Patient;
        emptyReferral.leadAmhpUser = {} as LeadAmhpUser;

        return of(emptyReferral);
      })
    );

    this.examinationForm = this.formBuilder.group({
      amhp: [''],
      examinationPostcode: [
        '',
        [
          Validators.minLength(6),
          Validators.maxLength(8),
          Validators.pattern(`${PostcodeRegex}$`)
        ]
      ],
    });
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

  get amhpField() {
    return this.examinationForm.controls.amhp;
  }

  get examination() {
    return this.examinationForm.controls;
  }

  get examinationPostcodeField() {
    return this.examinationForm.controls.examinationPostcode;
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

    this.isSearchingForPostcode = true;

    this.postcodeValidationService.searchPostcode(this.examinationPostcodeField.value)
    .subscribe( addresses => {
      this.isSearchingForPostcode = false;

      // ToDo: Do something with the results

    }, err => {
      this.isSearchingForPostcode = false;

      // ToDo: Warn the user of the error ?
    });
   }
}