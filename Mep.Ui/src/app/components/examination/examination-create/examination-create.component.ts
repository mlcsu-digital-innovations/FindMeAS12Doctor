import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { LeadAmhpUser } from 'src/app/interfaces/user';
import { Patient } from 'src/app/interfaces/patient';
import { Referral } from 'src/app/interfaces/referral';
import { ReferralService } from '../../../services/referral/referral.service';
import { ToastService } from '../../../services/toast/toast.service';

@Component({
  selector: 'app-examination-create',
  templateUrl: './examination-create.component.html',
  styleUrls: ['./examination-create.component.css']
})
export class ExaminationCreateComponent implements OnInit {
  private referral = {} as Referral;

  isRetrievingReferralData: boolean;
  dangerMessage: string;

  @ViewChild('dangerToast', null) dangerTemplate;

  constructor(
    private referralService: ReferralService,
    private route: ActivatedRoute,
    private toastService: ToastService
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

    this.isRetrievingReferralData = true;

    // fetch the latest referral details
    this.referralService.getReferral(this.referral.id).subscribe(
      (referral: Referral) => {
        this.isRetrievingReferralData = false;
        this.referral = referral;

        // ToDo: Inform the user if this referral already has an active examination
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
