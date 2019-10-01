import { ActivatedRoute, ParamMap } from '@angular/router';
import { AmhpListService } from '../../../services/amhp-list/amhp-list.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { debounceTime, distinctUntilChanged, tap, switchMap, catchError, map } from 'rxjs/operators';
import { LeadAmhpUser } from 'src/app/interfaces/user';
import { Observable, of } from 'rxjs';
import { Patient } from 'src/app/interfaces/patient';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from '../../../services/referral/referral.service';
import { ToastService } from '../../../services/toast/toast.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TypeAheadResult } from 'src/app/interfaces/typeahead-result';


@Component({
  selector: 'app-examination-create',
  templateUrl: './examination-create.component.html',
  styleUrls: ['./examination-create.component.css']
})
export class ExaminationCreateComponent implements OnInit {

  dangerMessage: string;
  errMessage: string;
  examinationForm: FormGroup;
  examinationPostcodeValidationMessage: string;
  hasAmhpSearchFailed: boolean;
  isAmhpSearching: boolean;

  referral$: Observable<Referral | any>;

  @ViewChild('dangerToast', null) dangerTemplate;

  constructor(
    private amhpListService: AmhpListService,
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private toastService: ToastService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {

    const postcodeRegex =
      '^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$';

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
          Validators.pattern(postcodeRegex)
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

}
