import { Component, OnInit, Renderer2 } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { ToastService } from 'src/app/services/toast/toast.service';
import { Referral } from 'src/app/interfaces/referral';
import { Observable, of } from 'rxjs';
import { map, switchMap, catchError, tap, distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { ReferralView } from 'src/app/interfaces/referral-view';
import { AmhpListService } from 'src/app/services/amhp-list/amhp-list.service';
import { TypeAheadResult } from 'src/app/interfaces/typeahead-result';

@Component({
  selector: 'app-examination-edit',
  templateUrl: './examination-edit.component.html',
  styleUrls: ['./examination-edit.component.css']
})
export class ExaminationEditComponent implements OnInit {

  examinationForm: FormGroup;
  isPatientIdValidated: boolean;
  pageSize: number;
  referral$: Observable<Referral | any>;
  referralCreated: Date;
  referralId: number;
  hasAmhpSearchFailed: boolean;
  isAmhpSearching: boolean;
  isSearchingForPostcode: boolean;

  constructor(
    private amhpListService: AmhpListService,
    private formBuilder: FormBuilder,
    private referralService: ReferralService,
    private renderer: Renderer2,
    private route: ActivatedRoute,
    private toastService: ToastService,
  ) { }

  ngOnInit() {
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
      preferredDoctorGenderTypeName: [
        ''
      ],
      specialityName: [
        ''
      ],
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

  InitialiseForm(referral: ReferralView) {

    const examination = referral.currentExamination;

    // AMHP User - mandatory field
    const AmhpUser: TypeAheadResult = {id: 1, resultText: examination.amhpUserName };
    this.examinationForm.controls.amhp.setValue(AmhpUser);

    this.examinationForm.controls.meetingArrangementComment.setValue(referral.currentExamination.meetingArrangementComment);
    this.examinationForm.controls.fullAddress.setValue(referral.currentExamination.fullAddress);
    this.examinationForm.controls.postCode.setValue(referral.currentExamination.postcode);
  }

  async SetFieldFocus(fieldName: string) {
    // ToDo: Find a better way to do this !
    await this.Delay(100);
    this.renderer.selectRootElement(fieldName).focus();
  }

}
