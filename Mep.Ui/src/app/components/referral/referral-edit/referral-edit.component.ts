import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ParamMap, ActivatedRoute, Router } from '@angular/router';
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

  referralCreated: Date;
  referralId: number;
  referral$: Observable<Referral | any>;

  constructor(
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

                this.referralCreated = referral.createdAt;
                this.referralId = referral.id;

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
  }

}
