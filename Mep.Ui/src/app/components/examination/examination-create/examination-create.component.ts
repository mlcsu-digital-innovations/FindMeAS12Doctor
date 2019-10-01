import { ActivatedRoute, ParamMap } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { LeadAmhpUser } from 'src/app/interfaces/user';
import { Patient } from 'src/app/interfaces/patient';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from '../../../services/referral/referral.service';
import { ToastService } from '../../../services/toast/toast.service';
import { switchMap, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-examination-create',
  templateUrl: './examination-create.component.html',
  styleUrls: ['./examination-create.component.css']
})
export class ExaminationCreateComponent implements OnInit {

  dangerMessage: string;
  errMessage: string;
  referral$: Observable<Referral | any>;

  @ViewChild('dangerToast', null) dangerTemplate;

  constructor(
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private toastService: ToastService
  ) {}

  ngOnInit() {

    this.referral$ = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          return this.referralService.getReferral(+params.get('referralId'));
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
  }
}
