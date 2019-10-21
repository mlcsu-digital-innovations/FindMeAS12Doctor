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
  selector: 'app-examination-view',
  templateUrl: './examination-view.component.html',
  styleUrls: ['./examination-view.component.css']
})
export class ExaminationViewComponent implements OnInit {

  isPatientIdValidated: boolean;
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
        console.log(err);
        this.toastService.displayError({
          title: 'Error',
          message: 'Error Retrieving Referral Information'
        });

        const emptyReferral = {} as Referral;

        return of(emptyReferral);
      })
    );

  }


  InitialiseForm(referral: Referral) {
    this.referralCreated = referral.createdAt;
    this.referralId = referral.id;

    console.log(referral);
  }

}
