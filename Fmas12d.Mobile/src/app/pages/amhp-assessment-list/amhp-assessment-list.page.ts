import { AmhpAssessmentList } from '../../models/amhp-assessment-list.model';
import { AmhpAssessmentService } from '../../services/amhp-assessment/amhp-assessment.service';
import { Component } from '@angular/core';
import { LoadingController } from '@ionic/angular';
import { REFERRALSTATUSASSESSMENTSCHEDULED } from 'src/app/constants/app.constants';

@Component({
  selector: 'app-amhp-assessment-list',
  templateUrl: './amhp-assessment-list.page.html',
  styleUrls: ['./amhp-assessment-list.page.scss'],
})
export class AmhpAssessmentListPage {
  public assessmentListLastUpdated: Date;
  public assessmentListScheduled: AmhpAssessmentList[] = [];
  public assessmentListUnscheduled: AmhpAssessmentList[] = [];
  private loading: HTMLIonLoadingElement;

  constructor(
    private assessmentService: AmhpAssessmentService,
    private loadingController: LoadingController
  ) { }

  ionViewDidEnter() {
    this.refreshPage();
  }

  refreshPage($event?: any) {
    const request = this.assessmentService.getList();
    this.showLoading();

    request.subscribe((result: AmhpAssessmentList[]) => {
      this.assessmentListLastUpdated = new Date();
      this.assessmentListScheduled = result
        .filter(assessment => assessment.referralStatusId >= REFERRALSTATUSASSESSMENTSCHEDULED);
      this.assessmentListUnscheduled = result
        .filter(assessment => assessment.referralStatusId < REFERRALSTATUSASSESSMENTSCHEDULED);
      this.closeLoading();
      this.closeRefreshing($event);
    }, error => {
      this.closeLoading();
      this.closeRefreshing($event);
    });
  }

  closeLoading() {
    if (this.loading) {
      setTimeout(() => { this.loading.dismiss(); }, 500);
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
