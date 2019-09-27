import { ActivatedRoute } from '@angular/router';
import { AmhpListService } from '../../../services/amhp-list/amhp-list.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { debounceTime, distinctUntilChanged, tap, switchMap, catchError } from 'rxjs/operators';
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
  private referral = {} as Referral;

  dangerMessage: string;
  examinationForm: FormGroup;
  hasAmhpSearchFailed: boolean;
  isAmhpSearching: boolean;
  isRetrievingReferralData: boolean;

  @ViewChild('dangerToast', null) dangerTemplate;

  constructor(
    private amhpListService: AmhpListService,
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private toastService: ToastService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {

    this.referral.id = parseInt(
      this.route.snapshot.paramMap.get('referralId'),
      null
    );
    this.referral.leadAmhpUser = {} as LeadAmhpUser;
    this.referral.leadAmhpUser.displayName = null;
    this.referral.patient = {} as Patient;
    this.referral.patient.patientIdentifier = null;

    this.isRetrievingReferralData = false;

    // fetch the latest referral details
    this.referralService.getReferral(this.referral.id).subscribe(
      (referral: Referral) => {
        console.log(referral);
        this.isRetrievingReferralData = false;
        this.referral = referral;
        this.referral.patient.patientIdentifier =
          this.referral.patient.nhsNumber != null
            ? this.referral.patient.nhsNumber.toString()
            : this.referral.patient.alternativeIdentifier;

        // ToDo: Inform the user if this referral already has an active examination

        // Set known fields
        this.SetAmhpField(referral.leadAmhpUserId, referral.leadAmhpUser.displayName);
      },
      err => {
        this.isRetrievingReferralData = false;
        this.dangerMessage = `Unable to find referral with id: ${this.referral.id}`;
        this.toastService.show(this.dangerTemplate, {
          classname: 'bg-danger text-light',
          delay: 10000
        });
      }
    );

    this.examinationForm = this.formBuilder.group({
      amhp: ['']
    });

  }

  get amhpField() {
    return this.examinationForm.controls.amhp;
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
