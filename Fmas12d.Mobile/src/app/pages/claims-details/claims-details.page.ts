import { Component, OnInit } from '@angular/core';
import { ClaimService } from 'src/app/services/assessment-claims/assessment-claims.service';
import { LoadingController } from '@ionic/angular';
import { AssessmentClaim } from 'src/app/models/assessment-claim.model';
import { ActivatedRoute } from '@angular/router';
import { UserAssessmentClaim } from 'src/app/models/user-assessment-claim.model';

@Component({
  selector: 'app-claims-details',
  templateUrl: './claims-details.page.html',
  styleUrls: ['./claims-details.page.scss'],
})
export class ClaimsDetailsPage implements OnInit {

  public lastUpdated: Date;
  private loading: HTMLIonLoadingElement;
  public claimId: number;
  public claim: UserAssessmentClaim;

  private assessmentList: AssessmentClaim[] = [];
  private claimsList: AssessmentClaim[] = [];

  constructor(
    private route: ActivatedRoute,
    private claimService: ClaimService,
    private loadingController: LoadingController
  ) { }

  ngOnInit() {
    this.claimId = +this.route.snapshot.paramMap.get('claimId');
    this.refreshPage();
  }

  refreshPage($event?: any) {
    const request = this.claimService.getClaim(this.claimId);
    this.showLoading();

    request.subscribe((result: UserAssessmentClaim) => {
      this.lastUpdated = new Date();
      console.log(result);
      this.claim = result;
      console.log(this.claim);
    }, error => {
      this.closeLoading();
      this.closeRefreshing($event);
    });    
  }  

  closeLoading() {
    if (this.loading) {
      this.loading.dismiss();
    }
  }

  closeRefreshing($event?: any) {
    if ($event) {
      $event.target.complete();
    }
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines',
      duration: 5000
    });
    await this.loading.present();
  }
}

