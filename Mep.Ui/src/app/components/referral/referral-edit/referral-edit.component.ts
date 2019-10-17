import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NhsNumberValidFormat } from 'src/app/helpers/nhs-number.validator';
import { Observable, of } from 'rxjs';
import { ParamMap, ActivatedRoute, Router } from '@angular/router';
import { Patient } from 'src/app/interfaces/patient';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { switchMap, map, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-referral-edit',
  templateUrl: './referral-edit.component.html',
  styleUrls: ['./referral-edit.component.css']
})
export class ReferralEditComponent implements OnInit {

  isPatientIdValidated: boolean;
  patientDetails: Patient;
  referral$: Observable<Referral | any>;
  referralCreated: Date;
  referralForm: FormGroup;
  referralId: number;

  constructor(
    private formBuilder: FormBuilder,
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService
  ) { }

  ngOnInit() {

    this.referral$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.referralService.getReferral(+params.get('referralId'))
            .pipe(
              map(referral => {
                this.InitialiseForm(referral);
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

        return of(emptyReferral);
      })
    );

    this.referralForm = this.formBuilder.group({
      nhsNumber: [
        '',
        [
          Validators.maxLength(10),
          Validators.pattern('^[1-9]\\d{9}$'),
          NhsNumberValidFormat
        ]
      ]
    });

    this.patientDetails = {} as Patient;
    this.isPatientIdValidated = false;
    this.OnChanges();

  }

  get nhsNumber(): string {
    return this.referralForm.controls.nhsNumber.value;
  }

  get nhsNumberField() {
    return this.referralForm.controls.nhsNumber;
  }

  HasInvalidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' && this.nhsNumberField.errors !== null
    );
  }

  HasNoPatientIdErrors(): boolean {
    return (
      this.nhsNumberField.errors == null
      // this.alternativeIdentifierField.errors == null
    );
  }

  HasValidNHSNumber(): boolean {
    return (
      this.nhsNumberField.value !== '' &&
      this.nhsNumberField.value !== null &&
      this.nhsNumberField.errors == null
    );
  }

  InitialiseForm(referral: Referral) {
    this.referralCreated = referral.createdAt;
    this.referralId = referral.id;
    this.nhsNumberField.setValue(referral.patient.nhsNumber);
  }

  OnChanges(): void {

    // fields are NOT validated if they are changed after initial validation
    this.nhsNumberField.valueChanges.subscribe(val => {
      this.isPatientIdValidated = val === this.patientDetails.nhsNumber;
    });

    // this.alternativeIdentifierField.valueChanges.subscribe((val: string) => {
    //   if (this.patientDetails.alternativeIdentifier && this.patientDetails.alternativeIdentifier !== '') {
    //     this.isPatientIdValidated = val.toUpperCase() === this.patientDetails.alternativeIdentifier.toUpperCase();
    //   }
    // });

    // this.residentialPostcodeField.valueChanges.subscribe((val: string) => {
    //   if (this.patientDetails.residentialPostcode && this.patientDetails.residentialPostcode !== '') {
    //     this.isPatientPostcodeValidated =
    //       this.RemoveWhiteSpace(val).toUpperCase() === this.RemoveWhiteSpace(this.patientDetails.residentialPostcode).toUpperCase();
    //   }
    // });
  }

}
