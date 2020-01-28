import { Assessment } from 'src/app/models/assessment.model';
import { AssessmentClaimService } from 'src/app/services/assessment-claims/assessment-claims.service';
import { Component, OnInit } from '@angular/core';
import { LoadingController } from '@ionic/angular';
import { UserAssessmentClaim } from 'src/app/models/user-assessment-claim.model';
import { UserAssessmentClaimList } from 'src/app/models/user-assessment-claim-list.model';

@Component({
  selector: 'app-claims-list',
  templateUrl: './claims-list.page.html',
  styleUrls: ['./claims-list.page.scss'],
})
export class ClaimsListPage implements OnInit {

  public listLastUpdated: Date;
  private loading: HTMLIonLoadingElement;

  private assessmentList: Assessment[] = [];
  private claimsList: UserAssessmentClaim[] = [];

  constructor(
    private assessmentClaimService: AssessmentClaimService,
    private loadingController: LoadingController
  ) { }

  ngOnInit() {
    this.refreshPage();
  }

  refreshPage($event?: any) {
    const request = this.assessmentClaimService.getList();
    this.showLoading();

    request.subscribe((result: UserAssessmentClaimList) => {
      this.listLastUpdated = new Date();
      this.claimsList = result.claims;
      this.assessmentList = result.assessments;
      this.closeLoading();
      this.closeRefreshing($event);
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
      duration: 3000
    });
    await this.loading.present();
  }
}
