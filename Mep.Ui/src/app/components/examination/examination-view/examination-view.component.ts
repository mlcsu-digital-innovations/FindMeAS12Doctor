import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { ParamMap, ActivatedRoute, Router } from '@angular/router';
import { Patient } from 'src/app/interfaces/patient';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from 'src/app/services/referral/referral.service';
import { switchMap, map, catchError } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';
import { Examination } from 'src/app/interfaces/examination';
import { ReferralView } from 'src/app/interfaces/referral-view';

@Component({
  selector: 'app-examination-view',
  templateUrl: './examination-view.component.html',
  styleUrls: ['./examination-view.component.css']
})
export class ExaminationViewComponent implements OnInit {

  isPatientIdValidated: boolean;
  referral$: Observable<Referral | any>;
  referralCreated: Date;
  currentExaminationForm: FormGroup;
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
          return this.referralService.getReferralView(+params.get('referralId'))
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



    this.currentExaminationForm = this.formBuilder.group({
      AmhpUser: [
        {
          value: '',
          disabled: true
        }
      ]
    });
  }


  InitialiseForm(referral: ReferralView) {
    this.referralId = referral.id;
    this.currentExaminationForm.controls.AmhpUser.setValue(referral.currentExamination.amhpUserName);
    console.log(referral);
  }

}
