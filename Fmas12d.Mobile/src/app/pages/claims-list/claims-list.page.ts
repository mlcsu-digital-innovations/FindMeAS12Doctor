import { Component, OnInit } from '@angular/core';
import { ClaimService } from 'src/app/services/assessment-claims/assessment-claims.service';
import { LoadingController } from '@ionic/angular';
import { AssessmentClaim } from 'src/app/models/assessment-claim.model';

@Component({
  selector: 'app-claims-list',
  templateUrl: './claims-list.page.html',
  styleUrls: ['./claims-list.page.scss'],
})
export class ClaimsListPage implements OnInit {

  public listLastUpdated: Date;
  private loading: HTMLIonLoadingElement;

  private assessmentList: AssessmentClaim[] = [];
  private claimsList: AssessmentClaim[] = [];

  constructor(
    private claimService: ClaimService,
    private loadingController: LoadingController
  ) { }

  ngOnInit() {
    this.refreshPage();
  }

  refreshPage($event?: any) {
    const request = this.claimService.getList();
    this.showLoading();

    request.subscribe((result: AssessmentClaim[]) => {
      this.listLastUpdated = new Date();
      console.log(result);
      this.claimsList = result
        .filter(assessment => assessment.claim !== undefined);
      this.assessmentList = result
        .filter(assessment => assessment.claim === undefined);
      this.closeLoading();
      this.closeRefreshing($event);
      console.log(this.claimsList);
      console.log(this.assessmentList);
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
