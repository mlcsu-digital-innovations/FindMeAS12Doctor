import { ActivatedRoute, ParamMap } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { LeadAmhpUser } from 'src/app/interfaces/user';
import { Patient } from 'src/app/interfaces/patient';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from '../../../services/referral/referral.service';
import { ToastService } from '../../../services/toast/toast.service';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-examination-create',
  templateUrl: './examination-create.component.html',
  styleUrls: ['./examination-create.component.css']
})
export class ExaminationCreateComponent implements OnInit {
  private referral = {} as Referral;

  isRetrievingReferralData: boolean;
  dangerMessage: string;
  referralQuery: Observable<any>;

  @ViewChild('dangerToast', null) dangerTemplate;

  constructor(
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private toastService: ToastService
  ) {}

  ngOnInit() {

    this.referral.leadAmhpUser = {} as LeadAmhpUser;
    this.referral.leadAmhpUser.displayName = null;
    this.referral.patient = {} as Patient;
    this.referral.patient.patientIdentifier = null;

    this.isRetrievingReferralData = true;

    this.referralQuery = this.route.paramMap.pipe(
      switchMap(
        (params: ParamMap) => {
          this.referral.id = +params.get('referralId');
          return this.referralService.getReferral(+params.get('referralId'));
        }
      )
    );

    this.referralQuery.subscribe(
      (referral: Referral) => {
        this.isRetrievingReferralData = false;
        this.referral = referral;
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
  }
}
