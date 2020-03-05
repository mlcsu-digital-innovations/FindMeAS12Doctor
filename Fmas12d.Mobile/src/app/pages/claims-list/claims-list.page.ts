import { Assessment } from 'src/app/models/assessment.model';
import { AssessmentClaimService } from 'src/app/services/assessment-claims/assessment-claims.service';
import { CLAIMSTATUSSUBMITTED, CLAIMSTATUSPROCESSING, CLAIMSTATUSQUERY, CLAIMSTATUSAPPROVED, CLAIMSTATUSAWAITING, CLAIMSTATUSREJECTED } from 'src/app/constants/app.constants';
import { Component } from '@angular/core';
import { IconDetail } from 'src/app/interfaces/icon-detail.interface';
import { LoadingController } from '@ionic/angular';
import { UserAssessmentClaim } from 'src/app/models/user-assessment-claim.model';
import { UserAssessmentClaimList } from 'src/app/models/user-assessment-claim-list.model';

@Component({
  selector: 'app-claims-list',
  templateUrl: './claims-list.page.html',
  styleUrls: ['./claims-list.page.scss'],
})
export class ClaimsListPage {

  public listLastUpdated: Date;
  private loading: HTMLIonLoadingElement;
  private hasData: boolean;

  assessmentList: Assessment[] = [];
  claimsList: UserAssessmentClaim[] = [];

  constructor(
    private assessmentClaimService: AssessmentClaimService,
    private loadingController: LoadingController
  ) { }

  ionViewWillEnter() {
    this.refreshPage();
  }

  refreshPage($event?: any) {
    const request = this.assessmentClaimService.getList();
    this.showLoading();

    request.subscribe((result: UserAssessmentClaimList) => {
      this.hasData = true;
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

  GetIconColourForClaimStatus(claimStatusId: number): string {
    return this.GetIconDetailsForClaimStatus(claimStatusId).colour;
  }

  GetIconNameForClaimStatus(claimStatusId: number): string {
    return this.GetIconDetailsForClaimStatus(claimStatusId).name;
  }

  GetIconDetailsForClaimStatus(claimStatusId: number): IconDetail {

    const iconDetail = {} as IconDetail;

    switch (claimStatusId) {
      case CLAIMSTATUSSUBMITTED:
        iconDetail.name = 'paper-plane';
        iconDetail.colour = 'warning';
        break;
      case CLAIMSTATUSPROCESSING:
        iconDetail.name = 'filing';
        iconDetail.colour = 'warning';
        break;
      case CLAIMSTATUSQUERY:
        iconDetail.name = 'help-circle';
        iconDetail.colour = 'warning';
        break;
      case CLAIMSTATUSAPPROVED:
        iconDetail.name = 'checkmark-circle';
        iconDetail.colour = 'success';
        break;
      case CLAIMSTATUSAWAITING:
        iconDetail.name = 'hourglass';
        iconDetail.colour = 'warning';
        break;
      case CLAIMSTATUSREJECTED:
        iconDetail.name = 'close-circle';
        iconDetail.colour = 'danger';
        break;
    }
    return iconDetail;
  }

  nothingToDisplay(): boolean {
    return this.claimsList.length === 0
      && this.assessmentList.length === 0
      && this.hasData;
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
