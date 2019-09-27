import { Component, OnInit } from '@angular/core';
import { ReferralService } from '../../../services/referral/referral.service';
import { Referral } from 'src/app/interfaces/referral';
import { LeadAmhpUser } from 'src/app/interfaces/user';
import { Patient } from 'src/app/interfaces/patient';

@Component({
  selector: 'app-examination-create',
  templateUrl: './examination-create.component.html',
  styleUrls: ['./examination-create.component.css']
})
export class ExaminationCreateComponent implements OnInit {

  private referral: Referral;

  constructor(
    private referralService: ReferralService
  ) { }

  ngOnInit() {
    this.referral = {} as Referral;
    this.referral.id = null;
    this.referral.leadAmhpUser = {} as LeadAmhpUser;
    this.referral.leadAmhpUser.displayName = null;
    this.referral.patient = {} as Patient;
    this.referral.patient.patientIdentifier = null;

    console.log('get');
    // fetch the latest referral details
    this.referralService
    .getReferral(1009)
    .subscribe(
      (referral: Referral) => {
        console.log(referral);
        this.referral = referral;
        // /this.referral.patient.patientIdentifier =
        //this.referral.patient.nhsNumber != null ? this.referral.patient.nhsNumber.toString() : this.referral.patient.alternativeIdentifier;
      });

  }

}
